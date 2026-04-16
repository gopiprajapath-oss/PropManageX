import { ChangeDetectionStrategy, Component, inject, output, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AdminRevenueService } from '../admin-user-component/admin-revenue-service';
@Component({
  selector: 'app-revenue-report-create',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './revenue-report-create.component.html',
  styleUrl: './revenue-report-create.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class RevenueReportCreateComponent {
  private revenueService = inject(AdminRevenueService);
// Outputs to tell the main table what to do
  saved = output<void>();
  canceled = output<void>();

  isSaving = signal(false);
// Form Model
  scope = '';
  yieldRate = 0;
  collectionRate = 0;
  occupancyRate = 0;
  submitForm() {
    this.isSaving.set(true);

    // Packaging metrics into a JSON string exactly how your parseMetrics function expects it
    const metricsPayload = JSON.stringify({
      RentalYield: `${this.yieldRate}%`,
      CollectionRate: `${this.collectionRate}%`,
      OccupancyRate: `${this.occupancyRate}%`
    });
    const newReport = {
      scope: this.scope,
      metrics: metricsPayload,
      generatedDate: new Date().toISOString()
    };
    this.revenueService.generateReport(newReport).subscribe({
      next: () => {
        this.isSaving.set(false);
        this.saved.emit(); // Tell parent to reload table
      },
      error: () => {
        alert('Failed to generate report.');
        this.isSaving.set(false);
      }
    });
}
}