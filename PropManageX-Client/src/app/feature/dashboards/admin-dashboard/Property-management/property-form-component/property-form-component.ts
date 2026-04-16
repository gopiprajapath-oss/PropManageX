import { Component, OnInit } from '@angular/core';
import { ReactiveFormsModule, FormBuilder, Validators, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { PropertyService } from '../services/property-service';
import { CommonModule } from '@angular/common';
 
@Component({
  selector: 'app-property-form',
   imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './property-form-component.html',
  styleUrls: ['./property-form-component.css']
})
export class PropertyFormComponent implements OnInit {
 
  form!: FormGroup;
  isEdit = false;
  propertyId!: number;
 
  constructor(
    private fb: FormBuilder,
    private propertyService: PropertyService,
    private route: ActivatedRoute,
    private router: Router
  ) {}
 
  ngOnInit(): void {
 
    // ✅ Create Form
    this.form = this.fb.group({
      name: ['', Validators.required],
      type: ['', Validators.required],
      location: ['', Validators.required],
      totalUnits: [0, Validators.required],
      status: ['', Validators.required]
    });
 
    // ✅ Check Edit Mode
    const id = this.route.snapshot.params['id'];
 
    if (id) {
      this.isEdit = true;
      this.propertyId = +id;
 
      this.propertyService.getPropertyById(id).subscribe(data => {
        this.form.patchValue({
          name: data.name,
          type: data.type.charAt(0).toUpperCase() + data.type.slice(1).toLowerCase(), // "Residential"
          location: data.location,
          totalUnits: data.totalUnits,
          status: data.status

        });
      });
    }
  }
 
  // ✅ Submit
  onSubmit() {
 
    if (this.form.invalid) return;
 
    const payload = {
      Name: this.form.value.name,
      Type: this.form.value.type,
      Location: this.form.value.location,
      TotalUnits: this.form.value.totalUnits,
      Status: this.form.value.status
    };
 
    if (this.isEdit) {
      this.propertyService.updateProperty(this.propertyId, payload as any)
        .subscribe(() => {
          this.router.navigate(['/admin-dashboard/properties']);
        });
    } else {
      this.propertyService.createProperty(payload as any)
        .subscribe(() => {
          this.router.navigate(['/admin-dashboard/properties']);
        },
      
      );
    }
  }
}
 