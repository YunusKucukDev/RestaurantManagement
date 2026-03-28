using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaaurantManagement.DtoLayer.Dtos.FixedExpenseDto
{
    public class ResultFixedExpense
    {
        public string Id { get; set; }
        public string Name { get; set; } // Kira, Maaş, Elektrik vb.
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string ShiftType { get; set; } // "Gunduz" veya "Gece"
    }
}
