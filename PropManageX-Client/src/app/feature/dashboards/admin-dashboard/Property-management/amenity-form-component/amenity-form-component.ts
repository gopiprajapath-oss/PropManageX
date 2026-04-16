import { CommonModule } from '@angular/common';
import { Component, inject, OnInit, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AmenityService } from '../services/amenity-service';

@Component({
  selector: 'app-amenity-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './amenity-form-component.html',
  styleUrls: ['./amenity-form-component.css']
})
export class AmenityFormComponent implements OnInit {
 
  private fb = inject(FormBuilder);
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private service = inject(AmenityService);
 
  propertyId!: number;
  amenityId!: number;
  isEditMode = false;
 
  loading = signal(false);
 
  form = this.fb.nonNullable.group({
    name: ['', Validators.required],
    description: ['']
  });
 
  ngOnInit() {
 
    this.propertyId = Number(this.route.snapshot.paramMap.get('id'));
 
    const amenityIdParam = this.route.snapshot.paramMap.get('amenityId');
 
    if (amenityIdParam) {
      this.isEditMode = true;
      this.amenityId = Number(amenityIdParam);
 
      this.service.getAmenityById(this.amenityId).subscribe({
        next: (data) => {
          this.form.patchValue({
            name: data.name,
            description: data.description
          });
        }
      });
 
    } else {
      this.isEditMode = false;
      this.form.reset({
        name: '',
        description: ''
      });
    }
  }
 
  submit() {
    if (this.form.invalid ) return;
 
    this.loading.set(true);
 
    const payload = {
      propertyId: this.propertyId,
      name: this.form.value.name,
      description: this.form.value.description
    };
 
    if (this.isEditMode) {
      this.service.updateAmenity(this.amenityId, payload).subscribe({
        next: () => {
          this.loading.set(false);
          alert('Amenity updated');
 
          this.router.navigate([
            '/admin-dashboard/properties',
            this.propertyId,
            'amenities'
          ]);
        }
      });
    } else {
      this.service.addAmenity(payload).subscribe({
        next: () => {
          this.loading.set(false);
          alert('Amenity added');
 
          this.router.navigate([
            '/admin-dashboard/properties',
            this.propertyId,
            'amenities'
          ]);
        }
      });
    }
  }
}
 