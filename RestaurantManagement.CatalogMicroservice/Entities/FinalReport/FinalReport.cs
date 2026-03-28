using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using RestaurantManagement.CatalogMicroservice.Dtos.IncomeDtos;
using RestaurantManagement.CatalogMicroservice.Dtos.OutcomeDtos;

namespace RestaurantManagement.CatalogMicroservice.Entities.FinalReport
{
    public class FinalReport
    {
        [BsonId] // MongoDB _id alanı
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string ReportName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<CostItem> SelectedCosts { get; set; }

        public List<ResultIncomeDto> IncomeDetails { get; set; } // O dönemdeki tüm gelirler
        public List<ResultOutcomeDto> OutcomeDetails { get; set; } // O dönemdeki tüm giderler

        public decimal NetTotal { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal TotalExpenses { get; set; }
        public string ShiftType { get; set; }
    }

    public class CostItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
    }
}
