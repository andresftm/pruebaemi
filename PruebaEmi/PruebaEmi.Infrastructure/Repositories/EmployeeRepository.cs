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

        public async Task<List<employee>> GetEmployeesByDepartmentWithProjectsAsync(int departmentId)
        {
            return await _context.Employees
                .Where(e => e.DepartmentId == departmentId && e.EmployeeProjects.Any())
                .Include(e => e.Departments)
                .Include(e => e.EmployeeProjects)
                    .ThenInclude(ep => ep.Project)
                .ToListAsync();
        }
    }
}
