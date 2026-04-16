using PropManageX.DTOs.PropertyDTOs.Property;
using PropManageX.Models.Context;
using Microsoft.EntityFrameworkCore;
using PropManageX.Services.PropertyListingInventoryManagement.PropertyService.PropertyService;

namespace PropManageX.Services.PropertyListingInventoryManagement.ServiceProperty
{
    public class PropertyService : IPropertyService
        {
            private readonly PropManageXContext _context;

            public PropertyService(PropManageXContext context)
            {
                _context = context;
            }

            public async Task<List<PropertyDetailsDto>> GetAllProperties()
            {
                return await _context.Properties
                .Select(p => new PropertyDetailsDto
                {
                    PropertyID = p.PropertyID,
                    Name = p.Name,
                    Type = p.Type,
                    Location = p.Location,
                    TotalUnits = p.TotalUnits,
                    Status = p.Status
                })
                .ToListAsync();
            }

            public async Task<PropertyDetailsDto> GetPropertyById(int id)
            {
                var property = await _context.Properties.FindAsync(id);

                if (property == null)
                    return null;

                return new PropertyDetailsDto
                {
                    PropertyID = property.PropertyID,
                    Name = property.Name,
                    Type = property.Type,
                    Location = property.Location,
                    TotalUnits = property.TotalUnits,
                    Status = property.Status
                };
            }

            public async Task<PropertyDetailsDto> CreateProperty(CreatePropertyDto Createdto)
            {
                var property = new Models.Entities.PropertyModel
                {
                    Name = Createdto.Name,
                    Type = Createdto.Type,
                    Location = Createdto.Location,
                    TotalUnits = Createdto.TotalUnits,
                    Status = Createdto.Status
                };

                _context.Properties.Add(property);
                await _context.SaveChangesAsync();

                return new PropertyDetailsDto
                {
                    PropertyID = property.PropertyID,
                    Name = property.Name,
                    Type = property.Type,
                    Location = property.Location,
                    TotalUnits = property.TotalUnits,
                    Status = property.Status
                };
            }

            public async Task<PropertyDetailsDto> UpdateProperty(int id, UpdatePropertyDto Updatedto)
            {
                var property = await _context.Properties.FindAsync(id);

                if (property == null)
                    return null;

                property.Name = Updatedto.Name;
                property.Type = Updatedto.Type;
                property.Location = Updatedto.Location;
                property.TotalUnits = Updatedto.TotalUnits;
                property.Status = Updatedto.Status;

                await _context.SaveChangesAsync();

                return new PropertyDetailsDto
                {
                    PropertyID = property.PropertyID,
                    Name = property.Name,
                    Type = property.Type,
                    Location = property.Location,
                    TotalUnits = property.TotalUnits,
                    Status = property.Status
                };
            }

            public async Task<bool> DeleteProperty(int id)
            {
                var property = await _context.Properties.FindAsync(id);

                if (property == null)
                    return false;

                _context.Properties.Remove(property);
                await _context.SaveChangesAsync();

                return true;
            }


        public async Task<List<PropertyDetailsDto>> SearchProperties(string? q, string? location, string? type)
        {
            // Start with a base query
            var query = _context.Properties.AsQueryable();

            // Dynamically apply filters if they are provided
            if (!string.IsNullOrWhiteSpace(q))
            {
                query = query.Where(p => p.Name.Contains(q));
            }

            if (!string.IsNullOrWhiteSpace(location))
            {
                query = query.Where(p => p.Location.Contains(location));
            }

            if (!string.IsNullOrWhiteSpace(type))
            {
                query = query.Where(p => p.Type == type);
            }

            // Project to DTO and execute
            return await query.Select(p => new PropertyDetailsDto
            {
                PropertyID = p.PropertyID,
                Name = p.Name,
                Type = p.Type,
                Location = p.Location,
                TotalUnits = p.TotalUnits,
                Status = p.Status
            }).ToListAsync();
        }
    }
  }