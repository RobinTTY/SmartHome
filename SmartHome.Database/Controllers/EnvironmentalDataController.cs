using Microsoft.AspNetCore.Mvc;
using SmartHome.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace SmartHome.Database.Controllers
{
    [ApiController]
    [Route("/weightData")]
    [SwaggerTag("Weight tracking data for a person.")]
    public class EnvironmentalDataController : ControllerBase
    {
        private readonly ILogger<EnvironmentalDataController> _logger;
        private readonly IDatabaseService _databaseService;

        public EnvironmentalDataController(ILogger<EnvironmentalDataController> logger, IDatabaseService databaseService)
        {
            _logger = logger;
            _databaseService = databaseService;
        }

        [HttpGet]
        public async Task<ActionResult<List<EnvironmentalData>>> Get([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            return await _databaseService.GetEnvironmentalData(startDate, endDate);
        }
    }
}
