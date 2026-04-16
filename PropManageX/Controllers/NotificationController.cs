using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PropManageX.DTOs.DTOsDocumentAndNotification.NotificationDTOs;
using PropManageX.Services.DocumentsAndComplianceManagement.Notification;

namespace PropManageX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _service;

        public NotificationController(INotificationService service)
        {
            _service = service;
        }

        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateNotification(CreateNotificationDto dto)
        {
            var notification = await _service.CreateNotification(dto);
            return Ok(notification);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetNotifications()
        {
            var notifications = await _service.GetNotifications();
            return Ok(notifications);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNotificationById(int id)
        {
            var notification = await _service.GetNotificationById(id);

            if (notification == null)
                return NotFound("Notification not found");

            return Ok(notification);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> MarkAsRead(int id, UpdateNotificationDto dto)
        {
            var notification = await _service.MarkAsRead(id, dto);

            if (notification == null)
                return NotFound("Notification not found");

            return Ok(notification);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotification(int id)
        {
            var result = await _service.DeleteNotification(id);

            if (!result)
                return NotFound("Notification not found");

            return Ok("Notification deleted successfully");
        }

    }
}
