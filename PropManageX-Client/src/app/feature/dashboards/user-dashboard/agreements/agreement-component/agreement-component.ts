import { Component, OnInit, ChangeDetectionStrategy, inject, signal } from '@angular/core';
import { CommonModule, DatePipe, CurrencyPipe } from '@angular/common';
import { TenantAgreementService, Agreement } from '../agreement-component/agreement-service'; // Adjust path

@Component({
  selector: 'app-tenant-agreements',
  standalone: true,
  imports: [CommonModule, DatePipe, CurrencyPipe],
  templateUrl: './agreement-component.html',
  styleUrls: ['./agreement-component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class TenantAgreementsComponent implements OnInit {
  private agreementService = inject(TenantAgreementService);

  agreements = signal<Agreement[]>([]);
  isLoading = signal<boolean>(true);
  isSigningId = signal<number | null>(null);

  ngOnInit() {
    this.loadAgreements();
  }

  loadAgreements() {
    this.isLoading.set(true);
    this.agreementService.getMyAgreements().subscribe({
      next: (res) => {
        this.agreements.set(res);
        this.isLoading.set(false);
      },
      error: (err) => {
        console.error("Failed to load agreements", err);
        this.isLoading.set(false);
      }
    });
  }

  signContract(contractId: number) {
    this.isSigningId.set(contractId);

    this.agreementService.signAgreement(contractId).subscribe({
      next: (res) => {
        // Update the status to 'Signed' locally so the UI updates instantly
        this.agreements.update(current => 
          current.map(a => 
            a.contractID === contractId 
              ? { ...a, status: 'Signed' } 
              : a
          )
        );
        this.isSigningId.set(null);
        alert(res.message || "Agreement Signed Successfully!");
      },
      error: (err) => {
        console.error("Signature failed", err);
        this.isSigningId.set(null);
        alert("Failed to sign the agreement. Please try again.");
      }
    });
  }
}