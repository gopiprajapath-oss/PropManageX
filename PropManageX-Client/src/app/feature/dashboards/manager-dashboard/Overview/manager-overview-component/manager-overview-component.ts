import { Component, ChangeDetectionStrategy, signal, inject, OnInit } from '@angular/core';
import { CommonModule, CurrencyPipe } from '@angular/common';
import { ManagerService } from '../../services/manager.service';

@Component({
  selector: 'app-manager-overview',
  imports: [CommonModule, CurrencyPipe],
  templateUrl: './manager-overview-component.html',
  styleUrls: ['./manager-overview-component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ManagerOverviewComponent implements OnInit {
  private managerService = inject(ManagerService);

  dashboardData = signal<any | null>(null);
  isLoading = signal(true);

  ngOnInit(): void {
    this.loadDashboard();
  }

  loadDashboard(): void {
    this.isLoading.set(true);
    this.managerService.getDashboardData().subscribe({
      next: (data) => {
        this.dashboardData.set(data);
        this.isLoading.set(false);
      },
      error: (err) => {
        console.error('Dashboard error', err);
        this.isLoading.set(false);
      }
    });
  }
}