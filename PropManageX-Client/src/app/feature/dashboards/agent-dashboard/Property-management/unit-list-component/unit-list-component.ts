import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, ActivatedRoute } from '@angular/router';
import { UnitService } from '../unit-service'; // adjust path
import { Unit } from '../model/unit.model' ;
import { ChangeDetectorRef } from '@angular/core';
 
@Component({
  selector: 'app-unit-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './unit-list-component.html',
  styleUrls: ['./unit-list-component.css']
})
export class UnitListComponent implements OnInit {
 
  private route = inject(ActivatedRoute);
  private unitService = inject(UnitService);
  private cdr = inject(ChangeDetectorRef);
 
  units: Unit[]=[];
  propertyId!: number;
 
  ngOnInit() {
    this.propertyId = Number(this.route.snapshot.paramMap.get('id'));
 
    console.log("PROPERTY ID:", this.propertyId); // 🔥 DEBUG
 
    this.loadUnits();
  }
 
  loadUnits() {
    this.unitService.getUnitsByProperty(this.propertyId).subscribe({
      next: (res) => {
        console.log("UNITS DATA:", res); 
        this.units=res;
        console.log("FILTERED DATA:", this.units);
 
        this.cdr.detectChanges();
      },
      error: (err) => {
        console.error("ERROR:", err);
      }
    });
  }
}
 