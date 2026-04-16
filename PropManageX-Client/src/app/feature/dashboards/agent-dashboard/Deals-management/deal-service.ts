import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Deal, DealPayload } from './models/deal.model';
import { environment } from '../../../../../environments/environment';
 
@Injectable({
  providedIn: 'root'
})
export class DealService {
 
  private http = inject(HttpClient);
  private baseUrl = environment.apiUrl + '/deal';
 
  getAllDeals(): Observable<Deal[]> {
    return this.http.get<Deal[]>(this.baseUrl);
  }
 
  getDealById(id: number): Observable<Deal> {
    return this.http.get<Deal>(`${this.baseUrl}/${id}`);
  }
 
  addDeal(payload: DealPayload) {
    const token = localStorage.getItem('token');
 
    return this.http.post(this.baseUrl, payload, {
      headers: { Authorization: `Bearer ${token}` }
    });
  }
 
  updateDeal(id: number, payload: DealPayload) {
    const token = localStorage.getItem('token');
 
    return this.http.put(`${this.baseUrl}/${id}`, payload, {
      headers: { Authorization: `Bearer ${token}` }
    });
  }
}
 