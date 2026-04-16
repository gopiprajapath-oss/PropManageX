using PropManageX.DTOs.DTOsInvoiceAndRevenueReport.RevenueReportDTOs;

namespace PropManageX.Services.BillingReferenceAndAnalytics.RevenueReport
{
    public interface IRevenueReportService
    {
        Task<RevenueReportDto> GenerateReport(CreateRevenueReportDto dto);

        Task<List<RevenueReportDto>> GetReports();

        Task<RevenueReportDto> GetReportById(int id);

        Task<bool> DeleteReport(int id);

        Task<RevenueReportDto> UpdateReportAsync(int id, UpdateRevenueReportDto dto);

    }
}
