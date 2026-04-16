import { Component, signal, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { ContractService } from '../contract.service';
import { Contract } from '../models/contract.model';

@Component({
  selector: 'app-contract-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './contract-list-component.html',
  styleUrls: ['./contract-list-component.css']
})
export class ContractListComponent {

  private service = inject(ContractService);
  private router = inject(Router);

  contracts = signal<Contract[]>([]);
  loading = signal(true);

  constructor() {
    this.loadContracts();
  }

  loadContracts() {
    this.service.getAllContracts().subscribe({
      next: (data) => {
        this.contracts.set(
          data.map((c: any) => ({
            ContractID: c.contractID,
            dealID: c.dealID,
            contractType: c.contractType,
            startDate: c.startDate,
            endDate: c.endDate,
            contractValue: c.contractValue,
            status: c.status
          }))
        );
        this.loading.set(false);
      },
      error: (err) => {
        console.error('Error:', err);
        this.loading.set(false);
      }
    });
  }

  addContract() {
    this.router.navigate(['/agent-dashboard/add-contract']);
  }

  editContract(id: number) {
    this.router.navigate(['/agent-dashboard/edit-contract', id]);
  }

  deleteContract(id: number) {
    if (confirm('Delete this contract?')) {
      this.service.deleteContract(id).subscribe(() => {
        this.loadContracts();
      });
    }
  }
}