using System.ComponentModel.DataAnnotations;

namespace PropManageX.DTOs.DTOsLeadSalesLeasing.LeadDTOs
{
    public class CreateLeadDto
    {
        [Required]
        public int PropertyID { get; set; }
        public string CustomerName { get; set; }
        [Required]
        [EmailAddress]
        public string ContactInfo { get; set; }
        public string InterestType { get; set; } //(Buy/Rent)

       // public string Status { get; set; } //(New/Contacted/VisitScheduled/Negotiating/Closed/Lost) 
    }
}
