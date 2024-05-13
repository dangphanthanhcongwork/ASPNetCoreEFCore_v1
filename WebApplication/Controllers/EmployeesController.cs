using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            return await _context.Employees
                .Include(e => e.Department)
                .Include(e => e.Salary)
                .Include(e => e.ProjectEmployees)
                    .ThenInclude(pe => pe.Project)
                .ToListAsync();
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(Guid id)
        {
            var employee = await _context.Employees
                .Include(e => e.Department)
                .Include(e => e.Salary)
                .Include(e => e.ProjectEmployees)
                    .ThenInclude(pe => pe.Project)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(Guid id, Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;
            _context.Entry(employee).Reference(e => e.Department).Load();
            _context.Entry(employee).Reference(e => e.Salary).Load();
            _context.Entry(employee).Collection(e => e.ProjectEmployees).Query().Include(pe => pe.Project).Load();

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
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            _context.Entry(employee).Reference(e => e.Department).Load();
            _context.Entry(employee).Reference(e => e.Salary).Load();
            _context.Entry(employee).Collection(e => e.ProjectEmployees).Query().Include(pe => pe.Project).Load();

            return CreatedAtAction("GetEmployee", new { id = employee.Id }, employee);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            var employee = await _context.Employees
                .Include(e => e.Department)
                .Include(e => e.Salary)
                .Include(e => e.ProjectEmployees)
                    .ThenInclude(pe => pe.Project)
                .FirstOrDefaultAsync(e => e.Id == id);
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

        [HttpGet("employees-departments")]
        public async Task<IEnumerable<object>> GetEmployeesWithDepartments()
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var employees = await _context.Employees
                    .Join(_context.Departments,
                        employee => employee.DepartmentId,
                        department => department.Id,
                        (employee, department) => new
                        {
                            EmployeeName = employee.Name,
                            DepartmentName = department.Name
                        })
                    .ToListAsync();

                transaction.Commit();
                return employees;
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        [HttpGet("employees-projects")]
        public async Task<IEnumerable<object>> GetEmployeesWithProjects()
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var employees = await _context.Employees
                    .GroupJoin(_context.ProjectEmployees,
                        employee => employee.Id,
                        projectEmployee => projectEmployee.EmployeeId,
                        (employee, projectEmployee) => new { employee, projectEmployee })
                    .SelectMany(x => x.projectEmployee.DefaultIfEmpty(),
                        (x, y) => new { x.employee, y })
                    .Join(_context.Projects,
                        result => result.y.ProjectId,
                        project => project.Id,
                        (result, project) => new
                        {
                            EmployeeName = result.employee.Name,
                            ProjectName = project.Name
                        })
                    .ToListAsync();

                transaction.Commit();
                return employees;
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        [HttpGet("employees-filtered")]
        public async Task<IEnumerable<object>> GetFilteredEmployees()
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var date = new DateTime(2024, 1, 1);
                var employees = await _context.Employees
                    .Join(_context.Salaries,
                        employee => employee.Id,
                        salary => salary.EmployeeId,
                        (employee, salary) => new { employee, salary })
                    .Where(x => x.salary.SalaryAmount > 100 && x.employee.JoinedDate >= date)
                    .Select(x => new
                    {
                        EmployeeName = x.employee.Name,
                        Salary = x.salary.SalaryAmount
                    })
                    .ToListAsync();

                transaction.Commit();
                return employees;
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}
