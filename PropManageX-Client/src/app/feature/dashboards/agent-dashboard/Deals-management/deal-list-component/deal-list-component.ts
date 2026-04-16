import { Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DealService } from '../deal-service';
import { Deal } from '../models/deal.model';
import { RouterModule } from '@angular/router';
 
@Component({
  selector: 'app-deal-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './deal-list-component.html',
  styleUrls: ['./deal-list-component.css']
})
export class DealListComponent {
 
  private dealService = inject(DealService);
 
  deals = signal<Deal[]>([]);
  loading = signal(true);
 
  ngOnInit() {
    this.loadDeals();
  }
 
  loadDeals() {
    this.dealService.getAllDeals().subscribe({
      next: (res) => {
        console.log('Deals loaded:', res);
        this.deals.set(res);
        this.loading.set(false);
      },
      error: () => this.loading.set(false)
    });
  }
}
 