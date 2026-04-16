import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../../environments/environment';
import { Observable } from 'rxjs';

export interface MaintenanceRequestDto {
  requestID: number;
  unitID: number;
  tenantID: number;
  category: string;
  description: string;
  raisedDate: string;
  priority: string;
  status: string;
}

export interface VendorAssignment {
  assignmentID?: number;
  requestID: number;
  vendorName: string;
  assignedDate: string;
  completionDate?: string;
  cost?: number;
  status?: string;
}

export interface Lease {
  contractID: number;
  tenant: string;
  unit: string;
  endDate: string;
  status: string;
}

@Injectable({ providedIn: 'root' })
export class ManagerService {

  private http = inject(HttpClient);
  private readonly baseUrl = environment.apiUrl || 'https://localhost:5157/api';

  // ===============================
  // 🔹 MAINTENANCE REQUEST
  // ===============================

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

  updateMaintenanceRequest(id: number, data: any) {
    return this.http.put(`${this.baseUrl}/MaintenanceRequest/${id}`, data, { responseType: 'text' });
  }

  deleteMaintenanceRequest(id: number) {
    return this.http.delete(`${this.baseUrl}/MaintenanceRequest/${id}`, { responseType: 'text' });
  }

  // ===============================
  // 🔹 VENDOR ASSIGNMENT
  // ===============================

  getVendorAssignments(): Observable<VendorAssignment[]> {
    return this.http.get<VendorAssignment[]>(`${this.baseUrl}/VendorAssignment`);
  }

  assignVendor(assignment: VendorAssignment): Observable<any> {
    return this.http.post(`${this.baseUrl}/VendorAssignment`, assignment, { responseType: 'text' });
  }

  updateVendorStatus(assignmentId: number, status: string) {
    return this.http.patch(
      `${this.baseUrl}/VendorAssignment/${assignmentId}/status`,
      { status },
      { responseType: 'text' }
    );
  }

  updateVendorAssignment(id: number, data: any) {
    return this.http.put(`${this.baseUrl}/VendorAssignment/${id}`, data, { responseType: 'text' });
  }

  deleteVendorAssignment(id: number) {
    return this.http.delete(`${this.baseUrl}/VendorAssignment/${id}`, { responseType: 'text' });
  }

  // ===============================
  // 🔹 DASHBOARD
  // ===============================

  getDashboardData() {
    return this.http.get<any>(`${this.baseUrl}/dashboard`);
  }

  // ===============================
  // 🔹 CONTRACTS
  // ===============================

  getContracts() {
    return this.http.get<any[]>(`${this.baseUrl}/contract`);
  }



  deleteContract(id: number) {
    return this.http.delete(`${this.baseUrl}/contract/${id}`, { responseType: 'text' });
  }

  // ===============================
  // 🔹 RENEWALS
  // ===============================

  getRenewals() {
    return this.http.get<any[]>(`${this.baseUrl}/renewal`);
  }

  createRenewal(data: any) {
    return this.http.post(
      `${this.baseUrl}/Renewal/renewcontract`,
      data,
      { responseType: 'text' }
    );
  }

  updateRenewal(id: number, data: any) {
    return this.http.put(`${this.baseUrl}/renewal/${id}`, data, { responseType: 'text' });
  }

  deleteRenewal(id: number) {
    return this.http.delete(`${this.baseUrl}/renewal/${id}`, { responseType: 'text' });
  }

}