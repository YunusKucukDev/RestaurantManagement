using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RestaurantManagement.CatalogMicroservice.Entities.Outcomes
{
    public class Outcome
    {
        [BsonId] // MongoDB _id alanı
        [BsonRepresentation(BsonType.ObjectId)]
        public string OutcomeId { get; set; }
        public string OutcomeName { get; set; }
        public decimal OutcomeAmount { get; set; }
        public string? OutcomeDescription { get; set; }
        public DateTime Date { get; set; }
        public string ShiftType { get; set; }
    }
}
