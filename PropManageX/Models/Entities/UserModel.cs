using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PropManageX.Models.Entities
{
    public class UserModel
    {
        [Key]
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }

        [NotMapped]
        public string ConfirmPassword { get; set; }

        public ICollection<MaintenanceRequestModel> MaintenanceRequests { get; set; }

        public ICollection<NotificationModel> Notifications { get; set; }
        }
}
