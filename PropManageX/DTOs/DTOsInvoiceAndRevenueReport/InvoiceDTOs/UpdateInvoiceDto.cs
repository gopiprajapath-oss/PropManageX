using System.ComponentModel.DataAnnotations;

namespace PropManageX.DTOs.DTOsInvoiceAndRevenueReport.InvoiceDTOs
{
    public class UpdateInvoiceDto
    {
        [Required]
        public string Status { get; set; }

    }
}
