namespace PropManageX.DTOs.DTOsLeadSalesLeasing.DealDTOs
{
    public class DealDto
    {
        public int DealID { get; set; }

        public int LeadID { get; set; }

        public int UnitID { get; set; }

        public string DealType { get; set; }

        public decimal AgreedValue { get; set; }

        public string Status { get; set; }
    }
}
