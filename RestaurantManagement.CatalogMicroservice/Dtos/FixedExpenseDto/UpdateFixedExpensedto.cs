namespace RestaurantManagement.CatalogMicroservice.Dtos.FixedExpenseDto
{
    public class UpdateFixedExpensedto
    {
        public string Id { get; set; }
        public string Name { get; set; } // Kira, Maaş, Elektrik vb.
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string ShiftType { get; set; }
    }
}
