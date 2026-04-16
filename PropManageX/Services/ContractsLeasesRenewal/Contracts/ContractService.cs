using PropManageX.DTOs.DTOsContractsLeasesRenewal.ContractDTOs;
using PropManageX.Models.Context;
using PropManageX.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace PropManageX.Services.ContractsLeasesRenewal.Contracts
{
    public class ContractService : IContractService
    {
        private readonly PropManageXContext _context;

        public ContractService(PropManageXContext context)
        {
            _context = context;
        }

        public async Task<List<ContractDto>> GetAllContracts()
        {
            return await _context.Contracts
                .Select(c => new ContractDto
                {
                    ContractID = c.ContractID,
                    DealID = c.DealID,
                    ContractType = c.ContractType,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    ContractValue = c.ContractValue,
                    Status = c.Status
                })
                .ToListAsync();
        }

        public async Task<ContractDto> GetContractById(int id)
        {
            var contract = await _context.Contracts.FindAsync(id);

            if (contract == null)
                return null;

            return new ContractDto
            {
                ContractID = contract.ContractID,
                DealID = contract.DealID,
                ContractType = contract.ContractType,
                StartDate = contract.StartDate,
                EndDate = contract.EndDate,
                ContractValue = contract.ContractValue,
                Status = contract.Status
            };
        }

        public async Task<ContractDto> CreateContract(CreateContractDto dto)
        {
            var exists = await _context.Contracts
                .AnyAsync(c => c.DealID == dto.DealID);

            if (exists)
                throw new Exception("Contract already exists for this deal");

            var contract = new ContractModel
            {
                DealID = dto.DealID,
                ContractType = dto.ContractType,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                ContractValue = dto.ContractValue,
                Status = "Active"
            };

            _context.Contracts.Add(contract);
            await _context.SaveChangesAsync();

            return new ContractDto
            {
                ContractID = contract.ContractID,
                DealID = contract.DealID,
                ContractType = contract.ContractType,
                StartDate = contract.StartDate,
                EndDate = contract.EndDate,
                ContractValue = contract.ContractValue,
                Status = contract.Status
            };
        }

        public async Task<ContractDto> UpdateContract(int id, UpdateContractDto dto)
        {
            var contract = await _context.Contracts.FindAsync(id);

            if (contract == null)
                return null;

            contract.ContractType = dto.ContractType;
            contract.StartDate = dto.StartDate;
            contract.EndDate = dto.EndDate;
            contract.ContractValue = dto.ContractValue;
            contract.Status = dto.Status;

            await _context.SaveChangesAsync();

            return new ContractDto
            {
                ContractID = contract.ContractID,
                DealID = contract.DealID,
                ContractType = contract.ContractType,
                StartDate = contract.StartDate,
                EndDate = contract.EndDate,
                ContractValue = contract.ContractValue,
                Status = contract.Status
            };
        }

        public async Task<bool> DeleteContract(int id)
        {
            var contract = await _context.Contracts.FindAsync(id);

            if (contract == null)
                return false;

            _context.Contracts.Remove(contract);
            await _context.SaveChangesAsync();

            return true;
        }

        // Location: Services/ContractsLeasesRenewal/Contracts/ContractService.cs

     



        public async Task<TenantOverviewDto> GetMyOverview(string email)
        {
            var contract = await _context.Contracts
                .Include(c => c.Deal)
                    .ThenInclude(d => d.LeadModel)
                .Include(c => c.Deal)
                    .ThenInclude(d => d.UnitModel)
                    .ThenInclude(u => u.PropertyModel)
                .Where(c => c.Deal.LeadModel.ContactInfo == email)
                .OrderByDescending(c => c.StartDate) // Gets the most recent contract
                .FirstOrDefaultAsync();

            if (contract == null) return null;

            return new TenantOverviewDto
            {
                ContractID = contract.ContractID,
                PropertyName = contract.Deal.UnitModel.PropertyModel.Name,
                Location = contract.Deal.UnitModel.PropertyModel.Location,
                UnitNumber = contract.Deal.UnitModel.UnitNumber,
                ContractType = contract.ContractType,
                ContractValue = contract.ContractValue,
                StartDate = contract.StartDate,
                EndDate = contract.EndDate,
                IsSigned = contract.Status == "Active" // Assuming
            };
        }




        // Location: Services/ContractsLeasesRenewal/Contracts/ContractService.cs

        // ==========================================
        // TENANT DASHBOARD: GET MY CONTRACTS
        // ==========================================
        public async Task<List<ContractDto>> GetMyContracts(string email)
        {
            return await _context.Contracts
                .Include(c => c.Deal)
                .ThenInclude(d => d.LeadModel)
                .Where(c => c.Deal.LeadModel.ContactInfo == email)
                .Select(c => new ContractDto
                {
                    ContractID = c.ContractID,
                    DealID = c.DealID,
                    ContractType = c.ContractType,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    ContractValue = c.ContractValue,
                    Status = c.Status
                })
                .ToListAsync();
        }

        // ==========================================
        // TENANT DASHBOARD: SIGN CONTRACT
        // ==========================================
        public async Task<bool> SignContract(int contractId)
        {
            // 1. Find the active contract
            var contract = await _context.Contracts.FindAsync(contractId);
            if (contract == null) return false;

            // 2. Find if there is an active renewal offer for this contract
            var pendingRenewal = await _context.Renewals
                .Where(r => r.ContractID == contractId && (r.Status == "Offered" || r.Status == "Pending"))
                .OrderByDescending(r => r.RenewalID)
                .FirstOrDefaultAsync();

            // 3. If a renewal exists, apply its terms
            if (pendingRenewal != null)
            {
                pendingRenewal.Status = "Accepted"; // Mark Renewal table as Accepted
                contract.EndDate = pendingRenewal.ProposedEndDate;
                contract.ContractValue = pendingRenewal.ProposedValue;
            }

            // 4. Mark the Contract table as Signed
            contract.Status = "Signed";

            // 5. Save all changes to the database simultaneously 
            await _context.SaveChangesAsync();

            return true;
        }
    }
}

