import { Component, inject, OnInit, signal } from '@angular/core';
import { FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { DealService } from '../deal-service';
import { Router, ActivatedRoute } from '@angular/router';
 
@Component({
  selector: 'app-deal-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './deal-form-component.html',
  styleUrls: ['./deal-form-component.css']
})
export class DealFormComponent implements OnInit {
 
  private fb = inject(FormBuilder);
  private dealService = inject(DealService);
  private router = inject(Router);
  private route = inject(ActivatedRoute);
 
  isEditMode = false;
  dealId!: number;
 
  loading = signal(false);
 
  form = this.fb.nonNullable.group({
    leadID: [0, Validators.required],
    unitID: [0, Validators.required],
    dealType: ['', Validators.required],
    agreedValue: [0, Validators.required],
    expectedClosureDate: ['', Validators.required],
    status: ['Open']
  });
 
  ngOnInit() {
    this.dealId = Number(this.route.snapshot.paramMap.get('id'));
 
    if (this.dealId) {
      this.isEditMode = true;
 
      this.dealService.getDealById(this.dealId).subscribe({
        next: (data) => {
          this.form.patchValue(data);
 
          // 🔥 Restrict fields (ONLY STATUS editable)
          this.form.get('leadID')?.disable();
          this.form.get('unitID')?.disable();
          this.form.get('dealType')?.disable();
          this.form.get('agreedValue')?.disable();
          this.form.get('expectedClosureDate')?.disable();
        }
      });
    }
  }
 
  submit() {
    if (this.form.invalid) return;
 
    this.loading.set(true);
 
    const value = this.form.getRawValue();
 
    const payload = {
      leadID: Number(value.leadID),
      unitID: Number(value.unitID),
      dealType: value.dealType,
      agreedValue: Number(value.agreedValue),
      expectedClosureDate: value.expectedClosureDate,
      status: value.status
    };
 
    if (this.isEditMode) {
      this.dealService.updateDeal(this.dealId, payload).subscribe({
        next: () => {
          this.loading.set(false);
          this.router.navigate(['/agent-dashboard/deals']);
        },
        error: () => this.loading.set(false)
      });
    } else {
      this.dealService.addDeal(payload).subscribe({
        next: () => {
          this.loading.set(false);
          this.router.navigate(['/agent-dashboard/deals']);
        },
        error: () => this.loading.set(false)
      });
    }
  }
}
 