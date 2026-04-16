import { Component, ChangeDetectionStrategy, signal, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { VendorService } from '../vendor-service';
import { VendorAssignment  } from '../models/vendor.dto';
import { CreateVendorAssignmentDto } from '../models/vendor.dto';

@Component({
  selector: 'app-vendor-form',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './vendor-form-component.html',
  styleUrls: ['./vendor-form-component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class VendorFormComponent implements OnInit {
  private fb = inject(FormBuilder);
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private vendorService = inject(VendorService);

  // State
  mode = signal<'dispatch' | 'edit'>('dispatch');
  targetId = signal<number | null>(null); // Represents either requestID or assignmentID
  isLoading = signal(false);
  error = signal<string | null>(null);

  // Non-nullable Reactive Form
  form = this.fb.group({
    vendorName: ['', Validators.required],
    completionDate: [''],
    cost: [0, [Validators.min(0)]]
  });

  ngOnInit(): void {
    // Determine mode based on URL path (e.g., /vendors/dispatch/123 vs /vendors/edit/456)
    const urlSegment = this.route.snapshot.url?.[0]?.path;
    if (urlSegment === 'edit') {
      this.mode.set('edit');
    } else {
      this.mode.set('dispatch');
    }

    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.targetId.set(Number(id));
      if (this.mode() === 'edit') {
        this.loadExistingAssignmentForEdit(Number(id));
      }
    }
  }

  loadExistingAssignmentForEdit(assignmentId: number): void {
    // In a production app, you might have a getById() method. 
    // Here we fetch the list and find the specific assignment to populate the form.
    this.isLoading.set(true);
    this.vendorService.getVendorAssignments().subscribe({
      next: (assignments) => {
        const assign = assignments.find(a => a.assignmentID === assignmentId);
        if (assign) {
          this.form.patchValue({
            vendorName: assign.vendorName,
            completionDate: assign.completionDate ? this.formatDateForInput(assign.completionDate) : '',
            cost: assign.cost || 0
          });
        }
        this.isLoading.set(false);
      },
      error: () => {
        this.error.set('Failed to load assignment details.');
        this.isLoading.set(false);
      }
    });
  }

  onSubmit(): void {
    if (this.form.invalid || !this.targetId()) return;

    this.isLoading.set(true);
    this.error.set(null);
    const formValues = this.form.getRawValue();

    if (this.mode() === 'dispatch') {
      this.dispatchNewVendor(formValues);
    } else {
      this.updateExistingAssignment(formValues);
    }
  }

  private dispatchNewVendor(formValues: any): void {
    // ✅ CHANGED TYPE TO CreateVendorAssignmentDto
    const newAssignment: CreateVendorAssignmentDto = {
      requestID: this.targetId()!,
      vendorName: formValues.vendorName!.trim(),
      assignedDate: new Date().toISOString(),
      completionDate: formValues.completionDate || undefined,
      cost: Number(formValues.cost)
    };

    this.vendorService.assignVendor(newAssignment).subscribe({
      next: () => this.navigateBack(),
      error: () => {
        this.error.set('Failed to dispatch vendor.');
        this.isLoading.set(false);
      }
    });
  }

  private updateExistingAssignment(formValues: any): void {
    const payload = {
      vendorName: formValues.vendorName!.trim(),
      completionDate: formValues.completionDate || undefined,
      cost: Number(formValues.cost)
    };

    this.vendorService.updateVendorAssignment(this.targetId()!, payload).subscribe({
      next: () => this.navigateBack(),
      error: () => {
        this.error.set('Failed to update vendor assignment[cite: 134].');
        this.isLoading.set(false);
      }
    });
  }

  cancel(): void {
    this.navigateBack();
  }

  private navigateBack(): void {
    this.router.navigate(['/manager-dashboard/vendors']);
  }

  private formatDateForInput(dateString: string): string {
    const d = new Date(dateString);
    if (isNaN(d.getTime())) return '';
    return d.toISOString().slice(0, 10);
  }
}