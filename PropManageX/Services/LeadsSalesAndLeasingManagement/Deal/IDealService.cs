using PropManageX.DTOs.DTOsLeadSalesLeasing.DealDTOs;
using PropManageX.DTOs.DTOsLeadSalesLeasing.LeadDTOs;

namespace PropManageX.Services.LeadsSalesAndLeasingManagement.ServiceDeal
{
    public interface IDealService
    {
        Task<List<DealDto>> GetAllDeals();

        Task<DealDto> GetDealById(int id);

        Task<DealDto> CreateDeal(CreateDealDto dto);

        Task<DealDto> UpdateDeal(int id, UpdateDealDto dto);

        Task<bool> DeleteDeal(int id);
    }
}
