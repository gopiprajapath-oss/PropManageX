export interface VendorAssignmentDto {
  assignmentID: number;
  requestID: number;
  vendorName: string;
  assignedDate: string;
  completionDate?: string;
  cost?: number;
}

export interface VendorAssignment {
  assignmentID: number;
  requestID: number;
  vendorName: string;
  assignedDate: string;
  completionDate?: string;
  cost?: number;
  status?: string;
}

export interface UpdateVendorAssignmentPayload {
  vendorName?: string;
  assignedDate?: string;
  completionDate?: string;
  cost?: number;
}

// ✅ UPDATED TO MATCH YOUR COMPONENT PAYLOAD
export interface CreateVendorAssignmentDto {
  requestID: number;
  vendorName: string;
  assignedDate?: string;
  completionDate?: string;
  cost?: number;
}