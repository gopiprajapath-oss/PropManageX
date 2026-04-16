using PropManageX.Models.Context;
using PropManageX.Models.Entities;
using Microsoft.EntityFrameworkCore;
using PropManageX.DTOs.DTOsInvoiceAndRevenueReport.InvoiceDTOs;

namespace PropManageX.Services.BillingReferenceAndAnalytics.Invoice
{
    public class InvoiceService : IInvoiceService
    {
        private readonly PropManageXContext _context;

        public InvoiceService(PropManageXContext context)
        {
            _context = context;
        }

        public async Task<InvoiceDto> CreateInvoice(CreateInvoiceDto dto)
        {
            var contract = await _context.Contracts.FindAsync(dto.ContractID);

            if (contract == null)
                throw new Exception("Contract not found");

            var invoice = new InvoiceModel
            {
                ContractID = dto.ContractID,
                Period = dto.Period,
                Amount = dto.Amount,
                DueDate = dto.DueDate,
                Status = "Pending"
            };

            _context.Invoices.Add(invoice);

            await _context.SaveChangesAsync();

            return new InvoiceDto
            {
                InvoiceID = invoice.InvoiceID,
                ContractID = invoice.ContractID,
                Period = invoice.Period,
                Amount = invoice.Amount,
                DueDate = invoice.DueDate,
                Status = invoice.Status
            };
        }

        public async Task<List<InvoiceDto>> GetInvoices()
        {
            return await _context.Invoices
            .Select(i => new InvoiceDto
            {
                InvoiceID = i.InvoiceID,
                ContractID = i.ContractID,
                Period = i.Period,
                Amount = i.Amount,
                DueDate = i.DueDate,
                Status = i.Status
            }).ToListAsync();
        }

        public async Task<InvoiceDto> GetInvoiceById(int id)
        {
            var invoice = await _context.Invoices.FindAsync(id);

            if (invoice == null)
                return null;

            return new InvoiceDto
            {
                InvoiceID = invoice.InvoiceID,
                ContractID = invoice.ContractID,
                Period = invoice.Period,
                Amount = invoice.Amount,
                DueDate = invoice.DueDate,
                Status = invoice.Status
            };
        }

        public async Task<InvoiceDto> UpdateInvoiceStatus(int id, UpdateInvoiceDto dto)
        {
            var invoice = await _context.Invoices.FindAsync(id);

            if (invoice == null)
                return null;

            invoice.Status = dto.Status;

            await _context.SaveChangesAsync();

            return new InvoiceDto
            {
                InvoiceID = invoice.InvoiceID,
                ContractID = invoice.ContractID,
                Period = invoice.Period,
                Amount = invoice.Amount,
                DueDate = invoice.DueDate,
                Status = invoice.Status
            };
        }

        public async Task<bool> DeleteInvoice(int id)
        {
            var invoice = await _context.Invoices.FindAsync(id);

            if (invoice == null)
                return false;

            _context.Invoices.Remove(invoice);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
