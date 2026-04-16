import { Component, OnInit, inject, signal } from '@angular/core';
import { CommonModule, Location } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { LeadService } from '../../../agent-dashboard/Leads-management/lead-service';
import { TokenService } from '../../../../../core/services/token.service';
import { CreateLeadDto } from '../models/lead.-model';

@Component({
  selector: 'app-enquiry-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './enquiry-form-component.html',
  styleUrls: ['./enquiry-form-component.css']
})
export class EnquiryFormComponent implements OnInit {
  private fb = inject(FormBuilder);
  private route = inject(ActivatedRoute);
  private location = inject(Location);
  private leadService = inject(LeadService);
  private tokenService = inject(TokenService);

  enquiryForm!: FormGroup;
  isSubmitting = signal(false);

  ngOnInit() {
    const propertyId = Number(this.route.snapshot.paramMap.get('id'));
    const user = this.tokenService.getUser();

    this.enquiryForm = this.fb.group({
      propertyID: [propertyId, Validators.required],
      customerName: [user?.name || '', Validators.required],
      contactInfo: [user?.email || '', [Validators.required, Validators.email]], // Gmail mapping
      interestType: ['', Validators.required]
    });
  }

  goBack() {
    this.location.back();
  }

  onSubmit() {
    if (this.enquiryForm.invalid) {
      this.enquiryForm.markAllAsTouched();
      return;
    }

    this.isSubmitting.set(true);
    const payload: CreateLeadDto = this.enquiryForm.value;

    this.leadService.addLead(payload).subscribe({
      next: () => {
        alert("Enquiry sent successfully!");
        this.isSubmitting.set(false);
        this.goBack();
      },
      error: (err) => {
        console.error(err);
        this.isSubmitting.set(false);
      }
    });
  }
}