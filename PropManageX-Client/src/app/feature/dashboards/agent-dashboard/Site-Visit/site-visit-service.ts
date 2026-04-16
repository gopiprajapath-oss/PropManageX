import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { SiteVisit } from './model/site-visit-model';
import { environment } from '../../../../../environments/environment';

 
@Injectable({
  providedIn: 'root'
})
export class SiteVisitService {
 
  private http = inject(HttpClient);
  private baseUrl = environment.apiUrl + '/SiteVisit';
 
  getAllVisits(): Observable<SiteVisit[]> {
    return this.http.get<SiteVisit[]>(this.baseUrl);
  }
 
  getVisitById(id: number): Observable<SiteVisit> {
    return this.http.get<SiteVisit>(`${this.baseUrl}/${id}`);
  }
 
  addVisit(data: any): Observable<any> {
    return this.http.post(this.baseUrl, data);
  }
 
  updateVisit(id: number, data: any): Observable<any> {
    return this.http.put(`${this.baseUrl}/${id}`, data);
  }
 
  deleteVisit(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/${id}`, {responseType:'text'});
  }
}
 