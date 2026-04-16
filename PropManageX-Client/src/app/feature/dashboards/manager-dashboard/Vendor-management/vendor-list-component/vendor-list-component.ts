import { Component, ChangeDetectionStrategy, signal, inject, OnInit } from '@angular/core';
import { CommonModule, DatePipe, CurrencyPipe } from '@angular/common';
import { RouterModule, Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { VendorService } from '../vendor-service';
import { VendorAssignmentDto as VendorAssignment  } from '../models/vendor.dto';
import { MaintenanceService } from '../../Maintenance-management/maintenance-service';
import { MaintenanceRequestDto } from '../../Maintenance-management/models/maintenance.dto';

@Component({
  selector: 'app-vendor-list',
  imports: [CommonModule, RouterModule, FormsModule],
  templateUrl: './vendor-list-component.html',
  styleUrls: ['./vendor-list-component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class VendorListComponent implements OnInit {
  private vendorService = inject(VendorService);
  private maintenanceService = inject(MaintenanceService);
  private router = inject(Router);

  vendorAssignments = signal<VendorAssignment[]>([]);
  maintenanceRequests = signal<MaintenanceRequestDto[]>([]);
  isLoading = signal(true);
  error = signal<string | null>(null);

  ngOnInit(): void {
    this.loadAllData();
  }

  loadAllData(): void {
    this.isLoading.set(true);
    
    // Load dependencies for status resolution
    this.maintenanceService.getMaintenanceRequests().subscribe({
      next: (reqs) => this.maintenanceRequests.set(reqs),
      error: () => console.error('Failed to load maintenance requests for status map')
    });

    this.vendorService.getVendorAssignments().subscribe({
      next: (data) => {
        this.vendorAssignments.set(data);
        this.isLoading.set(false);
      },
      error: (err) => {
        console.error('Vendor error', err);
        this.error.set('Failed to load vendor assignments');
        this.isLoading.set(false);
      }
    });
  }

  getVendorDisplayStatus(assign: VendorAssignment): string {
    const req = this.maintenanceRequests().find(r => r.requestID === assign.requestID);
    return req?.status || 'Dispatched';
  }

  handleVendorStatusChange(assign: VendorAssignment, newStatus: string): void {
    if (newStatus === 'Completed') {
      this.vendorService.updateMaintenanceStatus(assign.requestID, 'Completed').subscribe({
        next: () => this.loadAllData(),
        error: (err) => {
          console.error('Failed to update maintenance', err);
          this.error.set('Failed to update maintenance status');
        }
      });
    }
  }

  startEditAssignment(assign: VendorAssignment): void {
    // Navigates to the shared form component for editing
    // Passing the assignmentID as a query param or route param. 
    // Assuming you set up the route: { path: 'vendors/edit/:id', component: VendorFormComponent }
    this.router.navigate(['/manager-dashboard/vendors/edit', assign.assignmentID]);
  }

  deleteAssignment(assign: VendorAssignment): void {
    if (!assign.assignmentID || !confirm(`Delete vendor assignment #${assign.assignmentID}?`)) return;
    
    this.vendorService.deleteVendorAssignment(assign.assignmentID).subscribe({
      next: () => this.loadAllData(),
      error: () => this.error.set('Failed to delete vendor assignment')
    });
  }
}