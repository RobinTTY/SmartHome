using MongoDB.Bson;
using SmartHome.Database.Models;

namespace SmartHome.Database;

public interface IDatabaseService
{
    /// <summary>
    /// Gets <see cref="WeightData"/> without any applied filter.
    /// </summary>
    /// <param name="limit">Limits the returned documents. Defaults to no limit.</param>
    /// <returns>A list of <see cref="WeightData"/> elements.</returns>
    Task<List<WeightData>> GetWeightData(int limit = 0);

    /// <summary>
    /// Adds new <see cref="WeightData"/> to the database.
    /// </summary>
    /// <param name="weightData">The <see cref="WeightData"/> to add.</param>
    /// <returns>The added <see cref="WeightData"/> object, updated with the database <see cref="ObjectId"/>.</returns>
    Task<WeightData> AddWeightData(WeightData weightData);
}