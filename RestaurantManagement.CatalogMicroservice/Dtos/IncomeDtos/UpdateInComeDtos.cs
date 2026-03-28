namespace RestaurantManagement.CatalogMicroservice.Dtos.IncomeDtos
{
    public class UpdateInComeDtos
    {
        public string IncomeId { get; set; }
        public string IncomeName { get; set; }
        public string? SelectedCompany { get; set; } // seçilen şirket adı
        public decimal IncomeAmount { get; set; } = 0;
        public decimal IncomeDescription { get; set; } = 0;
        public DateTime Date { get; set; }
        public string ShiftType { get; set; }
    }
}
