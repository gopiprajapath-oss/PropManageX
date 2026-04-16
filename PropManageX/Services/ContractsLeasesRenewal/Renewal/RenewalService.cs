using Microsoft.EntityFrameworkCore;
using PropManageX.DTOs.DTOsContractsLeasesRenewal.Renewal;
using PropManageX.DTOs.DTOsDocumentAndNotification.NotificationDTOs;
using PropManageX.Models.Context;
using PropManageX.Models.Entities;
using PropManageX.Services.DocumentsAndComplianceManagement.Notification;
using System.Diagnostics.Contracts;

namespace PropManageX.Services.ContractsLeasesRenewal.Renewal
{
    public class RenewalService: IRenewalService
    {
        private readonly PropManageXContext _context;
        private readonly INotificationService _notificationService;

        public RenewalService(PropManageXContext context, INotificationService notificationService)
        {
            _context = context;
            _notificationService = notificationService;
        }


        public async Task<List<RenewalDto>> GetAllRenewals()
        {
            return await _context.Renewals
            .Select(r => new RenewalDto
            {
                RenewalID = r.RenewalID,
                ContractID = r.ContractID,
                ProposedValue = r.ProposedValue,
                ProposedEndDate = r.ProposedEndDate,
                Status = r.Status
            })
            .ToListAsync();
        }

        public async Task<RenewalDto> GetRenewalById(int id)
        {
            var renewal = await _context.Renewals.FindAsync(id);

            if (renewal == null)
                return null;

            return new RenewalDto
            {
                RenewalID = renewal.RenewalID,
                ContractID = renewal.ContractID,
                ProposedValue = renewal.ProposedValue,
                ProposedEndDate = renewal.ProposedEndDate,
                Status = renewal.Status
            };
        }

        //public async Task<RenewalDto> CreateRenewal(CreateRenewalDto dto)
        //{
        //    var contract = await _context.Contracts.FirstOrDefaultAsync(c => c.ContractID == dto.ContractID);
        //    if (contract == null || contract.Status != "Expired")
        //    {
        //        return null; // Or throw an exception, depending on your error handling strategy
        //    }

        //    var renewal = new RenewalModel
        //    {
        //        ContractID = dto.ContractID,
        //        ProposedValue = dto.ProposedValue,
        //        ProposedEndDate = dto.ProposedEndDate,
        //        Status = "Offered"
        //    };
        //    _context.Renewals.Add(renewal);
        //    await _context.SaveChangesAsync();

        //    return new RenewalDto
        //    {
        //        RenewalID = renewal.RenewalID,
        //        ContractID = renewal.ContractID,
        //        ProposedEndDate = renewal.ProposedEndDate,
        //        ProposedValue = renewal.ProposedValue,
        //        Status = renewal.Status
        //    };
        //}

        public async Task<RenewalDto> CreateRenewal(CreateRenewalDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto), "Invalid request data");

            var contract = await _context.Contracts
                .Include(c => c.Deal)
                .ThenInclude(d => d.LeadModel)
                .FirstOrDefaultAsync(c => c.ContractID == dto.ContractID);

            if (contract == null)
                throw new Exception($"Contract {dto.ContractID} not found");

            // Allow Active + Expired
            if (contract.Status != "Expired" && contract.Status != "Active")
                throw new Exception("Only Active or Expired contracts allowed");

            if (dto.ProposedEndDate == default)
                throw new Exception("End date required");

            var renewal = new RenewalModel
            {
                ContractID = dto.ContractID,
                ProposedValue = dto.ProposedValue > 0 ? dto.ProposedValue : contract.ContractValue,
                ProposedEndDate = dto.ProposedEndDate,
                Status = "Offered"
            };

            _context.Renewals.Add(renewal);
            await _context.SaveChangesAsync();

            // Resolve tenant from Lead → User
            var customerName = contract.Deal?.LeadModel?.CustomerName;
            if (!string.IsNullOrEmpty(customerName))
            {
                var tenant = await _context.Users.FirstOrDefaultAsync(u => u.Name == customerName);
                if (tenant != null)
                {
                    await _notificationService.CreateNotification(new CreateNotificationDto
                    {
                        UserID = tenant.UserID,
                        Message = $"New renewal offer for Contract #{contract.ContractID}",
                        Category = "Contract"
                    });
                }
            }

            return new RenewalDto
            {
                RenewalID = renewal.RenewalID,
                ContractID = renewal.ContractID,
                ProposedValue = renewal.ProposedValue,
                ProposedEndDate = renewal.ProposedEndDate,
                Status = renewal.Status
            };
        }


        public async Task<RenewalDto> UpdateRenewal(int id, UpdateRenewalDto dto)
        {
            {
                var renewal = await _context.Renewals.FindAsync(id);

                if (renewal == null)
                    return null;

                if (dto.Status != "Accepted" && dto.Status != "Declined" && dto.Status != "Offered")
                    throw new Exception("Invalid status");

                if (renewal.Status == "Accepted" && dto.Status == "Accepted")
                    throw new Exception("Already accepted");

                renewal.ProposedValue = dto.ProposedValue;
                renewal.ProposedEndDate = dto.ProposedEndDate;
                renewal.Status = dto.Status;

                var contract = await _context.Contracts
                    .FirstOrDefaultAsync(c => c.ContractID == renewal.ContractID);

                if (contract == null)
                    throw new Exception("Contract not found");

                if (renewal.Status == "Accepted")
                {
                    contract.Status = "Active";
                    contract.StartDate = DateTime.Now;
                    contract.EndDate = renewal.ProposedEndDate;
                    contract.ContractValue = renewal.ProposedValue;
                }

                await _context.SaveChangesAsync();

                return new RenewalDto
                {
                    RenewalID = renewal.RenewalID,
                    ContractID = renewal.ContractID,
                    ProposedValue = renewal.ProposedValue,
                    ProposedEndDate = renewal.ProposedEndDate,
                    Status = renewal.Status
                };
            }
        }

        public async Task<bool> DeleteRenewal(int id)
        {
            var renewal = await _context.Renewals.FindAsync(id);

            if (renewal == null)
                return false;

            _context.Renewals.Remove(renewal);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
