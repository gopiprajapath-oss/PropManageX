namespace PropManageX.DTOs.DTOsContractsLeasesRenewal.Renewal
{
    public class RenewalDto
    {
        public int RenewalID { get; set; }

        public int ContractID { get; set; }

        public decimal ProposedValue { get; set; }

        public DateTime ProposedEndDate { get; set; }

        public string Status { get; set; }
    }
}
