namespace PropManageX.DTOs.DTOsInvoiceAndRevenueReport.InvoiceDTOs
{
    public class InvoiceDto
    {
        public int InvoiceID { get; set; }

        public int ContractID { get; set; }

        public string Period { get; set; }

        public decimal Amount { get; set; }

        public DateTime DueDate { get; set; }

        public string Status { get; set; }
    }
}
