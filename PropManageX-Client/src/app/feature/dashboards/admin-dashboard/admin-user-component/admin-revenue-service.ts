import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../../environments/environment';
import { Observable } from 'rxjs';

export interface RevenueReportDto {
  reportID: number;
  scope: string; // e.g., "Institutional"
  metrics: string; // JSON string from DB containing OccupancyRate, RentalYield, etc.
  generatedDate: string; // ISO Date String
}

@Injectable({
  providedIn: 'root'
})
export class AdminRevenueService {
  private http = inject(HttpClient);
  // Adjust this URL based on your actual backend RevenueReportController
  private apiUrl = `${environment.apiUrl}/RevenueReport`;

  // Fetches only Institutional reports for the Admin
  getInstitutionalReports(): Observable<RevenueReportDto[]> {
    return this.http.get<RevenueReportDto[]>(this.apiUrl);
  }

  // Tells the backend to calculate and save a new global snapshot
  generateInstitutionalReport(): Observable<RevenueReportDto> {
    return this.http.post<RevenueReportDto>(this.apiUrl, {});
  }

  generateReport(payload: any): Observable<any> {
    return this.http.post(this.apiUrl, payload);
  }

  updateReport(id: number, payload: any): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, payload);
  }

  deleteReport(id: number): Observable<any> {
    // Using responseType 'text' to prevent JSON parse errors if backend returns a raw boolean/200 OK
    return this.http.delete(`${this.apiUrl}/${id}`, { responseType: 'text' as 'json' });
  }
}