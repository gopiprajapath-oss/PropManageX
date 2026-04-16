using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PropManageX.DTOs.DTOsLeadSalesLeasing.DealDTOs;
using PropManageX.Services.LeadsSalesAndLeasingManagement.ServiceDeal;

namespace PropManageX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DealController : ControllerBase
    {
        private readonly IDealService _dealService;

        public DealController(IDealService dealService)
        {
            _dealService = dealService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllDeals()
        {
            var deals = await _dealService.GetAllDeals();
            return Ok(deals);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDealById(int id)
        {
            var deal = await _dealService.GetDealById(id);

            if (deal == null)
                return NotFound("Deal not found");

            return Ok(deal);
        }

        [Authorize(Roles = "Agent,Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateDeal(CreateDealDto dto)
        {
            var deal = await _dealService.CreateDeal(dto);
            if(deal != null)
            {
                return Ok(deal);
            }
            return BadRequest("LeadID or UnitId is Invalid");
            
        }

        [Authorize(Roles = "Agent,Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDeal(int id, UpdateDealDto dto)
        {
            var deal = await _dealService.UpdateDeal(id, dto);

            if (deal == null)
                return NotFound("Deal not found");

            return Ok(deal);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeal(int id)
        {
            var result = await _dealService.DeleteDeal(id);

            if (!result)
                return NotFound("Deal not found");

            return Ok("Deal deleted successfully");
        }

    }
}
