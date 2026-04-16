using PropManageX.DTOs.DTOsLeadSalesLeasing.LeadDTOs;

namespace PropManageX.Services.LeadsSalesAndLeasingManagement.ServiceLead
{
    public interface ILeadService
    {
        Task<List<LeadDto>> GetAllLeads();

        Task<LeadDto> GetLeadById(int id);

        Task<LeadDto> CreateLead(CreateLeadDto dto);

        Task<LeadDto> UpdateLead(int id, UpdateLeadDto dto);

        Task<bool> DeleteLead(int id);

    }
}
