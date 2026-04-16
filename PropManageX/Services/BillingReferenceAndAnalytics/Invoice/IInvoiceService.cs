using PropManageX.DTOs.DTOsInvoiceAndRevenueReport.InvoiceDTOs;

namespace PropManageX.Services.BillingReferenceAndAnalytics.Invoice
{
    public interface IInvoiceService
    {
        Task<InvoiceDto> CreateInvoice(CreateInvoiceDto dto);

        Task<List<InvoiceDto>> GetInvoices();

        Task<InvoiceDto> GetInvoiceById(int id);

        Task<InvoiceDto> UpdateInvoiceStatus(int id, UpdateInvoiceDto dto);

        Task<bool> DeleteInvoice(int id);
    }
}
