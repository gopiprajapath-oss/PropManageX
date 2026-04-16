using PropManageX.DTOs.DTOsMaintenanceRequestAndTenantOperation.MaintenanceDTOs;

namespace PropManageX.Services.TenantOperationsAndMaintainenceRequest.MaintenanceRequest
{
    public interface IMaintenanceRequestService
    {
        Task<MaintenanceRequestDto> CreateRequest(CreateMaintenanceRequestDto dto);
        Task<List<MaintenanceRequestDto>> GetAllRequests();
        Task<MaintenanceRequestDto> GetRequestById(int id);
        Task<MaintenanceRequestDto> UpdateRequestStatus(int id, UpdateMaintenanceRequestDto dto);
        Task<bool> DeleteRequest(int id);

        // In IMaintenanceRequestService.cs
        Task<List<MaintenanceRequestDto>> GetMyRequests(string email);
    }
}
