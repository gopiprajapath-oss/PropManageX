using PropManageX.DTOs.DTOsDocumentAndNotification.NotificationDTOs;
using PropManageX.Models.Context;
using PropManageX.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace PropManageX.Services.DocumentsAndComplianceManagement.Notification
{
    public class NotificationService : INotificationService
    {
        private readonly PropManageXContext  _context;

        public NotificationService(PropManageXContext context)
        {
            _context = context;
        }

        public async Task<NotificationDto> CreateNotification(CreateNotificationDto dto)
        {
            var notification = new NotificationModel
            {
                UserID = dto.UserID,
                Message = dto.Message,
                Category = dto.Category,
                Status = "Unread",
                CreatedDate = DateTime.Now
            };

            _context.Notifications.Add(notification);

            await _context.SaveChangesAsync();

            return new NotificationDto
            {
                NotificationID = notification.NotificationID,
                UserID = notification.UserID,
                Message = notification.Message,
                Category = notification.Category,
                Status = notification.Status,
                CreatedDate = notification.CreatedDate
            };
        }

        public async Task<List<NotificationDto>> GetNotifications()
        {
            return await _context.Notifications
            .Select(n => new NotificationDto
            {
                NotificationID = n.NotificationID,
                UserID = n.UserID,
                Message = n.Message,
                Category = n.Category,
                Status = n.Status,
                CreatedDate = n.CreatedDate
            }).ToListAsync();
        }

        public async Task<NotificationDto> GetNotificationById(int id)
        {
            var notification = await _context.Notifications.FindAsync(id);

            if (notification == null)
                return null;

            return new NotificationDto
            {
                NotificationID = notification.NotificationID,
                UserID = notification.UserID,
                Message = notification.Message,
                Category = notification.Category,
                Status = notification.Status,
                CreatedDate = notification.CreatedDate
            };
        }

        public async Task<NotificationDto> MarkAsRead(int id, UpdateNotificationDto dto)
        {
            var notification = await _context.Notifications.FindAsync(id);

            if (notification == null)
                return null;

            notification.Status = dto.Status;

            await _context.SaveChangesAsync();

            return new NotificationDto
            {
                NotificationID = notification.NotificationID,
                UserID = notification.UserID,
                Message = notification.Message,
                Category = notification.Category,
                Status = notification.Status,
                CreatedDate = notification.CreatedDate
            };
        }

        public async Task<bool> DeleteNotification(int id)
        {
            var notification = await _context.Notifications.FindAsync(id);

            if (notification == null)
                return false;

            _context.Notifications.Remove(notification);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
