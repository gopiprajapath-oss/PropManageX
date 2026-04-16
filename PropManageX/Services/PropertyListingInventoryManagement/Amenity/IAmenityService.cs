using PropManageX.DTOs.PropertyDTOs.AmenityDTOs;

namespace PropManageX.Services.PropertyListingInventoryManagement.AmenityService
{
    public interface IAmenityService
    {
        Task<List<AmenityDto>> GetAllAmenities();

        Task<AmenityDto> GetAmenityById(int id);

        Task<List<AmenityDto>> GetAmenityByPropertyId(int id);

        Task<AmenityDto> CreateAmenity(CreateAmenityDto dto);

        Task<AmenityDto> UpdateAmenity(int id, UpdateAmenityDto dto);

        Task<bool> DeleteAmenity(int id);
    }
}
