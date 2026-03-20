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

        public async Task<List<employee>> GetEmployeesByDepartmentWithProjectsAsync(int departmentId)
        {
            return await _employeeRepository.GetEmployeesByDepartmentWithProjectsAsync(departmentId);
        }

        public override async Task<employee> AddAsync(employee entity)
        {
            // Validaciones de negocio
            if (string.IsNullOrWhiteSpace(entity.Name))
                throw new ArgumentException("El nombre del empleado es requerido");

            if (entity.Salary < 0)
                throw new ArgumentException("El salario no puede ser negativo");

            return await _repository.AddAsync(entity);
        }

        // Lo Sobrescribo por si hay validaciones por hacer antes de editar un empleado
        public override async Task UpdateAsync(employee entity)
        {
            if (entity.Salary < 0)
                throw new ArgumentException("El salario no puede ser negativo");

            await _repository.UpdateAsync(entity);
        }

        // Lo Sobrescribo por si hay validaciones por hacer antes de eliminar un empleado
        public override async Task DeleteAsync(employee entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            await _repository.DeleteAsync(entity);
        }
    }
}
