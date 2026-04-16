namespace PropManageX.DTOs.DTOsLeadSalesLeasing.LeadDTOs
{
    public class LeadDto
    {
        public int LeadID { get; set; }
        public int PropertyID { get; set; }
        public string CustomerName { get; set; }
        public string ContactInfo { get; set; }
        public string InterestType { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; }
    }
}
