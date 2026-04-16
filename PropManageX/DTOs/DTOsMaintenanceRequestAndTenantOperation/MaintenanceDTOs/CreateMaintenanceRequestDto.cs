using System.ComponentModel.DataAnnotations;

namespace PropManageX.DTOs.DTOsMaintenanceRequestAndTenantOperation.MaintenanceDTOs
{
    public class CreateMaintenanceRequestDto
    {
        [Required]
        public int UnitID { get; set; }

        [Required]
        public int TenantID { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Priority { get; set; }
    }
}
