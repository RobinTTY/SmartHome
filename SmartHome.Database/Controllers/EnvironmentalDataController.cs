using Microsoft.AspNetCore.Mvc;
using SmartHome.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace SmartHome.Database.Controllers
{
    [ApiController]
    [Route("/environmentalData")]
    [SwaggerTag("Environmental data, like temperature, humidity, etc.")]
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
        public async Task<ActionResult<List<EnvironmentalData>>> Get([FromQuery] long startDateUnixMillis, [FromQuery] long endDateUnixMillis)
        {
            var startDate = DateTimeOffset.FromUnixTimeMilliseconds(startDateUnixMillis);
            var endDate = DateTimeOffset.FromUnixTimeMilliseconds(endDateUnixMillis);
            
            return await _databaseService.GetEnvironmentalData(startDate.DateTime, endDate.DateTime);
        }
    }
}
