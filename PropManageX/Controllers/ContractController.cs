using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PropManageX.DTOs.DTOsContractsLeasesRenewal.ContractDTOs;
using PropManageX.Services.ContractsLeasesRenewal.Contracts;
using System.Security.Claims;

namespace PropManageX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractController : ControllerBase
    {
        private readonly IContractService _contractService;

        public ContractController(IContractService contractService)
        {
            _contractService = contractService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllContracts()
        {
            var contracts = await _contractService.GetAllContracts();
            return Ok(contracts);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContractById(int id)
        {
            var contract = await _contractService.GetContractById(id);

            if (contract == null)
                return NotFound("Contract not found");

            return Ok(contract);
        }

        [Authorize(Roles = "Agent,Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateContract(CreateContractDto createdto )
        { 
            var contract = await _contractService.CreateContract(createdto);
            if(contract == null)
            {
                return BadRequest($"Contract for {createdto.DealID} alredy exisit");
            }
            return Ok(contract);
        }

        [Authorize(Roles = "Agent,Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContract(int id, UpdateContractDto dto)
        {
            var contract = await _contractService.UpdateContract(id, dto);

            if (contract == null)
                return NotFound("Contract not found");

            return Ok(contract);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContract(int id)
        {
            var result = await _contractService.DeleteContract(id);

            if (!result)
                return NotFound("Contract not found");

            return Ok("Contract deleted successfully");
        }


        // ... inside your ContractController class ...




        [Authorize]
        [HttpGet("my-overview")]
        public async Task<IActionResult> GetMyOverview()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            if (string.IsNullOrEmpty(email))
                return Unauthorized("User email not found in token.");

            var overview = await _contractService.GetMyOverview(email);

            // Returns 204 No Content if they don't have a contract yet, or 200 OK with data
            if (overview == null) return NoContent();

            return Ok(overview);
        }

        // ==========================================
        // NEW ENDPOINTS FOR TENANTS
        // ==========================================

        [Authorize]
        [HttpGet("my-contracts")]
        public async Task<IActionResult> GetMyContracts()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrEmpty(email)) return Unauthorized("User email not found in token.");

            var contracts = await _contractService.GetMyContracts(email);
            return Ok(contracts);
        }

        [Authorize]
        [HttpPut("sign/{id}")]
        public async Task<IActionResult> SignContract(int id)
        {
            var success = await _contractService.SignContract(id);

            if (!success)
                return NotFound(new { message = "Contract not found" });

            return Ok(new { message = "Agreement Signed Successfully!" });
        }

    }
}
