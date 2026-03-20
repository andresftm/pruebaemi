using System;

namespace PruebaEmi.Domain.Entities
{
    public class EmployeeProject
    {
        public int Id { get; set; }  
        public int EmployeeId { get; set; }
        public int ProjectId { get; set; }


        public employee Employee { get; set; }
        public projects Project { get; set; }

    }
}