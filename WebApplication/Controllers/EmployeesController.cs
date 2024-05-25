using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;
using WebApplication.DTOs;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeesController(IEmployeeService service)
        {
            _service = service;
        }

        // GET: api/employees
        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var employees = await _service.GetEmployees();
            return Ok(employees);
        }

        // GET: api/employees/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(Guid id)
        {
            try
            {
                var employee = await _service.GetEmployee(id);
                return Ok(employee);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // PUT: api/employees/{id}
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(Guid id, EmployeeDTO employeeDTO)
        {
            try
            {
                await _service.PutEmployee(id, employeeDTO);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST: api/employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostEmployee(EmployeeDTO employeeDTO)
        {
            await _service.PostEmployee(employeeDTO);
            return CreatedAtAction(nameof(GetEmployee), new { id = Guid.NewGuid() }, employeeDTO);
        }

        // DELETE: api/employees/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            try
            {
                await _service.DeleteEmployee(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // GET: api/employees-departments
        [HttpGet("employees-departments")]
        public async Task<ActionResult<IEnumerable<EmployeeDepartmentDTO>>> GetEmployeesWithDepartments()
        {
            var employees = await _service.GetEmployeesWithDepartments();
            return Ok(employees);
        }

        // GET: api/employees-projects
        [HttpGet("employees-projects")]
        public async Task<ActionResult<IEnumerable<EmployeeProjectDTO>>> GetEmployeesWithProjects()
        {
            var employees = await _service.GetEmployeesWithProjects();
            return Ok(employees);
        }

        // GET: api/filter-employees-by-salary-and-joined-date
        [HttpGet("filter-employees-by-salary-and-joined-date")]
        public async Task<ActionResult<IEnumerable<EmployeeSalaryDTO>>> FilterEmployeesBySalaryAndJoinedDate()
        {
            var employees = await _service.FilterEmployeesBySalaryAndJoinedDate();
            return Ok(employees);
        }
    }
}