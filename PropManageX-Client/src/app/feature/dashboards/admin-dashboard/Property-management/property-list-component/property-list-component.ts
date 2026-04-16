import { Component, OnInit, ChangeDetectionStrategy, signal, inject } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { PropertyService } from '../services/property-service';
import { Property } from '../models/property-model';

@Component({
  selector: 'app-property-list',
  imports: [CommonModule, RouterModule],
  templateUrl: './property-list-component.html',
  styleUrls: ['./property-list-component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class PropertyListComponent implements OnInit {
  // Modern dependency injection
  private propertyService = inject(PropertyService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);

  // State managed by Signal
  properties = signal<Property[]>([]);

  ngOnInit(): void {
    this.loadProperties();
  }

  loadProperties(): void {
    this.propertyService.getAllProperties().subscribe({
      next: (data) => {
        // Update signal value using .set()
        this.properties.set(data);
      },
      error: (err) => {
        console.error('Failed to load properties', err);
      }
    });
  }

  addProperty(): void {
    this.router.navigate(['add'], { relativeTo: this.route });
  }

  editProperty(id: number): void {
    this.router.navigate(['edit', id], { relativeTo: this.route });
  }

  deleteProperty(id: number): void {
    if (confirm('Are you sure you want to delete?')) {
      this.propertyService.deleteProperty(id).subscribe({
        next: () => {
          this.loadProperties();

        },
        error: (err) => {
          console.error('Failed to delete property', err);
        }
      });
    }
  }
}