using System.ComponentModel.DataAnnotations;

namespace PropManageX.DTOs.DTOsMaintenanceRequestAndTenantOperation.MaintenanceDTOs
{
    public class UpdateMaintenanceRequestDto
    {
        [Required]
        public string Status { get; set; }
    }
}
