namespace PropManageX.DTOs.DTOsMaintenanceRequestAndTenantOperation.VendorAssignmentDTOs
{
    public class VendorAssignmentDto
    {
        public int AssignmentID { get; set; }

        public int RequestID { get; set; }

        public string VendorName { get; set; }

        public DateTime AssignedDate { get; set; }

        public DateTime? CompletionDate { get; set; }

        public decimal? Cost { get; set; }
    }
}
