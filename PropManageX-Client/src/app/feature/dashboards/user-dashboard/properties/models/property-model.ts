export interface Property {
  propertyID: number;
  name: string;
  location: string;
  type: string; // e.g., 'Apartment', 'Villa', 'Commercial'
  totalUnits: number;
  price?: number; 
  status: string;
  image?: string; 
}

export interface PropertySearchFilters {
  query: string;
  location: string;
  type: string;
  minPrice: number | null;
  maxPrice: number | null;
}