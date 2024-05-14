using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppication.DTOs;
using WebAppication.Models;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetEmployees()
        {
            return await _context.Employees
                .Select(e => new EmployeeDTO { Name = e.Name }) // Map Employee to EmployeeDTO
                .ToListAsync();
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDTO>> GetEmployee(Guid id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            var employeeDto = new EmployeeDTO { Name = employee.Name }; // Map Employee to EmployeeDTO

            return employeeDto;
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(Guid id, EmployeeDTO employeeDto)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            employee.Name = employeeDto.Name; // Map EmployeeDTO to Employee

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EmployeeDTO>> PostEmployee(EmployeeDTO employeeDto)
        {
            var employee = new Employee { Name = employeeDto.Name }; // Map EmployeeDTO to Employee

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployee", new { id = employee.Id }, new EmployeeDTO { Name = employee.Name }); // Return EmployeeDTO
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeExists(Guid id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }

        // GET: api/Employees/Department
        [HttpGet("employees-departments")]
        public async Task<ActionResult<IEnumerable<EmployeeDepartmentDTO>>> GetEmployeesWithDepartment()
        {
            return await _context.Employees
                .Include(e => e.Department)
                .Select(e => new EmployeeDepartmentDTO
                {
                    EmployeeName = e.Name,
                    DepartmentName = e.Department.Name
                })
                .ToListAsync();
        }

        // GET: api/Employees/Projects
        [HttpGet("employees-projects")]
        public async Task<ActionResult<IEnumerable<EmployeeProjectDTO>>> GetEmployeesWithProjects()
        {
            return await _context.Employees
                .Include(e => e.ProjectEmployees)
                    .ThenInclude(pe => pe.Project)
                .Select(e => new EmployeeProjectDTO
                {
                    EmployeeName = e.Name,
                    Projects = e.ProjectEmployees.Select(pe => new ProjectDTO { Name = pe.Project.Name }).ToList()
                })
                .ToListAsync();
        }

        // GET: api/Employees/SalaryAndDate
        [HttpGet("employees-filtered")]
        public async Task<ActionResult<IEnumerable<EmployeeSalaryDTO>>> GetEmployeesWithSalaryAndDate()
        {
            var date = new DateTime(2024, 1, 1);
            return await _context.Employees
                .Include(e => e.Salary)
                .Where(e => e.Salary.SalaryAmount > 100 && e.JoinedDate >= date)
                .Select(e => new EmployeeSalaryDTO
                {
                    EmployeeName = e.Name,
                    JoinedDate = e.JoinedDate,
                    SalaryAmount = e.Salary.SalaryAmount,
                })
                .ToListAsync();
        }

    }
}
