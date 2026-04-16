import { Component, ChangeDetectionStrategy, signal, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MaintenanceService } from '../maintenance-service';

@Component({
  selector: 'app-maintenance-form',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './maintenance-form-component.html',
  styleUrls: ['./maintenance-form-component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class MaintenanceFormComponent implements OnInit {
  private fb = inject(FormBuilder);
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private maintenanceService = inject(MaintenanceService);

  requestId = signal<number | null>(null);
  isLoading = signal(false);
  error = signal<string | null>(null);

  form = this.fb.nonNullable.group({
    category: ['', Validators.required],
    priority: ['Low', Validators.required],
    status: ['Open', Validators.required]
  });

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.requestId.set(Number(id));
      // In a fully developed backend, you would fetch the item by ID here to populate the form.
      // e.g., this.maintenanceService.getById(id).subscribe(data => this.form.patchValue(data));
    }
  }

  saveMaintenanceEdit(): void {
    if (this.form.invalid || !this.requestId()) return;
    
    this.isLoading.set(true);
    const payload = this.form.getRawValue();

    this.maintenanceService.updateMaintenanceRequest(this.requestId()!, payload).subscribe({
      next: () => {
        this.isLoading.set(false);
        this.router.navigate(['/manager-dashboard/maintenance']);
      },
      error: () => {
        this.error.set('Failed to update maintenance request');
        this.isLoading.set(false);
      }
    });
  }

  cancel(): void {
    this.router.navigate(['/manager-dashboard/maintenance']);
  }
}