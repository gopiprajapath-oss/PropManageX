using PropManageX.DTOs.DTOsMaintenanceRequestAndTenantOperation.VendorAssignmentDTOs;
using PropManageX.Models.Context;
using PropManageX.Models.Entities;
using Microsoft.EntityFrameworkCore;
namespace PropManageX.Services.TenantOperationsAndMaintainenceRequest.VendorAssignment
{
    public class VendorAssignmentService : IVendorAssignmentService
    {
        private readonly PropManageXContext _context;

        public VendorAssignmentService(PropManageXContext context)
        {
            _context = context;
        }

        public async Task<VendorAssignmentDto> AssignVendor(CreateVendorAssignmentDto dto)
        {
            var request = await _context.MaintenanceRequests.FindAsync(dto.RequestID);

            if (request == null)
                throw new Exception("Maintenance request not found");

            var assignment = new VendorAssignmentModel
            {
                RequestID = dto.RequestID,
                VendorName = dto.VendorName,
                AssignedDate = DateTime.Now,
                Cost = dto.Cost
            };

            _context.VendorAssignments.Add(assignment);

            request.Status = "Assigned";

            await _context.SaveChangesAsync();

            return new VendorAssignmentDto
            {
                AssignmentID = assignment.AssignmentID,
                RequestID = assignment.RequestID,
                VendorName = assignment.VendorName,
                AssignedDate = assignment.AssignedDate,
                CompletionDate = assignment.CompletionDate,
                Cost = assignment.Cost
            };
        }

        public async Task<List<VendorAssignmentDto>> GetAssignments()
        {
            return await _context.VendorAssignments
            .Select(a => new VendorAssignmentDto
            {
                AssignmentID = a.AssignmentID,
                RequestID = a.RequestID,
                VendorName = a.VendorName,
                AssignedDate = a.AssignedDate,
                CompletionDate = a.CompletionDate,
                Cost = a.Cost
            }).ToListAsync();
        }

        public async Task<VendorAssignmentDto> GetAssignmentById(int id)
        {
            var assignment = await _context.VendorAssignments.FindAsync(id);

            if (assignment == null)
                return null;

            return new VendorAssignmentDto
            {
                AssignmentID = assignment.AssignmentID,
                RequestID = assignment.RequestID,
                VendorName = assignment.VendorName,
                AssignedDate = assignment.AssignedDate,
                CompletionDate = assignment.CompletionDate,
                Cost = assignment.Cost
            };
        }

        public async Task<VendorAssignmentDto> UpdateAssignment(int id, UpdateVendorAssignmentDto dto)
        {
            var assignment = await _context.VendorAssignments.FindAsync(id);

            if (assignment == null)
                return null;

            // 1. Only update the specific fields passed from the frontend
            assignment.VendorName = dto.VendorName;
            assignment.CompletionDate = dto.CompletionDate;
            assignment.Cost = dto.Cost;

            // (Optional) If your DTO and Model have a Status field for the VENDOR specifically:
            // assignment.Status = dto.Status; 

            // ====================================================================
            // 🛑 DELETE OR COMMENT OUT THIS ENTIRE BLOCK. THIS WAS CAUSING BUG 1 & 2
            // var request = await _context.MaintenanceRequests.FindAsync(assignment.RequestID);
            // if (request != null)
            //     request.Status = "Completed";
            // ====================================================================

            await _context.SaveChangesAsync();

            return new VendorAssignmentDto
            {
                AssignmentID = assignment.AssignmentID,
                RequestID = assignment.RequestID,
                VendorName = assignment.VendorName,
                AssignedDate = assignment.AssignedDate,
                CompletionDate = assignment.CompletionDate,
                Cost = assignment.Cost
                // Status = assignment.Status 
            };
        }

        public async Task<bool> DeleteAssignment(int id)
        {
            var assignment = await _context.VendorAssignments.FindAsync(id);

            if (assignment == null)
                return false;

            _context.VendorAssignments.Remove(assignment);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
