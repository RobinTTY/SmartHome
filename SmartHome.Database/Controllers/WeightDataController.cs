using Microsoft.AspNetCore.Mvc;
using SmartHome.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace SmartHome.Database.Controllers
{
    [ApiController]
    [Route("/weightData")]
    [SwaggerTag("Weight tracking data for a person.")]
    public class WeightDataController : ControllerBase
    {
        private readonly ILogger<WeightDataController> _logger;
        private readonly IDatabaseService _databaseService;

        public WeightDataController(ILogger<WeightDataController> logger, IDatabaseService databaseService)
        {
            _logger = logger;
            _databaseService = databaseService;
        }

        /// <summary>
        /// Gets <see cref="WeightData"/> stored in the database.
        /// </summary>
        /// <param name="offset">Offset, starting with zero, which determines the current page.</param>
        /// <param name="limit">Number of items that are returned per page.</param>
        /// <returns>A list containing the <see cref="WeightData"/> elements stored in the database.</returns>
        [HttpGet]
        public async Task<ActionResult<List<WeightData>>> Get([FromQuery] int offset = 0, [FromQuery] int limit = 500)
        {
            var data = await _databaseService.GetWeightData(limit);
            return data.Skip(offset * limit).Take(limit).ToList();
        }

        /// <summary>
        /// Creates a new <see cref="WeightData"/> element in the database.
        /// </summary>
        /// <param name="weightData">The <see cref="WeightData"/> to create.</param>
        /// <returns>The <see cref="WeightData"/> element that was added to the database.</returns>
        [HttpPost]
        public async Task<ActionResult<WeightData>> Post([FromBody] WeightDataPostModel weightData)
        {
            var weightDataObject = new WeightData
            {
                TimeStamp = weightData.TimeStamp,
                Weight = weightData.Weight
            };
            var createdDocument = await _databaseService.AddWeightData(weightDataObject);
            return CreatedAtAction(nameof(Get), new { createdDocument.Id }, createdDocument);
        }
    }
}
