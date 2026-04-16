using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PropManageX.DTOs.DTOsMaintenanceRequestAndTenantOperation.VendorAssignmentDTOs;
using PropManageX.Services.TenantOperationsAndMaintainenceRequest.VendorAssignment;

namespace RealEstate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorAssignmentController : ControllerBase
    {
        private readonly IVendorAssignmentService _service;

        public VendorAssignmentController(IVendorAssignmentService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Manager")]
        [HttpPost]
        public async Task<IActionResult> AssignVendor(CreateVendorAssignmentDto dto)
        {
            var assignment = await _service.AssignVendor(dto);
            return Ok(assignment);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAssignments()
        {
            var assignments = await _service.GetAssignments();
            return Ok(assignments);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAssignmentById(int id)
        {
            var assignment = await _service.GetAssignmentById(id);

            if (assignment == null)
                return NotFound("Assignment not found");

            return Ok(assignment);
        }

        [Authorize(Roles = "Manager")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAssignment(int id, UpdateVendorAssignmentDto dto)
        {
            var assignment = await _service.UpdateAssignment(id, dto);

            if (assignment == null)
                return NotFound("Assignment not found");

            return Ok(assignment);
        }

        [Authorize(Roles = "Manager")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssignment(int id)
        {
            var result = await _service.DeleteAssignment(id);

            if (!result)
                return NotFound("Assignment not found");

            return Ok("Assignment deleted successfully");
        }
    }
}
