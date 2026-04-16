namespace PropManageX.DTOs.DTOsMaintenanceRequestAndTenantOperation.MaintenanceDTOs
{
    public class MaintenanceRequestDto
    {
        public int RequestID { get; set; }
        public int UnitID { get; set; }
        public int TenantID { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public DateTime RaisedDate { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
    }
}
