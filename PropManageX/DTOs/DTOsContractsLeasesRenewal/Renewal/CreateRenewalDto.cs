using System.ComponentModel.DataAnnotations;

namespace PropManageX.DTOs.DTOsContractsLeasesRenewal.Renewal
{
    public class CreateRenewalDto
    {
        [Required]
        public int ContractID { get; set; }

        [Required]
        public DateTime ProposedEndDate { get; set; }

        [Required]

        public decimal ProposedValue { get; set; }
    }
}
