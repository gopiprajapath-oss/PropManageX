import { Component, signal, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Router } from '@angular/router';
import { SiteVisitService } from '../site-visit-service';
import { SiteVisit } from '../model/site-visit-model';

@Component({
  selector: 'app-site-visit-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './site-visit-list-component.html',
  styleUrls: ['./site-visit-list-component.css']
})
export class SiteVisitListComponent {

  private service = inject(SiteVisitService);
  private router = inject(Router);

  visits = signal<SiteVisit[]>([]);
  loading = signal(true);

  constructor() {
    this.loadVisits();
  }

  loadVisits() {
    this.service.getAllVisits().subscribe({
      next: (data) => {
        this.visits.set(data);
        this.loading.set(false);
      },
      error: (err) => {
        console.error('Error:', err);
        this.loading.set(false);
      }
    });
  }

  addVisit() {
    this.router.navigate(['/agent-dashboard/add-site-visit']);
  }

  editVisit(id: number) {
    this.router.navigate(['/agent-dashboard/edit-site-visit', id]);
  }

  deleteVisit(id: number) {
    if (confirm('Are you sure?')) {
      this.service.deleteVisit(id).subscribe(() => {
        this.loadVisits();
      });
    }
  }
}