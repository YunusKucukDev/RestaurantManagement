using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RestaurantManagement.CatalogMicroservice.Entities.InComes
{
    public class Income
    {
        [BsonId] // MongoDB _id alanı
        [BsonRepresentation(BsonType.ObjectId)]
        public string IncomeId { get; set; }
        public string IncomeName { get; set; }
        public string? SelectedCompany { get; set; } // seçilen şirket adı
        public decimal IncomeAmount { get; set; } = 0;
        public decimal IncomeDescription { get; set; } = 0;
        public DateTime Date { get; set; }
        public string ShiftType { get; set; }

    }
}
