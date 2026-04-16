import { Component, ChangeDetectionStrategy, signal, inject, OnInit } from '@angular/core';
import { CommonModule, CurrencyPipe, DatePipe } from '@angular/common';
import { Router } from '@angular/router';
import { ContractService } from '../contract-service';
import { Contract } from '../models/contract.model';

@Component({
  selector: 'app-contract-list',
  imports: [CommonModule, CurrencyPipe, DatePipe],
  templateUrl: './contract-list-component.html',
  styleUrls: ['./contract-list-component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ContractListComponent implements OnInit {
  private contractService = inject(ContractService);
  private router = inject(Router);

  contracts = signal<Contract[]>([]);
  isLoading = signal(true);
  error = signal<string | null>(null);

  ngOnInit(): void {
    this.loadContracts();
  }

  loadContracts(): void {
    this.isLoading.set(true);
    this.contractService.getContracts().subscribe({
      next: (data) => {
        this.contracts.set(data);
        this.isLoading.set(false);
      },
      error: (err) => {
        console.error('API Error:', err);
        this.error.set('Failed to load contract information.');
        this.isLoading.set(false);
      }
    });
  }

  startRenewal(contract: Contract): void {
    // Navigates to the renewal creation form, passing the contractID
    this.router.navigate(['/manager-dashboard/renewals/edit', contract.contractID]);
  }
}