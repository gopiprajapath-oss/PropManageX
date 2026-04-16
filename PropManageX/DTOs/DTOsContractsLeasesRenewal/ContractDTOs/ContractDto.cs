
namespace PropManageX.DTOs.DTOsContractsLeasesRenewal.ContractDTOs
{
    public class ContractDto
    {
        public int ContractID { get; set; }

        public int DealID { get; set; }

        public string ContractType { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal ContractValue { get; set; }

        public string Status { get; set; }
    }
}
