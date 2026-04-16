import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../../environments/environment';

export interface TenantOverview {
  contractID: number;
  propertyName: string;
  location: string;
  unitNumber: number;
  contractType: string;
  contractValue: number;
  startDate: string;
  endDate: string;
  isSigned: boolean;
}

@Injectable({ providedIn: 'root' })
export class TenantOverviewService {
  private http = inject(HttpClient);
  private apiUrl = `${environment.apiUrl}/Contract`;

  getMyOverview(): Observable<TenantOverview | null> {
    return this.http.get<TenantOverview | null>(`${this.apiUrl}/my-overview`);
  }
}