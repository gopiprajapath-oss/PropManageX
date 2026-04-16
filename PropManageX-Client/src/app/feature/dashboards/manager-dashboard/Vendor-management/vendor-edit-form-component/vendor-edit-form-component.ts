import { Component, ChangeDetectionStrategy, signal, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { VendorService } from '../vendor-service';

@Component({
  selector: 'app-vendor-edit-form',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './vendor-edit-form-component.html',
  styleUrls: ['./vendor-edit-form-component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class VendorEditFormComponent implements OnInit {
  private fb = inject(FormBuilder);
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private vendorService = inject(VendorService);

  assignmentId = signal<number | null>(null);
  isLoading = signal(false);
  isFetching = signal(true);
  error = signal<string | null>(null);

  // We use nonNullable for guaranteed values, but allow nulls for optional fields
  form = this.fb.group({
    vendorName: ['', Validators.required],
    completionDate: [''],
    cost: [0, [Validators.required, Validators.min(0)]]
  });

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.assignmentId.set(Number(id));
      this.loadAssignmentData(Number(id));
    } else {
      this.error.set('Invalid Assignment ID');
      this.isFetching.set(false);
    }
  }

  private loadAssignmentData(id: number): void {
    this.vendorService.getVendorAssignmentById(id).subscribe({
      next: (data) => {
        // Transform date to YYYY-MM-DD for the HTML date input if it exists
        const formattedDate = data.completionDate ? new Date(data.completionDate).toISOString().slice(0, 10) : '';
        
        this.form.patchValue({
          vendorName: data.vendorName,
          completionDate: formattedDate,
          cost: data.cost ?? 0
        });
        this.isFetching.set(false);
      },
      error: () => {
        this.error.set('Failed to load assignment details.');
        this.isFetching.set(false);
      }
    });
  }

  saveAssignmentEdit(): void {
    if (this.form.invalid || !this.assignmentId()) return;
    
    this.isLoading.set(true);
    const formValue = this.form.value;

    const payload = {
      vendorName: formValue.vendorName!.trim(),
      completionDate: formValue.completionDate || undefined,
      cost: Number(formValue.cost)
    };

    this.vendorService.updateVendorAssignment(this.assignmentId()!, payload).subscribe({
      next: () => {
        this.isLoading.set(false);
        this.router.navigate(['/manager-dashboard/vendors']);
      },
      error: () => {
        this.error.set('Failed to update vendor assignment');
        this.isLoading.set(false);
      }
    });
  }

  cancel(): void {
    this.router.navigate(['/manager-dashboard/vendors']);
  }
}