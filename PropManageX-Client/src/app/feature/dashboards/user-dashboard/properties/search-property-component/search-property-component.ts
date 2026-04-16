import { Component, ChangeDetectionStrategy, signal, inject, OnInit } from '@angular/core';
import { CommonModule, NgOptimizedImage } from '@angular/common';
import { NonNullableFormBuilder, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { Property } from '../models/property-model';
import { PropertyService } from '../property-search-service';

@Component({
  selector: 'app-search-property-component',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './search-property-component.html',
  styleUrls: ['./search-property-component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class SearchPropertyComponent implements OnInit {
  private fb = inject(NonNullableFormBuilder);
  private searchService = inject(PropertyService);
  private router = inject(Router);

  // State Signals
  properties = signal<Property[]>([]);
  isLoading = signal<boolean>(false);
  hasSearched = signal<boolean>(false);

  // Reactive Form for Search Filters
  searchForm = this.fb.group({
    query: [''],
    location: [''],
    type: [''],
  });

  // Optional: Predefined lists for dropdowns
  locations = ['Hyderabad', 'Pune', 'Chennai', 'Bangalore', 'Mumbai'];
  propertyTypes = ['Residential', 'Commercial'];

  ngOnInit() {
    // Optionally load some initial properties or leave blank until search
    this.onSearch();
  }

  onSearch() {
    this.isLoading.set(true);
    this.hasSearched.set(true);
    
    const filters = this.searchForm.getRawValue();

    this.searchService.searchProperties(filters).subscribe({
      next: (results) => {
        // Fallback placeholder image logic if none provided by API
        const mappedResults = results.map(p => ({
          ...p,
          image: p.image || 'assets/placeholder-property.jpg' 
        }));
        
        this.properties.set(mappedResults);
        this.isLoading.set(false);
      },
      error: (err) => {
        console.error('Search failed', err);
        this.properties.set([]);
        this.isLoading.set(false);
      }
    });
  }

  resetFilters() {
    this.searchForm.reset();
    this.onSearch();
  }

  goToDetails(id: number) {
    this.router.navigate(['/user-dashboard/property', id]);
  }
}