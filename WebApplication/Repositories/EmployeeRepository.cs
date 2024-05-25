using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using WebApplication.DTOs;
using WebApplication.Models;

namespace WebApplication.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Employee> GetEmployee(Guid id)
        {
            var employee = await _context.Employees.FindAsync(id) ?? throw new Exception("Not found!!!");
            return employee;
        }

        public async Task PutEmployee(Guid id, Employee employee)
        {
            _context.Employees.Entry(employee).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await EmployeeExists(id))
                {
                    throw new Exception("Not found!!!");
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task PostEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEmployee(Guid id)
        {
            var employee = await _context.Employees.FindAsync(id) ?? throw new Exception("Not found!!!");
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> EmployeeExists(Guid id)
        {
            return await _context.Employees.AnyAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<EmployeeDepartmentDTO>> GetEmployeesWithDepartments()
        {
            return await _context.Employees
                .Join(_context.Departments,
                    employee => employee.DepartmentId,
                    department => department.Id,
                    (employee, department) => new EmployeeDepartmentDTO
                    {
                        EmployeeName = employee.Name,
                        DepartmentName = department.Name
                    })
                .ToListAsync();
        }

        public async Task<IEnumerable<EmployeeProjectDTO>> GetEmployeesWithProjects()
        {
            return await _context.Employees
                .GroupJoin(_context.ProjectEmployees,
                    employee => employee.Id,
                    projectEmployee => projectEmployee.EmployeeId,
                    (employee, projectEmployee) => new { employee, projectEmployee })
                .SelectMany(
                    x => x.projectEmployee.DefaultIfEmpty(),
                    (x, y) => new { Employee = x.employee, ProjectEmployee = y })
                .GroupJoin(_context.Projects,
                    x => x.ProjectEmployee.ProjectId,
                    project => project.Id,
                    (x, project) => new { x, project })
                .SelectMany(x => x.project.DefaultIfEmpty(),
                    (x, y) => new { x.x.Employee.Name, Project = y })
                .GroupBy(x => x.Name)
                .Select(z => new EmployeeProjectDTO
                {
                    EmployeeName = z.Key,
                    ProjectNames = z.Select(x => x.Project.Name).OrderBy(name => name).ToList()
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<EmployeeSalaryDTO>> FilterEmployeesBySalaryAndJoinedDate()
        {
            return await _context.Employees
                .Join(_context.Salaries,
                    employee => employee.Id,
                    salary => salary.EmployeeId,
                    (employee, salary) => new EmployeeSalaryDTO
                    {
                        EmployeeName = employee.Name,
                        JoinedDate = employee.JoinedDate,
                        SalaryAmount = salary.SalaryAmount
                    })
                .Where(employee => employee.SalaryAmount > 100 && employee.JoinedDate >= new DateTime(2024, 1, 1))
                .ToListAsync();
        }
    }
}