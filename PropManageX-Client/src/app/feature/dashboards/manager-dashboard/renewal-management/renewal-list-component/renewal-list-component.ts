import { Component, ChangeDetectionStrategy, signal, inject, OnInit } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common'; // NgClass is inside CommonModule
import { RenewalService } from '../renewal-service';
import { Renewal } from '../models/renewal.model';

@Component({
  selector: 'app-renewal-list',
  standalone: true, // Ensure standalone is true if not using NgModules
  imports: [CommonModule, DatePipe],
  templateUrl: './renewal-list-component.html',
  styleUrls: ['./renewal-list-component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class RenewalListComponent implements OnInit {
  private renewalService = inject(RenewalService);

  renewals = signal<Renewal[]>([]);
  isLoading = signal(true);
  error = signal<string | null>(null);

  ngOnInit(): void {
    this.loadRenewals();
  }

  loadRenewals(): void {
    this.isLoading.set(true);
    this.renewalService.getRenewals().subscribe({
      next: (data) => {
        // Ensure data is an array before setting
        if (Array.isArray(data)) {
          this.renewals.set(data);
        } else {
          console.error('Data received is not an array:', data);
          this.renewals.set([]);
        }
        this.isLoading.set(false);
      },
      error: (err) => {
        console.error('API Error:', err);
        this.error.set('Failed to load renewal history.');
        this.isLoading.set(false);
      }
    });
  }
}