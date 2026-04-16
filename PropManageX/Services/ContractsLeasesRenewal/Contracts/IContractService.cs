using PropManageX.DTOs.DTOsContractsLeasesRenewal.ContractDTOs;

namespace PropManageX.Services.ContractsLeasesRenewal.Contracts
{
    public interface IContractService
    {
        Task<List<ContractDto>> GetAllContracts();

        Task<ContractDto> GetContractById(int id);

        Task<ContractDto> CreateContract(CreateContractDto dto);

        Task<ContractDto> UpdateContract(int id, UpdateContractDto dto);

        Task<bool> DeleteContract(int id);

        Task<List<ContractDto>> GetMyContracts(string email);

        Task<TenantOverviewDto> GetMyOverview(string email);

        Task<bool> SignContract(int contractId);

    }
}
