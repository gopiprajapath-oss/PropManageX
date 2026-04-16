using PropManageX.DTOs.DTOsMaintenanceRequestAndTenantOperation.VendorAssignmentDTOs;

namespace PropManageX.Services.TenantOperationsAndMaintainenceRequest.VendorAssignment
{
    public interface IVendorAssignmentService
    {
        Task<VendorAssignmentDto> AssignVendor(CreateVendorAssignmentDto dto);

        Task<List<VendorAssignmentDto>> GetAssignments();

        Task<VendorAssignmentDto> GetAssignmentById(int id);

        Task<VendorAssignmentDto> UpdateAssignment(int id, UpdateVendorAssignmentDto dto);

        Task<bool> DeleteAssignment(int id);
    }
}
