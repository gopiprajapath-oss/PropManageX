import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../../../environments/environment';



// Exactly matches your C# ContractDto
export interface Agreement {
  contractID: number;
  dealID: number;
  contractType: string;
  startDate: string;
  endDate: string;
  contractValue: number;
  status: string; 
}

@Injectable({ providedIn: 'root' })
export class TenantAgreementService {
  private http = inject(HttpClient);
  private apiUrl = `${environment.apiUrl}/Contract`;

  // Fetch only the logged-in user's contracts
  getMyAgreements(): Observable<Agreement[]> {
    return this.http.get<Agreement[]>(`${this.apiUrl}/my-contracts`);
  }

  // Call the Sign endpoint
  signAgreement(contractId: number): Observable<any> {
    return this.http.put(`${this.apiUrl}/sign/${contractId}`, {});
  }
}