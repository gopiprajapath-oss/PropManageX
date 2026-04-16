import { Component, OnInit, ChangeDetectionStrategy, inject, signal } from '@angular/core';
import { CommonModule, Location } from '@angular/common';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { PropertyService } from '../property-search-service';

@Component({
  selector: 'app-property-details',
  imports: [CommonModule, RouterLink],
  templateUrl: './property-details-component.html',
  styleUrls: ['./property-details-component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class PropertyDetailsComponent implements OnInit {

  // Dependency Injection
  private route = inject(ActivatedRoute);
  private location = inject(Location);
  private propertyService = inject(PropertyService);

  // Signals for State Management
  property = signal<any>(null);
  isLoading = signal<boolean>(true);

  ngOnInit() {
    const propertyId = Number(this.route.snapshot.paramMap.get('id'));
    
    if (propertyId) {
      this.propertyService.getPropertyById(propertyId).subscribe({
        next: (res) => {
          const propertyData = {
            ...res,
            image: res.image || 'https://images.unsplash.com/photo-1560184897-ae75f418493e'
          };
          this.property.set(propertyData);
          this.isLoading.set(false);
        },
        error: (err) => {
          console.error("Failed to load property", err);
          this.isLoading.set(false);
        }
      });
    }
  }








  goBack() {
    this.location.back();
  }
}