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

export interface UpdateMaintenanceRequestDto {
  status: string;
}