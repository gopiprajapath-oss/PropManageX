using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PropManageX.DTOs.DTOsInvoiceAndRevenueReport.RevenueReportDTOs;
using PropManageX.Services.BillingReferenceAndAnalytics.RevenueReport;

namespace PropManageX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RevenueReportController : ControllerBase
    {
        private readonly IRevenueReportService _service;

        public RevenueReportController(IRevenueReportService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Admin , Finance Analyst")]
        [HttpPost]
        public async Task<IActionResult> GenerateReport(CreateRevenueReportDto dto)
        {
            var report = await _service.GenerateReport(dto);
            return Ok(report);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetReports()
        {
            var reports = await _service.GetReports();
            return Ok(reports);
        }

        [Authorize(Roles = "Admin , Finance Analyst")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReportById(int id)
        {
            var report = await _service.GetReportById(id);

            if (report == null)
                return NotFound("Report not found");

            return Ok(report);
        }

        [Authorize(Roles = "Admin , Finance Analyst")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReport(int id)
        {
            var result = await _service.DeleteReport(id);

            if (!result)
                return NotFound("Report not found");

            return Ok("Report deleted successfully");
        }


        [Authorize(Roles = "Admin , Finance Analyst")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReport(int id, [FromBody] UpdateRevenueReportDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedReport = await _service.UpdateReportAsync(id, dto);

            if (updatedReport == null)
            {
                return NotFound(new { message = $"Revenue Report with ID {id} was not found." });
            }

            return Ok(updatedReport);
        }




    }
}
