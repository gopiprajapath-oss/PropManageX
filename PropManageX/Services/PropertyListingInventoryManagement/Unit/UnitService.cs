using Humanizer;
using Microsoft.EntityFrameworkCore;
using PropManageX.DTOs.DTOsProperty.UnitsDTOs;
using PropManageX.DTOs.PropertyDTOs.UnitsDTOs;
using PropManageX.Models.Context;
using PropManageX.Models.Entities;
using PropManageX.Services.PropertyListingInventoryManagement.UnitService.UnitService;

namespace PropManageX.Services.PropertyListingInventoryManagement.ServiceUnit 
{
    public class UnitService : IUnitService
    {


        private readonly PropManageXContext _context;

        public UnitService(PropManageXContext context)
        {
            _context = context;
        }

        public async Task<List<UnitDto>> GetAllUnits()
        {
            return await _context.Units
            .Select(u => new UnitDto
            {
                UnitID = u.UnitID,
                PropertyID = u.PropertyID,
                UnitNumber = u.UnitNumber,
                AreaSqFt = u.AreaSqFt,
                BedroomCount = u.BedroomCount,
                BasePrice = u.BasePrice,
                Status = u.Status
            })
            .ToListAsync();
        }

        public async Task<UnitDto> GetUnitById(int id)
        {
            var unit = await _context.Units.FindAsync(id);

            if (unit == null)
                return null;

            return new UnitDto
            {
                UnitID = unit.UnitID,
                PropertyID = unit.PropertyID,
                UnitNumber = unit.UnitNumber,
                AreaSqFt = unit.AreaSqFt,
                BedroomCount = unit.BedroomCount,
                BasePrice = unit.BasePrice,
                Status = unit.Status
            };
        }

        public async Task<List<UnitDto>> GetUnitsByPropertyId(int propertyId)
        {
            return await _context.Units
                .Where(u => u.PropertyID == propertyId)
                .Select(u => new UnitDto
                {
                    UnitID = u.UnitID,
                    PropertyID = u.PropertyID,
                    UnitNumber = u.UnitNumber,
                    AreaSqFt = u.AreaSqFt,
                    BedroomCount = u.BedroomCount,
                    BasePrice = u.BasePrice,
                    Status = u.Status
                })
                .ToListAsync();
        }


        public async Task<UnitDto> CreateUnit(CreateUnitDto dto)
        {
            // Step 1: Get property
            var property = await _context.Properties
            .FirstOrDefaultAsync(p => p.PropertyID == dto.PropertyID);

            if (property == null)
                throw new Exception("Property not found");

            // Step 2: Count existing units
            var existingUnits = await _context.Units
            .CountAsync(u => u.PropertyID == dto.PropertyID);

            // Step 3: Check limit
            if (existingUnits >= property.TotalUnits)
            {
                return null;
            }

            // Step 4: Create unit
            var unit = new UnitModel
            {
                PropertyID = dto.PropertyID,
                UnitNumber = dto.UnitNumber,
                AreaSqFt = dto.AreaSqFt,
                BedroomCount = dto.BedroomCount,
                BasePrice = dto.BasePrice,
                Status = dto.Status
            };

            _context.Units.Add(unit);
            await _context.SaveChangesAsync();

            return new UnitDto
            {
                UnitID = unit.UnitID,
                PropertyID = unit.PropertyID,
                UnitNumber = unit.UnitNumber,
                AreaSqFt = unit.AreaSqFt,
                BedroomCount = unit.BedroomCount,
                BasePrice = unit.BasePrice,
                Status = unit.Status
            };
        }

        public async Task<UnitDto> UpdateUnit(int id, UpdateUnitDto dto)
        {
            var unit = await _context.Units.FindAsync(id);

            if (unit == null)
                return null;

            unit.UnitNumber = dto.UnitNumber;
            unit.AreaSqFt = dto.AreaSqFt;
            unit.BedroomCount = dto.BedroomCount;
            unit.BasePrice = dto.BasePrice;
            unit.Status = dto.Status;

            await _context.SaveChangesAsync();

            return new UnitDto
            {
                UnitID = unit.UnitID,
                PropertyID = unit.PropertyID,
                UnitNumber = unit.UnitNumber,
                AreaSqFt = unit.AreaSqFt,
                BedroomCount = unit.BedroomCount,
                BasePrice = unit.BasePrice,
                Status = unit.Status
            };
        }

        public async Task<bool> DeleteUnit(int id)
        {
            var unit = await _context.Units.FindAsync(id);

            if (unit == null)
                return false;

            _context.Units.Remove(unit);
            await _context.SaveChangesAsync();

            return true;
        }

        // 👉 Add this inside UnitService class
        public async Task<List<TenantUnitDto>> GetMyUnits(string email)
        {
            return await _context.Contracts
                .Include(c => c.Deal)
                    .ThenInclude(d => d.UnitModel)
                        .ThenInclude(u => u.PropertyModel)
                .Where(c => c.Deal.LeadModel.ContactInfo == email
                         && (c.Status == "Active" || c.Status == "Signed"))
                .Select(c => new TenantUnitDto
                {
                    UnitID = c.Deal.UnitID,
                    DisplayText = c.Deal.UnitModel.PropertyModel.Name + " - Unit " + c.Deal.UnitModel.UnitNumber
                })
                .Distinct()
                .ToListAsync();
        }


    }
}
