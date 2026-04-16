using PropManageX.DTOs.DTOsLeadSalesLeasing.SiteVistDTOs;
using PropManageX.Models.Context;
using PropManageX.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace PropManageX.Services.LeadsSalesAndLeasingManagement.ServiceSiteVist
{
    public class SiteVistService : ISiteVistService
    {
        private readonly PropManageXContext _context;

       public SiteVistService(PropManageXContext context)
        {
            _context = context;
        }

        public async Task<List<SiteVisitDto>> GetAllVisits()
        {
            return await _context.SiteVisits
            .Select(v => new SiteVisitDto
            {
                VisitID = v.VisitID,
                LeadID = v.LeadID,
                VisitDate = v.VisitDate,
                AgentID = v.AgentID,
                Notes = v.Notes
            })
            .ToListAsync();
        }

        public async Task<SiteVisitDto> GetVisitById(int id)
        {
            var visit = await _context.SiteVisits.FindAsync(id);

            if (visit == null)
                return null;

            return new SiteVisitDto
            {
                VisitID = visit.VisitID,
                LeadID = visit.LeadID,
                VisitDate = visit.VisitDate,
                AgentID = visit.AgentID,
                Notes = visit.Notes
            };
        }

        public async Task<SiteVisitDto> CreateVisit(CreateSiteVisitDto dto)
        {
            var visit = new SiteVisitModel
            {
                LeadID = dto.LeadID,
                VisitDate = dto.VisitDate,
                AgentID = dto.AgentID,
                Notes = dto.Notes
            };

            _context.SiteVisits.Add(visit);
             await _context.SaveChangesAsync();

            var lead = await _context.Leads.FirstOrDefaultAsync(l => l.LeadID == dto.LeadID);
            if (lead != null)
            {
                lead.Status = "Visit Scheduled";
                await _context.SaveChangesAsync();
            }


            return new SiteVisitDto
            {
                VisitID = visit.VisitID,
                LeadID = visit.LeadID,
                VisitDate = visit.VisitDate,
                AgentID = visit.AgentID,
                Notes = visit.Notes
            };
        }

        public async Task<SiteVisitDto> UpdateVisit(int id, UpdateSiteVisitDto dto)
        {
            var visit = await _context.SiteVisits.FindAsync(id);

            if (visit == null)
                return null;

            visit.VisitDate = dto.VisitDate;
            visit.AgentID = dto.AgentID;
            visit.Notes = dto.Notes;

            await _context.SaveChangesAsync();

            return new SiteVisitDto
            {
                VisitID = visit.VisitID,
                LeadID = visit.LeadID,
                VisitDate = visit.VisitDate,
                AgentID = visit.AgentID,
                Notes = visit.Notes
            };
        }

        public async Task<bool> DeleteVisit(int id)
        {
            var visit = await _context.SiteVisits.FindAsync(id);

            if (visit == null)
                return false;

            _context.SiteVisits.Remove(visit);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
