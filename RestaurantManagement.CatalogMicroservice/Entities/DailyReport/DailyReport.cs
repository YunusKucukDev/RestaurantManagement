using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RestaurantManagement.CatalogMicroservice.Entities.DailyReport
{
    public class DailyReport
    {
        [BsonId] // MongoDB _id alanı
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } // MongoDB kullanıyorsan string
        public DateTime Date { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalOutcome { get; set; }
        public decimal NetProfit { get; set; }
        public string ShiftType { get; set; } // "Gunduz" veya "Gece"
    }
}
