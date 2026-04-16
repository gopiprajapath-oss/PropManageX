using System.ComponentModel.DataAnnotations;

namespace PropManageX.DTOs.DTOsInvoiceAndRevenueReport.RevenueReportDTOs
{
    public class CreateRevenueReportDto
    {
        [Required]
        public string Scope { get; set; }

        [Required]
        public string Metrics { get; set; }
    }
}
