import { Component, inject, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { LeadService } from '../lead-service';
import { Router, ActivatedRoute } from '@angular/router';
 
@Component({
  selector: 'app-lead-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './lead-form-component.html',
  styleUrls: ['./lead-form-component.css']
})
export class LeadFormComponent implements OnInit {
 
  private fb = inject(FormBuilder);
  private leadService = inject(LeadService);
  private router = inject(Router);
  private route = inject(ActivatedRoute);
 
  isEditMode = false;
  leadId!: number;
 
  loading = signal(false);
 
  form = this.fb.nonNullable.group({
    propertyID: [0, Validators.required],
    customerName: ['', Validators.required],
    contactInfo: ['', Validators.required],
    interestType: ['', Validators.required],
    status: ['', Validators.required]
  });
 
  ngOnInit() {
  this.leadId = Number(this.route.snapshot.paramMap.get('id'));
 
  if (this.leadId) {
    this.isEditMode = true;
 
    this.leadService.getLeadById(this.leadId).subscribe({
      next: (data) => {
        this.form.patchValue(data);
        this.form.get('propertyID')?.disable();
        this.form.get('customerName')?.disable();
        this.form.get('contactInfo')?.disable();
        this.form.get('interestType')?.disable();
      }
    });
  }
}
 
 
  submit() {
  if (this.form.invalid) return;
 
  this.loading.set(true);
 
  const formValue = this.form.getRawValue();
 
  const payload = {
    propertyID: formValue.propertyID,
    customerName: formValue.customerName,
    contactInfo: formValue.contactInfo,
    interestType: formValue.interestType,
    status: formValue.status
  };
 
  if (this.isEditMode) {
    this.leadService.updateLead(this.leadId!, payload).subscribe({
      next: () => {
        this.loading.set(false);
        this.router.navigate(['/agent-dashboard/leads']);
      },
      error: () => this.loading.set(false)
    });
  } else {
    this.leadService.addLead(payload).subscribe({
      next: () => {
        this.loading.set(false);
        this.router.navigate(['/agent-dashboard/leads']);
      },
      error: (err) => {
        console.error(err); // 👈 ADD THIS FOR DEBUG
        this.loading.set(false);
      }
    });
  }
}
}
 