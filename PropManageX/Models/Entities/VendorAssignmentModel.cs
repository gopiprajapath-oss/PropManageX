using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PropManageX.Models.Entities
{
    public class VendorAssignmentModel
    {
        [Key]
        public int AssignmentID { get; set; }
        [ForeignKey("MaintenanceRequestModel")]
        public int RequestID { get; set; }

        public MaintenanceRequestModel MaintenanceRequestModel { get; set; }

        public string VendorName { get; set; }
        public DateTime AssignedDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public decimal Cost { get; set; }

        //Add Relationships

    }
}
