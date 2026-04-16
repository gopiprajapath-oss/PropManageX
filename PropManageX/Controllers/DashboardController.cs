using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PropManageX.Services.TenantOperationsAndMaintainenceRequest;

namespace PropManageX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly DashboardService _service;


        public DashboardController(DashboardService service)

        {

            _service = service;

        }


        [HttpGet]

        public async Task<IActionResult> GetDashboard()

        {

            var data = await _service.GetDashboardData();

            return Ok(data);

        }

    }
}

