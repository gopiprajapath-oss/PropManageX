namespace PropManageX.DTOs.DTOsMaintenanceRequestAndTenantOperation.VendorAssignmentDTOs
{
    public class UpdateVendorAssignmentDto
    {

        public  string VendorName { get; set; }
        public DateTime CompletionDate { get; set; }

        public decimal Cost { get; set; }
    }
}
