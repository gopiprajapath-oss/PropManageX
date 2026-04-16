export interface Agreement {
  contractID: number;
  dealID: number;
  contractType: string;
  startDate: string;
  endDate: string;
  contractValue: number;
  status: string;
  isSigned: boolean;
  signDate?: string;
}