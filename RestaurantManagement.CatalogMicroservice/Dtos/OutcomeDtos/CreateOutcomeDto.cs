namespace RestaurantManagement.CatalogMicroservice.Dtos.OutcomeDtos
{
    public class CreateOutcomeDto
    {
      
        public string OutcomeName { get; set; }
        public decimal OutcomeAmount { get; set; }
        public string? OutcomeDescription { get; set; }
        public DateTime Date { get; set; }
        public string ShiftType { get; set; }
    }
}
