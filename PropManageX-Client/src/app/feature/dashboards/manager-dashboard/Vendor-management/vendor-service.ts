import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../../environments/environment';
import { VendorAssignmentDto, UpdateVendorAssignmentPayload, CreateVendorAssignmentDto } from './models/vendor.dto';

@Injectable({ providedIn: 'root' })
export class VendorService {
  private http = inject(HttpClient);
  private readonly baseUrl = environment.apiUrl || 'https://localhost:5157/api';

  getVendorAssignmentById(id: number): Observable<VendorAssignmentDto> {
    return this.http.get<VendorAssignmentDto>(`${this.baseUrl}/VendorAssignment/${id}`);
  }

  getVendorAssignments(): Observable<VendorAssignmentDto[]> {
    return this.http.get<VendorAssignmentDto[]>(`${this.baseUrl}/VendorAssignment`);
  }

  // ✅ CHANGED: Parameter type is now CreateVendorAssignmentDto
  assignVendor(assignment: CreateVendorAssignmentDto): Observable<any> {
    return this.http.post(`${this.baseUrl}/VendorAssignment`, assignment, { responseType: 'text' });
  }

  updateVendorAssignment(id: number, data: UpdateVendorAssignmentPayload) {
    return this.http.put(`${this.baseUrl}/VendorAssignment/${id}`, data, { responseType: 'text' });
  }

  deleteVendorAssignment(id: number) {
    return this.http.delete(`${this.baseUrl}/VendorAssignment/${id}`, { responseType: 'text' });
  }

  // Cross-domain call needed by the vendor list status dropdown
  updateMaintenanceStatus(requestId: number, status: string) {
    return this.http.put(
      `${this.baseUrl}/MaintenanceRequest/${requestId}`,
      { status },
      { responseType: 'text' }
    );
  }
}