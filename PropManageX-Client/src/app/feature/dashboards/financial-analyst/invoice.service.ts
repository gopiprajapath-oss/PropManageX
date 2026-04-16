import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { Observable } from 'rxjs';

export interface InvoiceDto {
  invoiceID: number; 
  contractID: number;
  period: string;
  amount: number;
  dueDate: string;
  status: string;
}

export interface UpdateInvoiceDto {
  status: string;
}

@Injectable({ providedIn: 'root' })
export class InvoiceService {
  private http = inject(HttpClient);
  private apiUrl = `${environment.apiUrl}/Invoice`;

  getInvoices(): Observable<InvoiceDto[]> {
    return this.http.get<InvoiceDto[]>(this.apiUrl);
  }

  createInvoice(invoice: Omit<InvoiceDto, 'invoiceID'>): Observable<InvoiceDto> {
    return this.http.post<InvoiceDto>(this.apiUrl, invoice);
  }

  updateStatus(id: number, status: string): Observable<InvoiceDto> {
    return this.http.put<InvoiceDto>(`${this.apiUrl}/${id}`, { status });
  }

  deleteInvoice(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`, { responseType: 'text' as 'json' });
  }
}