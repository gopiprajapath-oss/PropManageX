using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PropManageX.DTOs.DTOsLeadSalesLeasing.LeadDTOs;
using PropManageX.Services.LeadsSalesAndLeasingManagement.ServiceLead;

namespace PropManageX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeadController : ControllerBase
    {
        private readonly ILeadService _leadService;

        public LeadController(ILeadService leadService)
        {
            _leadService = leadService;
        }

        [Authorize(Roles = "Admin , Agent")]
        [HttpGet]
        public async Task<IActionResult> GetAllLeads()
        {
            var leads = await _leadService.GetAllLeads();
            return Ok(leads);
        }

        [Authorize(Roles = "Admin , Agent")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLeadById(int id)
        {
            var lead = await _leadService.GetLeadById(id);

            if (lead == null)
                return NotFound("Lead not found");

            return Ok(lead);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateLead(CreateLeadDto dto)
        {
            var lead = await _leadService.CreateLead(dto);
            
            if(lead != null)
            {
                return Ok(lead);
            }
            return BadRequest("Invalid Email");

        }

        [Authorize(Roles = "Admin , Agent")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLead(int id, UpdateLeadDto dto)
        {
            var lead = await _leadService.UpdateLead(id, dto);

            if (lead == null)
                return NotFound("Lead not found");

            return Ok(lead);
        }

        [Authorize(Roles = "Admin , Agent")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLead(int id)
        {
            var result = await _leadService.DeleteLead(id);

            if (!result)
                return NotFound("Lead not found");

            return Ok("Lead deleted successfully");
        }

    }
}
