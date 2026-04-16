import { Component, OnInit, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { UnitService } from '../services/unit-service';
 
@Component({
  selector: 'app-unit-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './unit-form-component.html',
  styleUrls: ['./unit-form-component.css']
})
export class UnitFormComponent implements OnInit {
 
  // ✅ IDs
  propertyID!: number;
  unitId!: number;
 
  // ✅ MODE
  isEditMode = false;
 
  // ✅ LOADING
  loading = signal(false);
 
  // ✅ inject
  private fb = inject(FormBuilder);
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private service = inject(UnitService);
 
  // ✅ FORM
  form = this.fb.nonNullable.group({
    unitNumber: [0, Validators.required],
    areaSqFt: [0, Validators.required],
    bedroomCount: [0, Validators.required],
    basePrice: [0, Validators.required],
    status: ['', Validators.required]
  });
 
  ngOnInit(): void {
 
    // 🔥 STEP 1: GET propertyId
    this.propertyID = Number(this.route.snapshot.paramMap.get('id'));
    console.log('PROPERTY ID:', this.propertyID);
 
    // 🔥 STEP 2: GET unitId (for edit)
    const unitIdParam = this.route.snapshot.paramMap.get('unitId');
 
    if (unitIdParam) {
      // ✅ EDIT MODE
      this.isEditMode = true;
      this.unitId = Number(unitIdParam);
 
      console.log('EDIT MODE - UNIT ID:', this.unitId);
 
      // 🔥 STEP 3: CALL API
      this.service.getUnitById(this.unitId).subscribe({
        next: (data) => {
          console.log('UNIT DATA:', data);
 
          // ✅ PATCH FORM
          this.form.patchValue({
            unitNumber: data.unitNumber,
            areaSqFt: data.areaSqFt,
            bedroomCount: data.bedroomCount,
            basePrice: data.basePrice,
            status: data.status
          });
        },
        error: (err) => {
          console.error('Error loading unit:', err);
        }
      });
 
    } else {
      // ✅ ADD MODE
      this.isEditMode = false;
 
      console.log('ADD MODE');
 
      // 🔥 RESET FORM
      this.form.reset({
        unitNumber: 0,
        areaSqFt: 0,
        bedroomCount: 0,
        basePrice: 0,
        status: ''
      });
    }
  }
 
  submit() {
    if (this.form.invalid) return;
 
    this.loading.set(true);
 
    const value = this.form.getRawValue();
 
    const payload = {
      propertyID: this.propertyID,
      unitNumber: Number(value.unitNumber),
      areaSqFt: Number(value.areaSqFt),
      bedroomCount: Number(value.bedroomCount),
      basePrice: Number(value.basePrice),
      status: value.status
    };
 
    // ✅ EDIT MODE
    if (this.isEditMode) {
      this.service.updateUnit(this.unitId, payload).subscribe({
        next: () => {
          this.loading.set(false);
          alert('Unit updated successfully');
 
          //  navigate back to unit list
          this.router.navigate([
            '/admin-dashboard/properties',
            this.propertyID ,'units'
          ]);
        },
        error: (err) => {
          this.loading.set(false);
          console.error('Update failed:', err);
        }
      });
    }
 
    // ✅ ADD MODE
    else {
      this.service.addUnit(payload).subscribe({
        next: () => {
          this.loading.set(false);
          alert('Unit added successfully');
 
          this.router.navigate([
            '/admin-dashboard/properties',
            this.propertyID ,'units'
          ]);
        },
        error: (err) => {
          this.loading.set(false);
          console.error('Add failed:', err);
        }
      });
    }
  }
}
 