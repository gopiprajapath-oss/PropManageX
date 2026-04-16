using PropManageX.Models.Entities;
using PropManageX.Models.Context;
using Microsoft.EntityFrameworkCore;
using PropManageX.DTOs.PropertyDTOs.AmenityDTOs;
using PropManageX.Services.PropertyListingInventoryManagement.AmenityService;

namespace PropManageX.Services.PropertyListingInventoryManagement.ServiceAmenity
{
    public class AmenityService : IAmenityService
    {
        private readonly PropManageXContext _context;

        public AmenityService(PropManageXContext context)
        {
            _context = context;
        }

        public async Task<List<AmenityDto>> GetAllAmenities()
        {
            return await _context.Amenities
            .Select(a => new AmenityDto
            {
                AmenityID = a.AmenityID,
                PropertyID = a.PropertyID,
                Name = a.Name,
                Description = a.Description
            })
            .ToListAsync();
        }

        public async Task<AmenityDto> GetAmenityById(int id)
        {
            var amenity = await _context.Amenities.FindAsync(id);

            if (amenity == null)
                return null;

            return new AmenityDto
            {
                AmenityID = amenity.AmenityID,
                PropertyID = amenity.PropertyID,
                Name = amenity.Name,
                Description = amenity.Description
            };
        }

        public async Task<AmenityDto> CreateAmenity(CreateAmenityDto dto)
        {
            var amenity = new AmenityModel
            {
                PropertyID = dto.PropertyID,
                Name = dto.Name,
                Description = dto.Description
            };

            _context.Amenities.Add(amenity);
            await _context.SaveChangesAsync();

            return new AmenityDto
            {
                AmenityID = amenity.AmenityID,
                PropertyID = amenity.PropertyID,
                Name = amenity.Name,
                Description = amenity.Description
            };
        }

        public async Task<AmenityDto> UpdateAmenity(int id, UpdateAmenityDto dto)
        {
            var amenity = await _context.Amenities.FindAsync(id);

            if (amenity == null)
                return null;

            amenity.Name = dto.Name;
            amenity.Description = dto.Description;

            await _context.SaveChangesAsync();

            return new AmenityDto
            {
                AmenityID = amenity.AmenityID,
                PropertyID = amenity.PropertyID,
                Name = amenity.Name,
                Description = amenity.Description
            };
        }

        public async Task<bool> DeleteAmenity(int id)
        {
            var amenity = await _context.Amenities.FindAsync(id);

            if (amenity == null)
                return false;

            _context.Amenities.Remove(amenity);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<AmenityDto>> GetAmenityByPropertyId(int propertyId)
        {
            var amenity = await _context.Amenities
                .FirstOrDefaultAsync(a => a.PropertyID == propertyId);

            if (amenity == null)
                return null;

            return await _context.Amenities
            .Select(a => new AmenityDto
            {
                AmenityID = a.AmenityID,
                PropertyID = a.PropertyID,
                Name = a.Name,
                Description = a.Description
            })
            .ToListAsync();

        }
    }
}