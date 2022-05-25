using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;



namespace SchedulingService.API.Models
{
    public class Schedule
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("CompanyId")]
        public Guid CompanyId { get; set; }
        [BsonElement("Notifications")]
        public List<DateTime> Notifications { get; set; }
    }
}
