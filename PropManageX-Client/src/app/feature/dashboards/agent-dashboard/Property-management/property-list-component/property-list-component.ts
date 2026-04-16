import { Component, ChangeDetectionStrategy, signal, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PropertyService } from '../property-service';
import { Property } from '../model/property.model';
 import { RouterModule } from '@angular/router';
import { Router } from '@angular/router';

@Component({
  selector: 'app-property-list',
  imports: [CommonModule,RouterModule],
  templateUrl: './property-list-component.html',
  styleUrl: './property-list-component.css',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class PropertyListComponent {
 
  private propertyService = inject(PropertyService);
  private router = inject(Router);
 
  properties = signal<Property[]>([]);
  loading = signal(true);
 
  constructor() {
    this.loadProperties();
  }

  loadProperties() {
    this.propertyService.getAllProperties().subscribe({
      next: (res) => {
        this.properties.set(res);
        this.loading.set(false);
      },
      error: (err) => {
        console.error("API ERROR:",err)
        this.loading.set(false);
      }
    });
  }
}
 