using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PropManageX.DTOs.DTOsContractsLeasesRenewal.Renewal;
using PropManageX.Services.ContractsLeasesRenewal.Renewal;

namespace PropManageX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RenewalController : ControllerBase
    {
        private readonly IRenewalService _renewalService;

        public RenewalController(IRenewalService renewalService)
        {
            _renewalService = renewalService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllRenewals()
        {
            var renewals = await _renewalService.GetAllRenewals();
            return Ok(renewals);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRenewalById(int id)
        {
            var renewal = await _renewalService.GetRenewalById(id);

            if (renewal == null)
                return NotFound("Renewal not found");

            return Ok(renewal);
        }

        [Authorize(Roles = "Agent,Admin , Manager")]
        [HttpPost("renewcontract")]
        public async Task<IActionResult> CreateRenewal(CreateRenewalDto dto)
        {
            var renewal = await _renewalService.CreateRenewal(dto);
            if (renewal == null)
            {
                return BadRequest($"{dto.ContractID} contract not found or still contract is Active");
            }
            return Ok(renewal);
        }

        [Authorize(Roles = "Admin , Tenant")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRenewal(int id, UpdateRenewalDto dto)
        {
            var renewal = await _renewalService.UpdateRenewal(id, dto);

            if (renewal == null)
                return NotFound("Renewal not found");

            return Ok(renewal);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRenewal(int id)
        {
            var result = await _renewalService.DeleteRenewal(id);

            if (!result)
                return NotFound("Renewal not found");

            return Ok("Renewal deleted successfully");
        }
    }
}
