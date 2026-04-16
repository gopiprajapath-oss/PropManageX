using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PropManageX.DTOs.DTOsInvoiceAndRevenueReport.InvoiceDTOs;
using PropManageX.Services.BillingReferenceAndAnalytics.Invoice;

namespace PropManageX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _service;

        public InvoiceController(IInvoiceService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Finance Analyst")]
        [HttpPost]
        public async Task<IActionResult> CreateInvoice(CreateInvoiceDto dto)
        {
            var invoice = await _service.CreateInvoice(dto);
            return Ok(invoice);
        }

        [Authorize(Roles = "Admin,Finance Analyst")]
        [HttpGet]
        public async Task<IActionResult> GetInvoices()
        {
            var invoices = await _service.GetInvoices();
            return Ok(invoices);
        }

        [Authorize(Roles = "Admin,Finance Analyst")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInvoiceById(int id)
        {
            var invoice = await _service.GetInvoiceById(id);

            if (invoice == null)
                return NotFound("Invoice not found");

            return Ok(invoice);
        }

        [Authorize(Roles = "Finance Analyst")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInvoiceStatus(int id, UpdateInvoiceDto dto)
        {
            var invoice = await _service.UpdateInvoiceStatus(id, dto);

            if (invoice == null)
                return NotFound("Invoice not found");

            return Ok(invoice);
        }

        [Authorize(Roles = "Finance Analyst")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice(int id)
        {
            var result = await _service.DeleteInvoice(id);

            if (!result)
                return NotFound("Invoice not found");

            return Ok("Invoice deleted successfully");
        }
    }
}
