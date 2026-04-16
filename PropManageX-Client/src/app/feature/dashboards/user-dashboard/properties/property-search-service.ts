import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Property, PropertySearchFilters } from './models/property-model';
import { environment } from '../../../../../environments/environment';
// import { environment } from '../../../environments/environment';

@Injectable({ providedIn: 'root' })
export class PropertyService {
  private http = inject(HttpClient);
  
  // Replace with environment.apiUrl in your actual app
  private apiUrl = `${environment.apiUrl}/Property`;

  searchProperties(filters: Partial<PropertySearchFilters>): Observable<Property[]> {
    let params = new HttpParams();

    // Dynamically append parameters only if they have a value
    if (filters.query) params = params.set('q', filters.query);
    if (filters.location) params = params.set('location', filters.location);
    if (filters.type) params = params.set('type', filters.type);

    // If your API supports a dedicated search endpoint, use `${this.apiUrl}/search`
    // Otherwise, assuming your GET endpoint accepts these query parameters:
    return this.http.get<Property[]>(`${this.apiUrl}/search`, { params });
  }


  getPropertyById(id: number): Observable<Property> {
      return this.http.get<Property>(`${this.apiUrl}/${id}`);
    }
}