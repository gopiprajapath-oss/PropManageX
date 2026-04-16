using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PropManageX.Models.Entities
{
    public class MaintenanceRequestModel
    {
        [Key]
        public int RequestID { get; set; }

        [ForeignKey("UnitModel")]
        public int UnitID { get; set; }
        public UnitModel UnitModel { get; set; }

        [ForeignKey("Tenant")]
        public int TenantID { get; set; }
        public UserModel Tenant { get; set; }


        public string Category { get; set; }
        public string Description { get; set; }
        public DateTime RaisedDate { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }

        //Add relationships
        public ICollection<VendorAssignmentModel> VendorAssignments { get; set; }
    }
}
