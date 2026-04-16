import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../../environments/environment';
import { MaintenanceRequestDto, UpdateMaintenanceRequestDto } from './models/maintenance.dto';

@Injectable({ providedIn: 'root' })
export class MaintenanceService {
  private http = inject(HttpClient);
  private readonly baseUrl = environment.apiUrl || 'https://localhost:5157/api';

  getMaintenanceRequests(): Observable<MaintenanceRequestDto[]> {
    return this.http.get<MaintenanceRequestDto[]>(`${this.baseUrl}/MaintenanceRequest`);
  }

  updateMaintenanceStatus(requestId: number, status: string) {
    return this.http.put(
      `${this.baseUrl}/MaintenanceRequest/${requestId}`,
      { status },
      { responseType: 'text' }
    );
  }

  updateMaintenanceRequest(id: number, data: UpdateMaintenanceRequestDto) {
    return this.http.put(`${this.baseUrl}/MaintenanceRequest/${id}`, data, { responseType: 'text' });
  }

  deleteMaintenanceRequest(id: number) {
    return this.http.delete(`${this.baseUrl}/MaintenanceRequest/${id}`, { responseType: 'text' });
  }
}