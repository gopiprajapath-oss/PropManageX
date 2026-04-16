import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, ActivatedRoute , Router } from '@angular/router';
import { UnitService } from '../services/unit-service'; // adjust path
import { Unit } from '../models/unit-model' ;
import { ChangeDetectorRef } from '@angular/core';
 
@Component({
  selector: 'app-unit-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './unit-list-component.html',
  styleUrls: ['./unit-list-component.css']
})
export class UnitListComponent implements OnInit {
 
  private router = inject(Router);
  private route = inject(ActivatedRoute);
  private unitService = inject(UnitService);
  private cdr = inject(ChangeDetectorRef);
 
  units: Unit[]=[];
  propertyID!: number;
 
  ngOnInit() {
    this.propertyID = Number(this.route.snapshot.paramMap.get('id'));
 
    console.log("PROPERTY ID:", this.propertyID); //  DEBUG
 
    this.loadUnits();
  }

  
 
  loadUnits() {
    this.unitService.getUnitsByProperty(this.propertyID).subscribe({
      next: (res) => {
        this.units=res;
 
        this.cdr.detectChanges();
      },
      error: (err) => {
        console.error("ERROR:", err);
      }
    });
  }
deleteUnit(id: number) {
    if (confirm('Are you sure you want to delete?')) {
      this.unitService.deleteUnit(id).subscribe(() => {
        this.loadUnits();
        this.router.navigate([
            '/admin-dashboard/properties',
            this.propertyID ,'/units'
          ]);
      });
    }
  }

}
 