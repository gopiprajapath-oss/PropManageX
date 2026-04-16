import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Contract } from './models/contract.model';
import { environment } from '../../../../../environments/environment';
 
@Injectable({
  providedIn: 'root'
})
export class ContractService {
 
  private http = inject(HttpClient);
  private baseUrl = environment.apiUrl + '/Contract';
 
  getAllContracts(): Observable<Contract[]> {
    return this.http.get<Contract[]>(this.baseUrl);
  }
 
  getContractById(id: number): Observable<Contract> {
    return this.http.get<Contract>(`${this.baseUrl}/${id}`);
  }
 
  createContract(data: any) {
    return this.http.post(this.baseUrl, data);
  }
 
  updateContract(id: number, data: any) {
    return this.http.put(`${this.baseUrl}/${id}`, data);
  }
 
  deleteContract(id: number) {
    return this.http.delete(`${this.baseUrl}/${id}`, {
      responseType: 'text'   // 👈 same fix as SiteVisit
    });
  }
}
 