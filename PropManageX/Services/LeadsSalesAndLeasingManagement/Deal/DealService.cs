using PropManageX.DTOs.DTOsLeadSalesLeasing.DealDTOs;
using PropManageX.DTOs.DTOsLeadSalesLeasing.LeadDTOs;
using PropManageX.Models.Context;
using PropManageX.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace PropManageX.Services.LeadsSalesAndLeasingManagement.ServiceDeal
{
    public class DealService : IDealService
    {
        private readonly PropManageXContext _context;

        public DealService(PropManageXContext context)
        {
            _context = context;
        }

        public async Task<List<DealDto>> GetAllDeals()
        {
            return await _context.Deals
            .Select(d => new DealDto
            {
                DealID = d.DealID,
                LeadID = d.LeadID,
                UnitID = d.UnitID,
                DealType = d.DealType,
                AgreedValue = d.AgreedValue,
                Status = d.Status
            }).ToListAsync();
        }

        public async Task<DealDto> GetDealById(int id)
        {
            var deal = await _context.Deals.FindAsync(id);

            if (deal == null)
                return null;

            return new DealDto
            {
                DealID = deal.DealID,
                LeadID = deal.LeadID,
                UnitID = deal.UnitID,
                DealType = deal.DealType,
                AgreedValue = deal.AgreedValue,
                Status = deal.Status
            };
        }

        public async Task<DealDto> CreateDeal(CreateDealDto dto)
        {
            var unitExist = await _context.Units.AnyAsync(u => u.UnitID == dto.UnitID);
       
            var leadExist = await _context.Leads.AnyAsync(l => l.LeadID == dto.LeadID);
            if (unitExist == false || leadExist == false)
            {
                return null;
            }
               

            var deal = new DealModel
            {
                LeadID = dto.LeadID,
                UnitID = dto.UnitID,
                DealType = dto.DealType,
                AgreedValue = dto.AgreedValue,
                Status = "Open"
            };

            _context.Deals.Add(deal);
            await _context.SaveChangesAsync();
            if (true)
            {
                _context.Leads.Where(l => l.LeadID == deal.LeadID).ToList().ForEach(l => l.Status = "Negotiating");
                await _context.SaveChangesAsync();

            }

            return new DealDto
            {
                DealID = deal.DealID,
                LeadID = deal.LeadID,
                UnitID = deal.UnitID,
                DealType = deal.DealType,
                AgreedValue = deal.AgreedValue,
                Status = deal.Status
            };
        }

        public async Task<DealDto> UpdateDeal(int id, UpdateDealDto dto)
        {
            var deal = await _context.Deals.FindAsync(id);

            if (deal == null)
                return null;

            deal.DealType = dto.DealType;
            deal.AgreedValue = dto.AgreedValue;
            deal.Status = dto.Status;

            await _context.SaveChangesAsync();

            if (dto.Status == "Booked")
            {
                _context.Leads.Where(l => l.LeadID == deal.LeadID).ToList().ForEach(l => l.Status = "Closed");
                await _context.SaveChangesAsync();

            }
            else if (dto.Status == "Cancelled")
            {
                _context.Leads.Where(l => l.LeadID == deal.LeadID).ToList().ForEach(l => l.Status = "Lost");
                await _context.SaveChangesAsync();
            }

            return new DealDto
            {
                DealID = deal.DealID,
                LeadID = deal.LeadID,
                UnitID = deal.UnitID,
                DealType = deal.DealType,
                AgreedValue = deal.AgreedValue,
                Status = deal.Status
            };
        }

        public async Task<bool> DeleteDeal(int id)
        {
            var deal = await _context.Deals.FindAsync(id);

            if (deal == null)
                return false;

            _context.Deals.Remove(deal);
            await _context.SaveChangesAsync();

            return true;
        }

    }
}
