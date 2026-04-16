using System.ComponentModel.DataAnnotations;

namespace PropManageX.DTOs.DTOsDocumentAndNotification.NotificationDTOs
{
    public class UpdateNotificationDto
    {
        [Required]
        public string Status { get; set; }
    }
}
