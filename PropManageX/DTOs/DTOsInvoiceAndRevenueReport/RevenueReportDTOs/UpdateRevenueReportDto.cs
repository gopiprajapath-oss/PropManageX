namespace PropManageX.DTOs.DTOsInvoiceAndRevenueReport.RevenueReportDTOs
{
    public class UpdateRevenueReportDto
    {
        public string Scope { get; set; }

        // Matches the 'metrics' JSON string sent from Angular
        public string Metrics { get; set; }
    }
}
