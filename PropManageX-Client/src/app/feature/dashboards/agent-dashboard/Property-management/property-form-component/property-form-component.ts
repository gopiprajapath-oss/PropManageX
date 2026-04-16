import { Component, inject, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { PropertyService } from '../property-service';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router'; 


@Component({
  selector: 'app-property-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './property-form-component.html',
  styleUrls: ['./property-form-component.css']
})
export class PropertyFormComponent implements OnInit {
 
  private route = inject(ActivatedRoute);
   private router = inject(Router);
   private fb = inject(FormBuilder);
  private propertyService = inject(PropertyService);
  isEditMode = false;
  propertyId!: number;
  
  
 
  
  loading = signal(false);
  
 
  form = this.fb.nonNullable.group({
    name: [{value:'',disabled:true}],
    type: [{value:'',disabled:true}],
    location: [{value:'',disabled:true}],
    totalUnits: [{value:0,disabled:true}],
    status: ['', Validators.required]
  });
 


ngOnInit() {
  this.propertyId = Number(this.route.snapshot.paramMap.get('id'));
 
  if (this.propertyId) {
    this.isEditMode = true;
 
    this.propertyService.getPropertyById(this.propertyId)
      .subscribe(data => {
        this.form.patchValue(data);
      });
  }
}
 
  submit() {
    if (this.form.invalid) return;
 
    this.loading.set(true);
    if (this.isEditMode) 
      {
      this.propertyService.updateProperty(this.propertyId, this.form.getRawValue())
        .subscribe(() => {
          this.loading.set(false);
          this.router.navigate(['/agent-dashboard/properties']);
        });
    } 
  //   else
  //      {
  //     this.propertyService.addProperty(this.form.getRawValue())
  //       .subscribe(() => {
  //         this.loading.set(false);
  //         this.router.navigate(['/agent-dashboard/properties']);
  //   });
  // }
}
}