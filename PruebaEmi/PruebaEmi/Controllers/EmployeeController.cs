using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PruebaEmi.Domain.Entities;
using PruebaEmi.Domain.Interfaces;

namespace PruebaEmi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // ⚠️ Requiere autenticación para todos los endpoints
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        // GET: api/Employee
        // Ambos roles pueden acceder (Admin y User)
        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<IEnumerable<employee>>> GetAllEmployees()
        {
            try
            {
                var employees = await _employeeService.GetAllAsync();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // GET: api/Employee/5
        // Ambos roles pueden acceder (Admin y User)
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<employee>> GetEmployeeById(int id)
        {
            try
            {
                var employee = await _employeeService.GetByIdAsync(id);
                
                if (employee == null)
                {
                    return NotFound($"Empleado con ID {id} no encontrado");
                }

                return Ok(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // GET: api/Employee/department/{departmentId}/with-projects
        // Ambos roles pueden acceder (Admin y User)
        [HttpGet("department/{departmentId}/with-projects")]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<List<employee>>> GetEmployeesByDepartmentWithProjects(int departmentId)
        {
            try
            {
                var employees = await _employeeService.GetEmployeesByDepartmentWithProjectsAsync(departmentId);
                
                if (!employees.Any())
                {
                    return NotFound($"No se encontraron empleados con proyectos en el departamento {departmentId}");
                }

                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // POST: api/Employee
        // Solo Admin puede crear
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<employee>> CreateEmployee([FromBody] employee employee)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var createdEmployee = await _employeeService.AddAsync(employee);
                return CreatedAtAction(nameof(GetEmployeeById), new { id = createdEmployee.Id }, createdEmployee);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // PUT: api/Employee/5
        // Solo Admin puede actualizar
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] employee employee)
        {
            try
            {
                if (id != employee.Id)
                {
                    return BadRequest("El ID del empleado no coincide");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var existingEmployee = await _employeeService.GetByIdAsync(id);
                if (existingEmployee == null)
                {
                    return NotFound($"Empleado con ID {id} no encontrado");
                }

                await _employeeService.UpdateAsync(employee);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // DELETE: api/Employee/5
        // Solo Admin puede eliminar
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                var employee = await _employeeService.GetByIdAsync(id);
                
                if (employee == null)
                {
                    return NotFound($"Empleado con ID {id} no encontrado");
                }

                await _employeeService.DeleteAsync(employee);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // GET: api/Employee/5/bonus
        // Ambos roles pueden acceder (Admin)
        [HttpGet("{id}/bonus")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<object>> GetEmployeeBonus(int id)
        {
            try
            {
                var bonus = await _employeeService.CalculateYearlyBonusAsync(id);
                
                return Ok(new 
                { 
                    employeeId = id,
                    yearlyBonus = bonus,
                    formattedBonus = $"${bonus:N2}"
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}