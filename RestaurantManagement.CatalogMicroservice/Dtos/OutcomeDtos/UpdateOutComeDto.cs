namespace RestaurantManagement.CatalogMicroservice.Dtos.OutcomeDtos
{
    public class UpdateOutComeDto
    {
        public string OutcomeId { get; set; }
        public string OutcomeName { get; set; }
        public decimal OutcomeAmount { get; set; }
        public string? OutcomeDescription { get; set; }
        public DateTime Date { get; set; }
        public string ShiftType { get; set; }
    }
}
