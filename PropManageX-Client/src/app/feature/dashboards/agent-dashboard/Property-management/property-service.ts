import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../../environments/environment';
import { Observable } from 'rxjs';
import { Property } from './model/property.model';
 
@Injectable({
  providedIn: 'root'
})
export class PropertyService {
 
  private http = inject(HttpClient);
  private apiUrl = `${environment.apiUrl}/Property`;
 
  getAllProperties(): Observable<Property[]> {
  const token = localStorage.getItem('token');
 
  return this.http.get<Property[]>(this.apiUrl, {
    headers: {
      Authorization: `Bearer ${token}`
    }
  });
}
 

  // addProperty(data: Property): Observable<Property> {
  //   return this.http.post<Property>(`${this.apiUrl}/create-property`,data);
  // }

  getPropertyById(id: number) {
  return this.http.get<Property>(`${this.apiUrl}/${id}`);
}
 
  updateProperty(id: number, data: Property) {
  return this.http.put(`${this.apiUrl}/${id}`, data);
}
 

}
 