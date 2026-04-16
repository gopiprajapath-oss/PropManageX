import { CommonModule } from '@angular/common';
import { Component, inject, OnInit, signal } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { AmenityService } from '../services/amenity-service';

@Component({
  selector: 'app-amenity-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './amenity-list-component.html',
  styleUrls: ['./amenity-list-component.css']
})
export class AmenityListComponent implements OnInit {

  private route = inject(ActivatedRoute);
  private service = inject(AmenityService);

  propertyId!: number;

  // ✅ Signal instead of plain array
  amenities = signal<any[]>([]);

  ngOnInit() {
    this.propertyId = Number(this.route.snapshot.paramMap.get('id'));
    this.loadAmenities();
  }

  loadAmenities() {
    this.service.getAmenitiesByProperty(this.propertyId).subscribe({
      next: (res) => {
        this.amenities.set(Array.isArray(res) ? res : [res]);
      }
    });
  }

  deleteAmenity(id: number) {
    if (confirm('Delete this amenity?')) {
      this.service.deleteAmenity(id).subscribe(() => {
        this.loadAmenities();
      });
    }
  }
}
