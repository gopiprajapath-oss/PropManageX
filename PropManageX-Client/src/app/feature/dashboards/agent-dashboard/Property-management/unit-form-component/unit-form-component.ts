import { Component, OnInit, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { UnitService } from '../unit-service';
 
@Component({
  selector: 'app-unit-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './unit-form-component.html',
  styleUrls: ['./unit-form-component.css']
})
export class UnitFormComponent implements OnInit {
 
  private fb = inject(FormBuilder);
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private service = inject(UnitService);
 
  unitId!: number;
  loading = signal(false);
 
  // ✅ FORM (ONLY STATUS EDITABLE)
  form = this.fb.nonNullable.group({
    unitNumber: [{value: 0,disabled:true}],
    areaSqFt:[{value: 0,disabled:true}],
    bedroomCount:[{value: 0,disabled:true}],
    basePrice:[{value: 0,disabled:true}],
    //type: [{ value: '', disabled: true }],
    status: ['', Validators.required]
  });
 
  ngOnInit(): void {
    this.unitId = Number(this.route.snapshot.paramMap.get('id'));
 
    if (this.unitId) {
      this.service.getUnitById(this.unitId).subscribe({
        next: (data) => {
          this.form.patchValue({
            unitNumber: data.unitNumber,
            areaSqFt: data.areaSqFt,
            bedroomCount: data.bedroomCount,
            basePrice:data.basePrice,
            //type: data.type,
            status: data.status
          });
        },
        error: (err) => {
          console.error('Error loading unit:', err);
        }
      });
    }
  }
 
  submit() {
    if (this.form.invalid) return;
 
    this.loading.set(true);
 
    // ✅ IMPORTANT: getRawValue() includes disabled fields
    const updatedData = this.form.getRawValue();
 
    this.service.updateUnit(this.unitId, updatedData).subscribe({
      next: () => {
        this.loading.set(false);
        alert('Unit status updated successfully');
 
        // redirect back to units list
        this.router.navigate(['/agent-dashboard/properties']);
      },
      error: (err) => {
        this.loading.set(false);
        console.error('Update failed:', err);
      }
    });
  }
}
 