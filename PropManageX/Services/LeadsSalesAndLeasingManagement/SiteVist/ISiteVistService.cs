using PropManageX.DTOs.DTOsLeadSalesLeasing.SiteVistDTOs;

namespace PropManageX.Services.LeadsSalesAndLeasingManagement.ServiceSiteVist
{
    public interface ISiteVistService
    {
        Task<List<SiteVisitDto>> GetAllVisits();

        Task<SiteVisitDto> GetVisitById(int id);

        Task<SiteVisitDto> CreateVisit(CreateSiteVisitDto dto);

        Task<SiteVisitDto> UpdateVisit(int id, UpdateSiteVisitDto dto);

        Task<bool> DeleteVisit(int id);
    }
}
