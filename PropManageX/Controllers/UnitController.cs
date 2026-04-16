using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PropManageX.DTOs.PropertyDTOs.UnitsDTOs;
using PropManageX.Services.PropertyListingInventoryManagement.UnitService.UnitService;
using System.Security.Claims;

namespace PropManageX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitController : ControllerBase
    {
        private readonly IUnitService _unitService;

        public UnitController(IUnitService unitService)
        {
            _unitService = unitService;
        }

        // View all units (Any logged in user)
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllUnits()
        {
            var units = await _unitService.GetAllUnits();
            return Ok(units);
        }


        [HttpGet("property/{propertyId}")]
        public async Task<IActionResult> GetUnitsByPropertyId(int propertyId)
        {
            var units = await _unitService.GetUnitsByPropertyId(propertyId);
            return Ok(units);
        }


        // View unit by id
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUnitById(int id)
        {
            var unit = await _unitService.GetUnitById(id);

            if (unit == null)
                return NotFound("Unit not found");

            return Ok(unit);
        }

        // Create unit (Admin only)
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateUnit(CreateUnitDto dto)
        {
            var unit = await _unitService.CreateUnit(dto);
            if (unit == null)
            {
                return BadRequest("Limit Exceeded for this property");
            }

            return Ok(unit);
        }

        // Update unit (Admin only)
        [Authorize(Roles = "Admin , Agent")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUnit(int id, UpdateUnitDto dto)
        {
            var unit = await _unitService.UpdateUnit(id, dto);

            if (unit == null)
                return NotFound("Unit not found");

            return Ok(unit);
        }

        // Delete unit (Admin only)
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUnit(int id)
        {
            var result = await _unitService.DeleteUnit(id);

            if (!result)
                return NotFound("Unit not found");

            return Ok("Unit deleted successfully");
        }


        [Authorize]
        [HttpGet("my-units")]
        public async Task<IActionResult> GetMyUnits()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrEmpty(email))
                return Unauthorized("User email not found in token.");

            var units = await _unitService.GetMyUnits(email);
            return Ok(units);
        }

    }
}