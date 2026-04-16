using System.ComponentModel.DataAnnotations;

namespace PropManageX.DTOs.DTOsContractsLeasesRenewal.ContractDTOs
{
    public class UpdateContractDto
    {
        [Required]
        [RegularExpression("Sale|Lease", ErrorMessage = "Contract type must be Sale or Lease")]
        public string ContractType { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Contract value must be greater than 0")]
        public decimal ContractValue { get; set; }

        [Required]
        [RegularExpression("Active|Expired|Terminated", ErrorMessage = "Invalid status")]
        public string Status { get; set; }
    }
}
