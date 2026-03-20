using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaEmi.Domain.Entities
{
    public class employee
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CurrentPosition { get; set; }

        public decimal Salary { get; set; }

        public int DepartmentId { get; set; }

        public departments Departments { get; set; }

        public List<PositionHistory> PositionHistories { get; set; }

        public List<EmployeeProject> EmployeeProjects { get; set; }
    }
}
