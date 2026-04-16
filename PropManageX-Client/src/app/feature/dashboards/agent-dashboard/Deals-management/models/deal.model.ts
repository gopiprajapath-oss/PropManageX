export interface Deal {
  dealID: number;
  leadID: number;
  unitID: number;
  dealType: string;
  agreedValue: number;
  expectedClosureDate: string;
  status: string;
}
 
export interface DealPayload {
  leadID: number;
  unitID: number;
  dealType: string;
  agreedValue: number;
  expectedClosureDate: string;
  status?: string; // only for update
}
 