using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PropManageX.DTOs.DTOsMaintenanceRequestAndTenantOperation.MaintenanceDTOs;
using PropManageX.Services.TenantOperationsAndMaintainenceRequest.MaintenanceRequest;
using System.Security.Claims;

namespace RealEstate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaintenanceRequestController : ControllerBase
    {
        private readonly IMaintenanceRequestService _service;

        public MaintenanceRequestController(IMaintenanceRequestService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateRequest(CreateMaintenanceRequestDto dto)
        {
            var result = await _service.CreateRequest(dto);
            return Ok(result);
        }

        [Authorize(Roles ="Admin,Manager")]
        [HttpGet]
        public async Task<IActionResult> GetAllRequests()
        {
            var requests = await _service.GetAllRequests();
            return Ok(requests);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRequestById(int id)
        {
            var request = await _service.GetRequestById(id);

            if (request == null)
                return NotFound("Request not found");

            return Ok(request);
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStatus(int id, UpdateMaintenanceRequestDto dto)
        {
            var request = await _service.UpdateRequestStatus(id, dto);

            if (request == null)
                return NotFound("Request not found");

            return Ok(request);
        }

        [Authorize(Roles = "Admin , Manager")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequest(int id)
        {
            var result = await _service.DeleteRequest(id);

            if (!result)
                return NotFound("Request not found");

            return Ok("Request deleted successfully");
        }






        [Authorize] // Allows any logged-in user
        [HttpGet("my-requests")]
        public async Task<IActionResult> GetMyRequests()
        {
            // Safely extract the email from the logged-in user's JWT token
            var email = User.FindFirstValue(ClaimTypes.Email);

            if (string.IsNullOrEmpty(email))
            {
                return Unauthorized("Email claim not found in token.");
            }

            var requests = await _service.GetMyRequests(email);
            return Ok(requests);
        }





    }
}
