using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaaurantManagement.DtoLayer.Dtos.OutcomeDto
{
    public class CreateOutcomeDto
    {
        public string OutcomeName { get; set; }
        public decimal OutcomeAmount { get; set; }
        public string? OutcomeDescription { get; set; }
        public DateTime Date { get; set; }
        public string ShiftType { get; set; } // "Gunduz" veya "Gece"
    }
}
