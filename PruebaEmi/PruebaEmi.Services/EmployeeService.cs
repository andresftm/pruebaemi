using PruebaEmi.Domain.Entities;
using PruebaEmi.Domain.Interfaces;

namespace PruebaEmi.Services
{
    public class EmployeeService : Service<employee>, IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
            : base(employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        /// <summary>
        /// Método para obtener una lista de empleados que pertenecen a un departamento específico, incluyendo la información de los proyectos en los que están involucrados. Este método utiliza una consulta optimizada para cargar los datos relacionados de manera eficiente y evitar problemas de rendimiento al acceder a la base de datos.
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public async Task<List<employee>> GetEmployeesByDepartmentWithProjectsAsync(int departmentId)
        {
            return await _employeeRepository.GetEmployeesByDepartmentWithProjectsAsync(departmentId);
        }

        /// <summary>
        /// Método para calcular el bono anual de un empleado basado en su salario y posición actual.
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<decimal> CalculateYearlyBonusAsync(int employeeId)
        {
            var employee = await _employeeRepository.GetByIdAsync(employeeId);

            if (employee == null)
                throw new KeyNotFoundException($"Empleado con ID {employeeId} no encontrado");

            if (employee.Salary <= 0)
                throw new InvalidOperationException("El empleado no tiene un salario válido para calcular el bono");

            return employee.CalculateYearlyBonus();
        }

        /// <summary>
        /// Método para agregar un nuevo empleado con validaciones básicas para asegurar que los datos sean correctos antes de guardarlos en la base de datos.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public override async Task<employee> AddAsync(employee entity)
        {
            if (string.IsNullOrWhiteSpace(entity.Name))
                throw new ArgumentException("El nombre del empleado es requerido");

            if (entity.Salary < 0)
                throw new ArgumentException("El salario no puede ser negativo");

            return await _repository.AddAsync(entity);
        }

        /// <summary>
        /// Método para actualizar la información de un empleado existente con validaciones para asegurar que los datos sean correctos antes de guardarlos en la base de datos.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public override async Task UpdateAsync(employee entity)
        {
            if (entity.Salary < 0)
                throw new ArgumentException("El salario no puede ser negativo");

            await _repository.UpdateAsync(entity);
        }
        /// <summary>
        /// Metodo para eliminar un empleado existente con validaciones para asegurar que el empleado exista antes de intentar eliminarlo de la base de datos.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public override async Task DeleteAsync(employee entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            await _repository.DeleteAsync(entity);
        }
    }
}
