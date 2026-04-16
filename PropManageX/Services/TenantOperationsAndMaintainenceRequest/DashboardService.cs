using PropManageX.Models.Context;
using Microsoft.EntityFrameworkCore;
namespace PropManageX.Services.TenantOperationsAndMaintainenceRequest
{
    public class DashboardService
    {
        private readonly PropManageXContext _context;

        public DashboardService(PropManageXContext context)

        {

            _context = context;

        }

        public async Task<object> GetDashboardData()

        {

            var properties = await _context.Properties.ToListAsync();

            var units = await _context.Units.ToListAsync();

            var contracts = await _context.Contracts.ToListAsync();


            int totalProperties = properties.Count;

            int totalUnits = units.Count;


            int occupiedUnits = units.Count(u => u.Status == "Leased" || u.Status == "Sold");


             int vacantUnits = units.Count(u => u.Status == "Available");


            decimal totalRevenue = contracts.Sum(c => c.ContractValue);


            return new
            {

                totalProperties,

                totalUnits,

                occupiedUnits,

                vacantUnits,

                totalRevenue
            };

        }
    }
}
