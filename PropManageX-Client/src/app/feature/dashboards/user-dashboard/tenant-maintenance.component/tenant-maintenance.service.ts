import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../../environments/environment'; // Adjust your path

export interface MaintenanceRequest {
  requestID: number;
  unitID: number;
  tenantID: number;
  category: string;
  description: string;
  priority: string;
  status: string;
  raisedDate: string;
}

export interface CreateMaintenanceRequest {
  unitID: number;
  tenantID: number;
  category: string;
  description: string;
  priority: string;
}

export interface TenantUnit {
  unitID: number;
  displayText: string;
}

@Injectable({ providedIn: 'root' })
export class TenantMaintenanceService {
  private http = inject(HttpClient);
  private apiUrl = `${environment.apiUrl}/MaintenanceRequest`;

  // 👉 Calls the secure endpoint that extracts the email from the JWT
  getMyRequests(): Observable<MaintenanceRequest[]> {
    return this.http.get<MaintenanceRequest[]>(`${this.apiUrl}/my-requests`);
  }

  createRequest(data: CreateMaintenanceRequest): Observable<MaintenanceRequest> {
    return this.http.post<MaintenanceRequest>(this.apiUrl, data);
  }

  getMyUnits(): Observable<TenantUnit[]> {
  return this.http.get<TenantUnit[]>(`${environment.apiUrl}/Unit/my-units`);
}
}