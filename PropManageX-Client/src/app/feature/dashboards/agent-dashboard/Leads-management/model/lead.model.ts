export interface Lead {
  leadID?: number;
  propertyID: number;
  customerName: string;
  contactInfo: string;
  interestType: string;
  createdDate: string;
  status: string;
}
 
export interface LeadPayload{
  propertyID: number;
  customerName: string;
  contactInfo: string;
  interestType: string;
  status: string;
}