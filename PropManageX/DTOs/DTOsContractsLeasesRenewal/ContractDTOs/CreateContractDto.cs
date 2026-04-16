using System.ComponentModel.DataAnnotations;

namespace PropManageX.DTOs.DTOsContractsLeasesRenewal.ContractDTOs
{
    public class CreateContractDto
    {
        [Required(ErrorMessage = "Deal ID is required")]
        public int DealID { get; set; }

        [Required(ErrorMessage = "Contract type is required")]
        public string ContractType { get; set; }

        [Required(ErrorMessage = "Start date is required")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Contract value is required")]
        [Range(1, double.MaxValue, ErrorMessage = "Contract value must be greater than 0")]
        public decimal ContractValue { get; set; }
    }
}
