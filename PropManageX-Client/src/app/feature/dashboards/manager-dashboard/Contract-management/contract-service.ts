import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../../environments/environment';
import { Contract } from './models/contract.model';

@Injectable({ providedIn: 'root' })
export class ContractService {
  private http = inject(HttpClient);
  private readonly baseUrl = environment.apiUrl || 'https://localhost:5157/api';

  getContracts(): Observable<Contract[]> {
    return this.http.get<Contract[]>(`${this.baseUrl}/contract`);
  }
}