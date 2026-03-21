using Microsoft.EntityFrameworkCore;
using PruebaEmi.Domain.Entities;
using PruebaEmi.Domain.Interfaces;
using PruebaEmi.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaEmi.Infrastructure.Repositories
{
    public class EmployeeRepository : Repository<employee>, IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;
        
        public EmployeeRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Método para obtener un empleado por su ID, incluyendo sus departamentos, historial de posiciones y proyectos asignados. Se utiliza AsNoTracking para mejorar el rendimiento al no rastrear los cambios en la entidad.
        /// </summary>
        /// <returns></returns>
        public override async Task<employee?> GetByIdAsync(int id)
        {
            var tracked = _context.ChangeTracker.Entries<employee>()
                .FirstOrDefault(e => e.Entity.Id == id);
            if (tracked != null)
            {
                tracked.State = EntityState.Detached;
            }

            return await _context.Employees
                .AsNoTracking()
                .Where(e => e.Id == id)
                .Include(e => e.Departments)
                .Include(e => e.PositionHistories.Where(ph => ph.EmployeeId == id))
                .Include(e => e.EmployeeProjects)
                    .ThenInclude(ep => ep.Project)
                .AsSplitQuery() 
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Método para obtener todos los empleados con sus departamentos, historial de posiciones y proyectos asignados. Se utiliza AsSplitQuery para optimizar la carga de datos relacionados.
        /// </summary>
        /// <returns></returns>
        public override async Task<IEnumerable<employee>> GetAllAsync()
        {
            return await _context.Employees
                .Include(e => e.Departments)
                .Include(e => e.PositionHistories)
                .Include(e => e.EmployeeProjects)
                    .ThenInclude(ep => ep.Project)
                .ToListAsync();
        }

        /// <summary>
        /// Metodo para obtener empleados por departamento, incluyendo solo aquellos que tienen proyectos asignados. Se utiliza AsSplitQuery para optimizar la carga de datos relacionados.
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public async Task<List<employee>> GetEmployeesByDepartmentWithProjectsAsync(int departmentId)
        {
            return await _context.Employees
                .Where(e => e.DepartmentId == departmentId && e.EmployeeProjects.Any())
                .Include(e => e.Departments)
                .Include(e => e.PositionHistories)
                .Include(e => e.EmployeeProjects)
                    .ThenInclude(ep => ep.Project)
                .ToListAsync();
        }
    }
}
