using System.ComponentModel.DataAnnotations;

namespace PropManageX.DTOs.DTOsInvoiceAndRevenueReport.InvoiceDTOs
{
    public class CreateInvoiceDto
    {
        [Required]
        public int ContractID { get; set; }

        [Required]
        public string Period { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime DueDate { get; set; }
    }
}
