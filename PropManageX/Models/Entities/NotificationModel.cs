using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PropManageX.Models.Entities
{
    public class NotificationModel
    {
        [Key]
        public int NotificationID { get; set; }

        [ForeignKey("UserModel")]
        public int UserID { get; set; }

        public UserModel UserModel { get; set; }

        public string Message { get; set; }
        public string Category { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }

        //Add relation many notifications one user
    }
}
