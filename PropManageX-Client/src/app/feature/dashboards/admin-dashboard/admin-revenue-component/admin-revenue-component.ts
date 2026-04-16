import { ChangeDetectionStrategy, Component, inject, OnInit, signal, Input } from '@angular/core';
import { AdminRevenueService, RevenueReportDto } from '../admin-user-component/admin-revenue-service';
import { DatePipe } from '@angular/common';
import { RevenueReportCreateComponent } from '../revenue-report-create.component/revenue-report-create.component';
import { RevenueReportEditComponent } from '../revenue-report-edit.component/revenue-report-edit.component';

@Component({
  selector: 'app-admin-revenue',
  standalone: true,
  imports: [DatePipe, RevenueReportCreateComponent, RevenueReportEditComponent],
  templateUrl: './admin-revenue-component.html',
  styleUrl: './admin-revenue-component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
  host: { class: 'page-wrapper' }
})
export class AdminRevenueComponent implements OnInit {
  private revenueService = inject(AdminRevenueService);
  
  @Input() role: 'Admin' | 'Finance Analyst' = 'Admin';

  reports = signal<RevenueReportDto[]>([]);
  isLoading = signal<boolean>(true);
  error = signal<string | null>(null);

  // State Management to swap views
  activeView = signal<'table' | 'create' | 'edit'>('table');
  selectedReport = signal<RevenueReportDto | null>(null);

  ngOnInit(): void {
    this.fetchReports();
  }

  fetchReports(): void {
    this.isLoading.set(true);
    this.error.set(null);
    this.revenueService.getInstitutionalReports().subscribe({
      next: (data) => {
        // Sort descending so newest is on top
        const sorted = data.sort((a, b) => new Date(b.generatedDate).getTime() - new Date(a.generatedDate).getTime());
        this.reports.set(sorted);
        this.isLoading.set(false);
      },
      error: (err) => {
        this.error.set('Failed to load institutional reports.');
        this.isLoading.set(false);
      }
    });
  }

  parseMetrics(jsonString: string): { occupancy: string, yield: string, collection: string } {
    try {
      const parsed = JSON.parse(jsonString);
      return {
        occupancy: parsed.OccupancyRate || 'N/A',
        yield: parsed.RentalYield || 'N/A',
        collection: parsed.CollectionRate || 'N/A'
      };
    } catch {
      return { occupancy: 'Error', yield: 'Error', collection: 'Error' };
    }
  }

  // --- ACTIONS ---

  generateReport(): void {
    this.activeView.set('create');
  }

  editReport(report: RevenueReportDto): void {
    this.selectedReport.set(report);
    this.activeView.set('edit');
  }

  deleteReport(id: number): void {
    if (confirm('Are you sure you want to permanently delete this report?')) {
      this.revenueService.deleteReport(id).subscribe({
        next: () => {
          this.reports.update(list => list.filter(r => r.reportID !== id));
        },
        error: () => alert('Failed to delete report. Please try again.')
      });
    }
  }

  // --- FORM EVENT HANDLERS ---
  
  onFormSaved(): void {
    this.activeView.set('table');
    this.fetchReports(); 
  }

  onFormCanceled(): void {
    this.activeView.set('table');
    this.selectedReport.set(null);
  }
}