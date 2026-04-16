import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { SiteVisitService } from '../site-visit-service';
 
@Component({
  selector: 'app-site-visit-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './site-visit-form-component.html',
  styleUrls: ['./site-visit-form-component.css']
})
export class SiteVisitFormComponent implements OnInit {
 
  private fb = inject(FormBuilder);
  private service = inject(SiteVisitService);
  private router = inject(Router);
  private route = inject(ActivatedRoute);
 
  isEditMode = false;
  visitId!: number;
 
  form = this.fb.group({
    leadID: [0, Validators.required],
    visitDate: ['', Validators.required],
    agentID: [0, Validators.required],
    notes: ['']
  });
 
  ngOnInit() {
    this.visitId = Number(this.route.snapshot.paramMap.get('id'));
 
    if (this.visitId) {
      this.isEditMode = true;
 
      this.service.getVisitById(this.visitId).subscribe(res => {
        this.form.patchValue(res);
      });
    }
  }
 
  submit() {
    if (this.form.invalid) return;
 
    const payload = this.form.value;
 
    if (this.isEditMode) {
      this.service.updateVisit(this.visitId, payload).subscribe(() => {
        this.router.navigate(['/agent-dashboard/site-visits']);
      });
    } else {
      this.service.addVisit(payload).subscribe(() => {
        this.router.navigate(['/agent-dashboard/site-visits']);
      });
    }
  }
}
 