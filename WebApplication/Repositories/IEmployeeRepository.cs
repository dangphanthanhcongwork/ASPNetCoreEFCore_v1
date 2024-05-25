using WebApplication.DTOs;
using WebApplication.Models;

namespace WebApplication.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetEmployees();
        Task<Employee> GetEmployee(Guid id);
        Task PutEmployee(Guid id, Employee employee);
        Task PostEmployee(Employee employee);
        Task DeleteEmployee(Guid id);
        Task<bool> EmployeeExists(Guid id);
        Task<IEnumerable<EmployeeDepartmentDTO>> GetEmployeesWithDepartments();
        Task<IEnumerable<EmployeeProjectDTO>> GetEmployeesWithProjects();
        Task<IEnumerable<EmployeeSalaryDTO>> FilterEmployeesBySalaryAndJoinedDate();
    }
}