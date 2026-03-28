using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaaurantManagement.DtoLayer.Dtos.IncomeDto
{
    public class ResultIncomeDto
    {
        public string IncomeId { get; set; }
        public string IncomeName { get; set; }
        public string? SelectedCompany { get; set; } // seçilen şirket adı
        public decimal IncomeAmount { get; set; } = 0;
        public decimal IncomeDescription { get; set; } = 0;
        public DateTime Date { get; set; }
        public string ShiftType { get; set; } // "Gunduz" veya "Gece"
    }
}
