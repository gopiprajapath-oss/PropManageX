namespace PropManageX.DTOs.DTOsContractsLeasesRenewal.ContractDTOs
{
    public class TenantOverviewDto
    {
        public int ContractID { get; set; }
        public string PropertyName { get; set; }
        public string Location { get; set; }
        public int UnitNumber { get; set; }
        public string ContractType { get; set; }
        public decimal ContractValue { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsSigned { get; set; }
    }
}
