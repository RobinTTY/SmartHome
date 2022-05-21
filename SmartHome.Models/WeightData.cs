using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SmartHome.Models
{
    public class WeightData
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public double Weight { get; set; }
    }
}
