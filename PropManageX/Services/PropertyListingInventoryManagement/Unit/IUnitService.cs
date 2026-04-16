using PropManageX.DTOs.DTOsProperty.UnitsDTOs;
using PropManageX.DTOs.PropertyDTOs.UnitsDTOs;

namespace PropManageX.Services.PropertyListingInventoryManagement.UnitService.UnitService
{
    public interface IUnitService
    {

        Task<List<UnitDto>> GetAllUnits();
        Task<UnitDto> GetUnitById(int id);

        Task<UnitDto> CreateUnit(CreateUnitDto dto);

        Task<UnitDto> UpdateUnit(int id, UpdateUnitDto dto);

        Task<bool> DeleteUnit(int id);

        Task<List<UnitDto>> GetUnitsByPropertyId(int propertyId);

        Task<List<TenantUnitDto>> GetMyUnits(string email);

    }
}
