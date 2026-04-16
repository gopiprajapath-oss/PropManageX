using System.ComponentModel.DataAnnotations;

namespace PropManageX.DTOs.DTOsMaintenanceRequestAndTenantOperation.VendorAssignmentDTOs
{
    public class CreateVendorAssignmentDto
    {
        [Required]
        public int RequestID { get; set; }

        [Required]
        public string VendorName { get; set; }

        [Required]
        public decimal Cost { get; set; } = 1000;
    }
}
