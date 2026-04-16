using PropManageX.DTOs.DTOsDocumentAndNotification.NotificationDTOs;

namespace PropManageX.Services.DocumentsAndComplianceManagement.Notification
{
    public interface INotificationService
    {
        Task<NotificationDto> CreateNotification(CreateNotificationDto dto);

        Task<List<NotificationDto>> GetNotifications();

        Task<NotificationDto> GetNotificationById(int id);

        Task<NotificationDto> MarkAsRead(int id, UpdateNotificationDto dto);

        Task<bool> DeleteNotification(int id);
    }
}
