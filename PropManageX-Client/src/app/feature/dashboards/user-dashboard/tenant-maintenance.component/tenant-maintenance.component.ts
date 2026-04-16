import { Component, OnInit, ChangeDetectionStrategy, inject, signal } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { NonNullableFormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { TenantMaintenanceService, MaintenanceRequest, CreateMaintenanceRequest, TenantUnit } from '../../user-dashboard/tenant-maintenance.component/tenant-maintenance.service'; // Adjust path
import { TokenService } from '../../../../core/services/token.service'; // ✅ Inject TokenService

@Component({
  selector: 'app-tenant-maintenance',
  imports: [CommonModule, ReactiveFormsModule, DatePipe],
  templateUrl: './tenant-maintenance.component.html',
  styleUrls: ['./tenant-maintenance.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class TenantMaintenanceComponent implements OnInit {
  private maintenanceService = inject(TenantMaintenanceService);
  private tokenService = inject(TokenService); // ✅ Injected here
  private fb = inject(NonNullableFormBuilder);

  requests = signal<MaintenanceRequest[]>([]);
  isLoading = signal<boolean>(true);
  showForm = signal<boolean>(false);
  isSubmitting = signal<boolean>(false);
  myUnits = signal<TenantUnit[]>([]);

  form = this.fb.group({
    unitID: [0, [Validators.required, Validators.min(1)]],
    tenantID: [0, Validators.required], // ✅ Kept in the form group, but we will auto-fill it
    category: ['', Validators.required],
    description: ['', Validators.required],
    priority: ['', Validators.required]
  });

  ngOnInit() {
    this.loadRequests();
    this.autoFillTenantId(); // ✅ Call our new method on load
    this.loadMyUnits(); // Load tenant's units for the dropdown
  }

  // ✅ New method to extract ID from token and patch the form
  autoFillTenantId() {
    const token = this.tokenService.getToken();
    if (token) {
      try {
        const payload = JSON.parse(atob(token.split('.')[1]));
        
        // Extract the User ID (Checks standard JWT claim keys for the ID)
        const userId = payload['nameid'] || 
                       payload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'] || 
                       payload.sub || 
                       0;

        // Silently update the form value behind the scenes
        if (userId) {
          this.form.patchValue({ tenantID: Number(userId) });
        }
      } catch (e) {
        console.error("Failed to decode token for Tenant ID", e);
      }
    }
  }

  loadRequests() {
    this.isLoading.set(true);
    this.maintenanceService.getMyRequests().subscribe({
      next: (res) => {
        this.requests.set(res);
        this.isLoading.set(false);
      },
      error: (err) => {
        console.error("Failed to load requests", err);
        this.isLoading.set(false);
      }
    });
  }

  openForm() {
    this.showForm.set(true);
  }

  closeForm() {
    this.showForm.set(false);
    // Reset form, but keep the auto-filled Tenant ID intact!
    const currentTenantId = this.form.getRawValue().tenantID;
    this.form.reset();
    this.form.patchValue({ tenantID: currentTenantId }); 
  }

  submit() {
    if (this.form.invalid) {
      alert("Please fill out all required fields.");
      return;
    }

    this.isSubmitting.set(true);

    const payload: CreateMaintenanceRequest = {
      unitID: Number(this.form.getRawValue().unitID),
      tenantID: Number(this.form.getRawValue().tenantID), // ✅ Will send the auto-filled ID
      category: this.form.getRawValue().category,
      description: this.form.getRawValue().description,
      priority: this.form.getRawValue().priority
    };

    this.maintenanceService.createRequest(payload).subscribe({
      next: (newReq) => {
        this.requests.update(current => [newReq, ...current]);
        this.isSubmitting.set(false);
        this.closeForm();
        alert("Maintenance request submitted successfully.");
      },
      error: (err) => {
        console.error("Failed to submit request", err);
        this.isSubmitting.set(false);
        alert("Failed to create request. Check console for details.");
      }
    });
  }

  loadMyUnits() {
    this.maintenanceService.getMyUnits().subscribe({
      next: (res) => this.myUnits.set(res),
      error: (err) => console.error("Failed to load units for dropdown", err)
    });
  }
}