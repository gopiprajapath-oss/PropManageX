import { Component, ChangeDetectionStrategy, signal, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { RenewalService } from '../renewal-service';

@Component({
  selector: 'app-renewal-form',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './renewal-form-component.html',
  styleUrls: ['./renewal-form-component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class RenewalFormComponent implements OnInit {
  private fb = inject(FormBuilder);
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private renewalService = inject(RenewalService);

  targetContractId = signal<number | null>(null);
  isLoading = signal(false);
  error = signal<string | null>(null);

 form = this.fb.group({
    newEndDate: ['', Validators.required],
    proposedValue: [null as number | null, [Validators.required, Validators.min(1)]] // Added proposed value
  });

  ngOnInit(): void {
    // Capture the contract ID passed from the Contract List route
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.targetContractId.set(Number(id));
    } else {
      this.error.set('No Contract ID provided for renewal.');
    }
  }

  processRenewal(): void {
    if (this.form.invalid || !this.targetContractId()) {
      this.form.markAllAsTouched();
      return;
    }
    
    this.isLoading.set(true);
    const payload = {
  contractID: this.targetContractId()!,
  proposedEndDate: new Date(this.form.value.newEndDate!).toISOString(), // ISO format
  proposedValue: Number(this.form.value.proposedValue)
};

    this.renewalService.createRenewal(payload).subscribe({
      next: () => {
        this.isLoading.set(false);
        // After successful renewal, navigate to the renewals list
        this.router.navigate(['/manager-dashboard/renewals']);
      },
      error: (err) => {
        console.error('Renewal Error', err);
        this.error.set('Failed to process renewal. Please ensure the new date is valid.');
        this.isLoading.set(false);
      }
    });
  }

  cancel(): void {
    // Route back to the contracts list if they cancel
    this.router.navigate(['/manager-dashboard/contracts']);
  }
}