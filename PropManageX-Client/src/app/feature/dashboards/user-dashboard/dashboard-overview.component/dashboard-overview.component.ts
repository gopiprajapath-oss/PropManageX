import { Component, OnInit, ChangeDetectionStrategy, inject, signal } from '@angular/core';
import { CommonModule, CurrencyPipe, DatePipe } from '@angular/common';
import { RouterModule } from '@angular/router';
import { TokenService } from '../../../../core/services/token.service';
import { TenantOverviewService, TenantOverview } from '../../../dashboards/user-dashboard/dashboard-overview.component/tenant-overview.service';

@Component({
  selector: 'app-dashboard-overview',
  imports: [CommonModule, RouterModule, CurrencyPipe, DatePipe],
  templateUrl: './dashboard-overview.component.html',
  styleUrls: ['./dashboard-overview.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class DashboardOverviewComponent implements OnInit {
  private tokenService = inject(TokenService);
  private overviewService = inject(TenantOverviewService);

  // Identity Signals
  userName = signal<string>('Guest');
  role = signal<string>('User');

  // Real Backend Data Signals
  overviewData = signal<TenantOverview | null>(null);
  isLoading = signal<boolean>(true);

  ngOnInit() {
    const user = this.tokenService.getUser();
    if (user) {
      if (user.name) this.userName.set(user.name);
      if (user.role) this.role.set(user.role);
    }

    if (this.role() === 'Tenant' || this.role() === 'Buyer') {
      this.loadOverview();
    } else {
      this.isLoading.set(false);
    }
  }

  loadOverview() {
    this.overviewService.getMyOverview().subscribe({
      next: (data) => {
        this.overviewData.set(data);
        this.isLoading.set(false);
      },
      error: (err) => {
        console.error("Failed to load overview data", err);
        this.isLoading.set(false);
      }
    });
  }
}

