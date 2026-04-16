export interface Renewal {
proposedValue: string|number;
proposedEndDate: string|number|Date;
  renewalID: number;
  contractID: number;
  renewalDate: string;
  newEndDate: string;
  status: string;
}

export interface CreateRenewalPayload {
  contractID: number;
  proposedEndDate: string; // matches backend
  proposedValue: number;   // PascalCase in backend
}