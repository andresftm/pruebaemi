using System;

namespace PruebaEmi.Domain.Entities
{
    /// <summary>
    /// Clase que representa la relación entre empleados y proyectos, indicando qué empleados están asignados a qué proyectos.
    /// </summary>
    public class EmployeeProject
    {
        public int Id { get; set; }  
        public int EmployeeId { get; set; }
        public int ProjectId { get; set; }


        public employee Employee { get; set; }
        public projects Project { get; set; }

    }
}