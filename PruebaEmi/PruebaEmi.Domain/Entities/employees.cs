using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaEmi.Domain.Entities
{
    public class employee
    {
        // Constantes para posiciones
        public const int POSITION_REGULAR = 1;
        public const int POSITION_MANAGER = 2;

        public int Id { get; set; }
        public string Name { get; set; }
        public int? CurrentPosition { get; set; }
        public decimal Salary { get; set; }
        public int DepartmentId { get; set; }

        public departments? Departments { get; set; }
        public List<PositionHistory>? PositionHistories { get; set; }
        public List<EmployeeProject>? EmployeeProjects { get; set; }

        /// <summary>
        /// Calcula el bono anual basado en el salario y la posición actual del historial.
        /// - Empleados regulares (Position contiene "Regular"): 10% del salario
        /// - Managers (Position contiene "Manager"): 20% del salario
        /// </summary>
        public decimal CalculateYearlyBonus()
        {
            // Buscar la posición actual (donde EndDate es null)
            var currentPosition = PositionHistories?
                .FirstOrDefault(ph => ph.EndDate == null);

            if (currentPosition == null || string.IsNullOrWhiteSpace(currentPosition.Position))
            {
                // Si no hay posición activa, retornar 10% por defecto
                return Salary * 0.10m;
            }

            var position = currentPosition.Position.ToLower();

            // Verificar si la posición CONTIENE "manager" o "regular"
            if (position.Contains("manager"))
            {
                return Salary * 0.20m;  // 20% para managers
            }
            else
            {
                return Salary * 0.10m;  // 10% por defecto para otras posiciones
            }
        }
    }
}
