using PropManageX.DTOs.DTOsContractsLeasesRenewal.Renewal;

namespace PropManageX.Services.ContractsLeasesRenewal.Renewal
{
    public interface IRenewalService
    {
        Task<List<RenewalDto>> GetAllRenewals();

        Task<RenewalDto> GetRenewalById(int id);

        Task<RenewalDto> CreateRenewal(CreateRenewalDto dto);

        Task<RenewalDto> UpdateRenewal(int id, UpdateRenewalDto dto);

        Task<bool> DeleteRenewal(int id);
    }
}
