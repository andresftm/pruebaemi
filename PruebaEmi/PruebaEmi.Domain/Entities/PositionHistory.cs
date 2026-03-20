using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaEmi.Domain.Entities
{
    public class PositionHistory
    {
        public int id { get; set; }
        public int EmployeeId { get; set; }

        public string Position { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public employee Employee { get; set; }
    }
}
