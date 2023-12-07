using KraviaTest.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KraviaTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController (IDashboardService _iDashboardService): ControllerBase
    {
        private readonly IDashboardService iDashboardService = _iDashboardService;

        // GET api/<DashboardController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return iDashboardService.GetDashboardData(id);
        }
    }
}
