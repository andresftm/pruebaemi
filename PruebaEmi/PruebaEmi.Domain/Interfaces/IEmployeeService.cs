using PruebaEmi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaEmi.Domain.Interfaces
{
    public interface IEmployeeService : IService<employee>
    {
        Task<List<employee>> GetEmployeesByDepartmentWithProjectsAsync(int departmentId);
    }

}
