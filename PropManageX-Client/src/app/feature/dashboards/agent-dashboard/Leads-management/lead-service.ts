import { Injectable, inject } from '@angular/core';
import { HttpClient , HttpHeaders} from '@angular/common/http';
import { Lead } from './model/lead.model';
import { Observable } from 'rxjs';
import { environment } from '../../../../../environments/environment';
 
@Injectable({
  providedIn: 'root'
})
export class LeadService {
 
  private http = inject(HttpClient);
  private baseUrl = environment.apiUrl+ '/Lead';
 
 
  private getHeaders() {
    const token = localStorage.getItem('token');
 
    return {
      headers: new HttpHeaders({
        Authorization: `Bearer ${token}`
      })
    };
  }
 
  //  GET ALL
  getAllLeads(): Observable<Lead[]> {
    return this.http.get<Lead[]>(this.baseUrl, this.getHeaders());
  }
 
  //  GET BY ID
  getLeadById(id: number): Observable<Lead> {
    return this.http.get<Lead>(`${this.baseUrl}/${id}`, this.getHeaders());
  }

  //  CREATE
  addLead(lead: any): Observable<any> {
    return this.http.post(this.baseUrl, lead, this.getHeaders());
  }
 
  //  UPDATE
  updateLead(id: number, lead: any): Observable<any> {
    return this.http.put(`${this.baseUrl}/${id}`, lead, this.getHeaders());
  }
 
  //  DELETE
  // deleteLead(id: number): Observable<any> {
  //   return this.http.delete(`${this.baseUrl}/${id}`, this.getHeaders());
  // }
}
 