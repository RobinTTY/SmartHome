using MongoDB.Bson;
using MongoDB.Driver;
using SmartHome.Database.Models;
using SmartHome.Database.Utility;

namespace SmartHome.Database
{
    /// <summary>
    /// Provides access to the database handling all SmartHome data.
    /// </summary>
    public class DatabaseService : IDatabaseService
    {
        private readonly IMongoCollection<WeightData> _weightDataCollection;

        /// <summary>
        /// Creates a new instance of <see cref="DatabaseService"/>.
        /// </summary>
        /// <param name="configurationManager">The <see cref="IConfigurationManager"/> to use for setup.</param>
        public DatabaseService(IConfigurationManager configurationManager)
        {
            var databaseSecrets = configurationManager.AppSecrets.DatabaseSecrets;
            var mongoClient = new MongoClient(databaseSecrets?.ConnectionString);
            var db = mongoClient.GetDatabase(databaseSecrets?.DatabaseName);

            _weightDataCollection = db.GetCollection<WeightData>("WeightData");
        }

        /// <summary>
        /// Gets <see cref="WeightData"/> without any applied filter.
        /// </summary>
        /// <param name="limit">Limits the returned documents. Defaults to no limit.</param>
        /// <returns>A list of <see cref="WeightData"/> elements.</returns>
        public async Task<List<WeightData>> GetWeightData(int limit = 0)
        {
            return await _weightDataCollection.Find(_ => true).Limit(limit).ToListAsync();
        }

        /// <summary>
        /// Adds new <see cref="WeightData"/> to the database.
        /// </summary>
        /// <param name="weightData">The <see cref="WeightData"/> to add.</param>
        /// <returns>The added <see cref="WeightData"/> object, updated with the database <see cref="ObjectId"/>.</returns>
        public async Task<WeightData> AddWeightData(WeightData weightData)
        {
            await _weightDataCollection.InsertOneAsync(weightData);
            return weightData;
        }
    }

    // TODO: this class is probably not necessary? ModelValidation maybe?
    public class WeightDataPostModel
    {
        public DateTime TimeStamp { get; set; }
        public double Weight { get; set; }
    }
}
