import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { Observable } from 'rxjs';
import { InvoiceDto } from './invoice.service';

@Injectable({ providedIn: 'root' })
export class CollectionService {
  private http = inject(HttpClient);
  // Make sure this points to your standard invoice controller
  private apiUrl = `${environment.apiUrl}/Invoice`;

  getPendingCollections(): Observable<InvoiceDto[]> {
    // Just call the base URL to get ALL invoices, and let the component filter them
    return this.http.get<InvoiceDto[]>(this.apiUrl);
  }

  recordPayment(invoiceID: number): Observable<InvoiceDto> {
    // This perfectly matches your C# UpdateInvoiceDto!
    const updatePayload = { status: 'Closed' }; 
    
    return this.http.put<InvoiceDto>(`${this.apiUrl}/${invoiceID}`, updatePayload);
  }
}