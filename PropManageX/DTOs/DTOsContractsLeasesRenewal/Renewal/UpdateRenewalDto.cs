using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.DataAnnotations;

namespace PropManageX.DTOs.DTOsContractsLeasesRenewal.Renewal
{
    public class UpdateRenewalDto
    {
        [Required]
        [Range(1, double.MaxValue)]
        public decimal ProposedValue { get; set; }

        [Required]
        public DateTime ProposedEndDate { get; set; }

        [Required]
        [RegularExpression("Accepted|Declined", ErrorMessage = "Invalid status")]
        public string Status { get; set; }
    }
}
