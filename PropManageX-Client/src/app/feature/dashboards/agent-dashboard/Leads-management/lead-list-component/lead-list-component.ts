import { Component, signal, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LeadService } from '../lead-service';
import { Lead } from '../model/lead.model';
import { RouterModule } from '@angular/router';
 
@Component({
  selector: 'app-lead-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './lead-list-component.html',
  styleUrls: ['./lead-list-component.css']
})
export class LeadListComponent {
 
  private leadService = inject(LeadService);
 
  leads = signal<Lead[]>([]);
  loading = signal(true);
 
  constructor() {
    this.loadLeads();
  }
 
  loadLeads() {
    this.leadService.getAllLeads().subscribe({
      next: (res) => {
        this.leads.set(res);
        this.loading.set(false);
      },
      error: () => {
        this.loading.set(false);
      }
    });
  }
} 