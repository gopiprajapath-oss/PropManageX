namespace PropManageX.DTOs.DTOsInvoiceAndRevenueReport.RevenueReportDTOs
{
    public class RevenueReportDto
    {
        public int ReportID { get; set; }

        public string Scope { get; set; }

        public string Metrics { get; set; }

        public DateTime GeneratedDate { get; set; }
    }
}
