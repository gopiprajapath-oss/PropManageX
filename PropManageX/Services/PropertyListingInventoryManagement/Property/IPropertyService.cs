using Microsoft.Build.Framework;
using PropManageX.DTOs.PropertyDTOs.Property;

namespace PropManageX.Services.PropertyListingInventoryManagement.PropertyService.PropertyService
{
    public interface IPropertyService
    {
        Task<List<PropertyDetailsDto>> GetAllProperties();

        Task<PropertyDetailsDto> GetPropertyById(int id);

        Task<PropertyDetailsDto> CreateProperty(CreatePropertyDto dto);

        Task<PropertyDetailsDto> UpdateProperty(int id, UpdatePropertyDto dto);

        Task<bool> DeleteProperty(int id);

        Task<List<PropertyDetailsDto>> SearchProperties(string? q, string? location, string? type);
    }
}
