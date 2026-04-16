using PropManageX.DTOs.DTOsLeadSalesLeasing.LeadDTOs;
using PropManageX.Models.Context;
using PropManageX.Models.Entities;
using Microsoft.EntityFrameworkCore;   

namespace PropManageX.Services.LeadsSalesAndLeasingManagement.ServiceLead
{
    public class LeadService : ILeadService
    {
        private readonly PropManageXContext _context;

        public LeadService(PropManageXContext context)
        {
            _context = context;
        }

        public async Task<List<LeadDto>> GetAllLeads()
        {
            return await _context.Leads
            .Select(l => new LeadDto
            {
                LeadID = l.LeadID,
                PropertyID = l.PropertyID,
                CustomerName = l.CustomerName,
                ContactInfo = l.ContactInfo,
                InterestType = l.InterestType,
                CreatedDate = l.CreatedDate,
                Status = l.Status
            }).ToListAsync();
        }

        public async Task<LeadDto> GetLeadById(int id)
        {
            var lead = await _context.Leads.FindAsync(id);

            if (lead == null)
                return null;

            return new LeadDto
            {
                LeadID = lead.LeadID,
                PropertyID = lead.PropertyID,
                CustomerName = lead.CustomerName,
                ContactInfo = lead.ContactInfo,
                InterestType = lead.InterestType,
                CreatedDate = lead.CreatedDate,
                Status = lead.Status
            };
        }

        public async Task<LeadDto> CreateLead(CreateLeadDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email.ToLower() == dto.ContactInfo.ToLower());
            if (user == null)
            {
                return null;
            }
            var lead = new LeadModel
            {
                PropertyID = dto.PropertyID,
                CustomerName = dto.CustomerName,
                ContactInfo = dto.ContactInfo,
                InterestType = dto.InterestType,
                CreatedDate = DateTime.Now,
                Status = "New"
            };

            _context.Leads.Add(lead);
            await _context.SaveChangesAsync();

            return new LeadDto
            {
                LeadID = lead.LeadID,
                PropertyID = lead.PropertyID,
                CustomerName = lead.CustomerName,
                ContactInfo = lead.ContactInfo,
                InterestType = lead.InterestType,
                CreatedDate = lead.CreatedDate,
                Status = lead.Status
            };
        }

        public async Task<LeadDto> UpdateLead(int id, UpdateLeadDto dto)
        {
            var lead = await _context.Leads.FindAsync(id);

            if (lead == null)
                return null;

            lead.CustomerName = dto.CustomerName;
            lead.ContactInfo = dto.ContactInfo;
            lead.InterestType = dto.InterestType;
            lead.Status = dto.Status;

            await _context.SaveChangesAsync();

            return new LeadDto
            {
                LeadID = lead.LeadID,
                PropertyID = lead.PropertyID,
                CustomerName = lead.CustomerName,
                ContactInfo = lead.ContactInfo,
                InterestType = lead.InterestType,
                CreatedDate = lead.CreatedDate,
                Status = lead.Status
            };
        }

        public async Task<bool> DeleteLead(int id)
        {
            var lead = await _context.Leads.FindAsync(id);

            if (lead == null)
                return false;

            _context.Leads.Remove(lead);
            await _context.SaveChangesAsync();

            return true;
        }

    }
}
