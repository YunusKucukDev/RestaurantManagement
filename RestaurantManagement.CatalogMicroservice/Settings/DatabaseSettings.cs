namespace RestaurantManagement.CatalogMicroservice.Settings
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string InComeCollectionName { get; set; }
        public string OutcomeCollectionName { get; set; }
        public string FixedExpenseCollectionName { get; set; }
        public string DailyReportCollectionName { get; set; }
        public string FinalReportCollectionName { get; set; }

        public string UsersIdentityCollectionName { get; set; } // Identity kullanıcıları için (Örn: "IdentityUsers")
        public string RolesIdentityCollectionName { get; set; } // Identity rolleri için (Örn: "IdentityRoles")
    }
}
