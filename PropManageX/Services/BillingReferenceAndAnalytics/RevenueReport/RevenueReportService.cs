using PropManageX.Models.Context;
using PropManageX.Models.Entities;
using Microsoft.EntityFrameworkCore;
using PropManageX.DTOs.DTOsInvoiceAndRevenueReport.RevenueReportDTOs;
namespace PropManageX.Services.BillingReferenceAndAnalytics.RevenueReport
{
    public class RevenueReportService : IRevenueReportService 
    {
        private readonly PropManageXContext _context;

        public RevenueReportService(PropManageXContext context)
        {
            _context = context;
        }

        public async Task<RevenueReportDto> GenerateReport(CreateRevenueReportDto dto)
        {
            var report = new RevenueReportModel
            {
                Scope = dto.Scope,
                Metrics = dto.Metrics,
                GeneratedDate = DateTime.Now
            };

            _context.RevenueReports.Add(report);

            await _context.SaveChangesAsync();

            return new RevenueReportDto
            {
                ReportID = report.ReportID,
                Scope = report.Scope,
                Metrics = report.Metrics,
                GeneratedDate = report.GeneratedDate
            };
        }

        public async Task<List<RevenueReportDto>> GetReports()
        {
            return await _context.RevenueReports
            .Select(r => new RevenueReportDto
            {
                ReportID = r.ReportID,
                Scope = r.Scope,
                Metrics = r.Metrics,
                GeneratedDate = r.GeneratedDate
            })
            .ToListAsync();
        }

        public async Task<RevenueReportDto> GetReportById(int id)
        {
            var report = await _context.RevenueReports.FindAsync(id);

            if (report == null)
                return null;

            return new RevenueReportDto
            {
                ReportID = report.ReportID,
                Scope = report.Scope,
                Metrics = report.Metrics,
                GeneratedDate = report.GeneratedDate
            };
        }

        public async Task<bool> DeleteReport(int id)
        {
            var report = await _context.RevenueReports.FindAsync(id);

            if (report == null)
                return false;

            _context.RevenueReports.Remove(report);

            await _context.SaveChangesAsync();

            return true;
        }




        public async Task<RevenueReportDto> UpdateReportAsync(int id, UpdateRevenueReportDto dto)
        {
            // 1. Find the existing report in the database
            var existingReport = await _context.RevenueReports.FindAsync(id);

            // 2. If it doesn't exist, return null so the controller can return a 404
            if (existingReport == null)
            {
                return null;
            }

            // 3. Update the fields with the new data from Angular
            existingReport.Scope = dto.Scope;
            existingReport.Metrics = dto.Metrics;

        

            // 4. Save the changes to the database
            _context.RevenueReports.Update(existingReport);
            await _context.SaveChangesAsync();

            // 5. Return the updated data back to the frontend
            return new RevenueReportDto
            {
                ReportID = existingReport.ReportID,
                Scope = existingReport.Scope,
                Metrics = existingReport.Metrics,
                GeneratedDate = existingReport.GeneratedDate
            };
        }
    }
}
