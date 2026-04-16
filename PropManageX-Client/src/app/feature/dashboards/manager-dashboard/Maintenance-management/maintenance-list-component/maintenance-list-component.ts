import { Component, ChangeDetectionStrategy, signal, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Router } from '@angular/router';
import { MaintenanceService } from '../maintenance-service';
import { MaintenanceRequestDto } from '../models/maintenance.dto';

@Component({
  selector: 'app-maintenance-list',
  imports: [CommonModule, RouterModule],
  templateUrl: './maintenance-list-component.html',
  styleUrls: ['./maintenance-list-component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class MaintenanceListComponent implements OnInit {
  private maintenanceService = inject(MaintenanceService);
  private router = inject(Router);

  maintenanceRequests = signal<MaintenanceRequestDto[]>([]);
  isLoading = signal(true);
  error = signal<string | null>(null);

  ngOnInit(): void {
    this.loadRequests();
  }

  loadRequests(): void {
    this.isLoading.set(true);
    this.maintenanceService.getMaintenanceRequests().subscribe({
      next: (data) => {
        this.maintenanceRequests.set(data);
        this.isLoading.set(false);
      },
      error: () => {
        this.error.set('Failed to load maintenance requests');
        this.isLoading.set(false);
      }
    });
  }

  startAssignment(req: MaintenanceRequestDto): void {
    this.router.navigate(['/manager-dashboard/vendors/dispatch', req.requestID]);
  }

  deleteMaintenance(req: MaintenanceRequestDto): void {
    if (!confirm(`Delete maintenance request #${req.requestID}?`)) return;
    
    this.maintenanceService.deleteMaintenanceRequest(req.requestID).subscribe({
      next: () => this.loadRequests(),
      error: () => this.error.set('Failed to delete maintenance request')
    });
  }
}