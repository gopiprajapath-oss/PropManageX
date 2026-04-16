import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Unit } from '../models/unit-model';
import { environment } from '../../../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AmenityService {
 
  private http = inject(HttpClient);
  private apiUrl = `${environment.apiUrl}/Amenity`;
 
  getAmenitiesByProperty(propertyId: number) {
    return this.http.get<any[]>(`${this.apiUrl}/property/${propertyId}`);
  }
 
  getAmenityById(id: number) {
    return this.http.get<any>(`${this.apiUrl}/${id}`);
  }
 
  addAmenity(data: any) {
    return this.http.post(this.apiUrl, data);
  }
 
  updateAmenity(id: number, data: any) {
    return this.http.put(`${this.apiUrl}/${id}`, data);
  }
 
  deleteAmenity(id: number) {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
 