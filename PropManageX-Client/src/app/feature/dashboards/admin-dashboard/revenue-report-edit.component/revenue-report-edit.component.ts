import { ChangeDetectionStrategy, Component, inject, Input, OnInit, output, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AdminRevenueService, RevenueReportDto } from '../admin-user-component/admin-revenue-service';
@Component({
  selector: 'app-revenue-report-edit',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './revenue-report-edit.component.html',
  styleUrl: './revenue-report-edit.component.css', // 🚨 Reusing the create CSS!
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class RevenueReportEditComponent implements OnInit {
  private revenueService = inject(AdminRevenueService);
  @Input({ required: true }) report!: RevenueReportDto;
  
  saved = output<void>();
  canceled = output<void>();

  isSaving = signal(false);
// Form Model
  scope = '';
  yieldRate = '';
  collectionRate = '';
  occupancyRate = '';
  ngOnInit() {
    // Populate form with existing data
    this.scope = this.report.scope;
    try {
      const parsed = JSON.parse(this.report.metrics);
      this.yieldRate = parsed.RentalYield ? parsed.RentalYield.replace('%', '') : '0';
      this.collectionRate = parsed.CollectionRate ? parsed.CollectionRate.replace('%', '') : '0';
      this.occupancyRate = parsed.OccupancyRate ? parsed.OccupancyRate.replace('%', '') : '0';
    } catch { }
  }

  submitForm() {
    this.isSaving.set(true);
    const metricsPayload = JSON.stringify({
      RentalYield: `${this.yieldRate}%`,
      CollectionRate: `${this.collectionRate}%`,
      OccupancyRate: `${this.occupancyRate}%`
    });
    const updatePayload = {
      scope: this.scope,
      metrics: metricsPayload
    };
    this.revenueService.updateReport(this.report.reportID, updatePayload).subscribe({
      next: () => {
        this.isSaving.set(false);
        this.saved.emit();
      },
      error: () => {
        alert('Failed to update report.');
        this.isSaving.set(false);
      }
    });
}
}