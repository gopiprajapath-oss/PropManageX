export interface CreateLeadDto {
  propertyID: number;
  customerName: string;
  contactInfo: string; // This is the email/gmail
  interestType: 'Buy' | 'Rent';
  status?: 'New' // Optional for creation, defaults to 'New'
}