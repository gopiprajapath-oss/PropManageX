import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../../environments/environment';
import { Renewal, CreateRenewalPayload } from './models/renewal.model';

@Injectable({ providedIn: 'root' })
export class RenewalService {
  private http = inject(HttpClient);
  private readonly baseUrl = environment.apiUrl || 'https://localhost:5157/api';

  getRenewals(): Observable<Renewal[]> {
    return this.http.get<Renewal[]>(`${this.baseUrl}/renewal`);
  }

  createRenewal(data: CreateRenewalPayload) {
    // Calling the exact endpoint specified in your ManagerDashboard.txt
    return this.http.post(`${this.baseUrl}/Renewal/renewcontract`, data, { responseType: 'text' });
  }
}