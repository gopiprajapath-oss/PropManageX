using PropManageX.DTOs.DTOsMaintenanceRequestAndTenantOperation.MaintenanceDTOs;
using PropManageX.Models.Context;
using PropManageX.Models.Entities;
using Microsoft.EntityFrameworkCore;
namespace PropManageX.Services.TenantOperationsAndMaintainenceRequest.MaintenanceRequest
{
    public class MaintenanceRequestService : IMaintenanceRequestService
    {
        private readonly PropManageXContext _context;

        public MaintenanceRequestService(PropManageXContext context)
        {
            _context = context;
        }

        public async Task<MaintenanceRequestDto> CreateRequest(CreateMaintenanceRequestDto dto)
        {
            var request = new MaintenanceRequestModel
            {
                UnitID = dto.UnitID,
                TenantID = dto.TenantID,
                Category = dto.Category,
                Description = dto.Description,
                Priority = dto.Priority,
                RaisedDate = DateTime.Now,
                Status = "Open"
            };

            _context.MaintenanceRequests.Add(request);
            await _context.SaveChangesAsync();

            return new MaintenanceRequestDto
            {
                RequestID = request.RequestID,
                UnitID = request.UnitID,
                TenantID = request.TenantID,
                Category = request.Category,
                Description = request.Description,
                RaisedDate = request.RaisedDate,
                Priority = request.Priority,
                Status = request.Status
            };
        }

        public async Task<List<MaintenanceRequestDto>> GetAllRequests()
        {
            return await _context.MaintenanceRequests
            .Select(r => new MaintenanceRequestDto
            {
                RequestID = r.RequestID,
                UnitID = r.UnitID,
                TenantID = r.TenantID,
                Category = r.Category,
                Description = r.Description,
                RaisedDate = r.RaisedDate,
                Priority = r.Priority,
                Status = r.Status
            }).ToListAsync();
        }

        public async Task<MaintenanceRequestDto> GetRequestById(int id)
        {
            var request = await _context.MaintenanceRequests.FindAsync(id);

            if (request == null)
                return null;

            return new MaintenanceRequestDto
            {
                RequestID = request.RequestID,
                UnitID = request.UnitID,
                TenantID = request.TenantID,
                Category = request.Category,
                Description = request.Description,
                RaisedDate = request.RaisedDate,
                Priority = request.Priority,
                Status = request.Status
            };
        }

        public async Task<MaintenanceRequestDto> UpdateRequestStatus(int id, UpdateMaintenanceRequestDto dto)
        {
            var request = await _context.MaintenanceRequests.FindAsync(id);

            if (request == null)
                return null;

            request.Status = dto.Status;

            await _context.SaveChangesAsync();

            return new MaintenanceRequestDto
            {
                RequestID = request.RequestID,
                UnitID = request.UnitID,
                TenantID = request.TenantID,
                Category = request.Category,
                Description = request.Description,
                RaisedDate = request.RaisedDate,
                Priority = request.Priority,
                Status = request.Status
            };
        }

        public async Task<bool> DeleteRequest(int id)
        {
            var request = await _context.MaintenanceRequests.FindAsync(id);

            //if (request == null)
            //    return false;

            _context.MaintenanceRequests.Remove(request);
            await _context.SaveChangesAsync();

            return true;
        }





        public async Task<List<MaintenanceRequestDto>> GetMyRequests(string email)
{
    // 1. Find the logged-in user by their email
    var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    
    if (user == null) 
        return new List<MaintenanceRequestDto>();

    // 2. Filter the maintenance requests where TenantID matches this user's ID
    return await _context.MaintenanceRequests
        .Where(m => m.TenantID == user.UserID)
        .Select(r => new MaintenanceRequestDto
        {
            RequestID = r.RequestID,
            UnitID = r.UnitID,
            TenantID = r.TenantID,
            Category = r.Category,
            Description = r.Description,
            RaisedDate = r.RaisedDate,
            Priority = r.Priority,
            Status = r.Status
        }).ToListAsync();
}





    }
}
