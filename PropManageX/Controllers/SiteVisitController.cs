using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PropManageX.DTOs.DTOsLeadSalesLeasing.SiteVistDTOs;
using PropManageX.Services.LeadsSalesAndLeasingManagement.ServiceSiteVist;

namespace PropManageX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SiteVisitController : ControllerBase
    {
        private readonly ISiteVistService _visitService;

        public SiteVisitController(ISiteVistService visitService)
        {
            _visitService = visitService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllVisits()
        {
            var visits = await _visitService.GetAllVisits();
            return Ok(visits);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVisitById(int id)
        {
            var visit = await _visitService.GetVisitById(id);

            if (visit == null)
                return NotFound("Visit not found");

            return Ok(visit);
        }

        [Authorize(Roles = "Admin,Agent")]
        [HttpPost]
        public async Task<IActionResult> CreateVisit(CreateSiteVisitDto dto)
        {
            var visit = await _visitService.CreateVisit(dto);
            return Ok(visit);
        }

        [Authorize(Roles = "Admin,Agent")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVisit(int id, UpdateSiteVisitDto dto)
        {
            var visit = await _visitService.UpdateVisit(id, dto);

            if (visit == null)
                return NotFound("Visit not found");

            return Ok(visit);
        }

        [Authorize(Roles = "Admin,Agent")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVisit(int id)
        {
            var result = await _visitService.DeleteVisit(id);

            if (!result)
                return NotFound("Visit not found");

            return Ok("Visit deleted successfully");
        }

    }
}
