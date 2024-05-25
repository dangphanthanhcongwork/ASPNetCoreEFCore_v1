using WebApplication.DTOs;
using WebApplication.Models;

namespace WebApplication.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetEmployees();
        Task<Employee> GetEmployee(Guid id);
        Task PutEmployee(Guid id, EmployeeDTO employeeDTO);
        Task PostEmployee(EmployeeDTO employeeDTO);
        Task DeleteEmployee(Guid id);
        Task<IEnumerable<EmployeeDepartmentDTO>> GetEmployeesWithDepartments();
        Task<IEnumerable<EmployeeProjectDTO>> GetEmployeesWithProjects();
        Task<IEnumerable<EmployeeSalaryDTO>> FilterEmployeesBySalaryAndJoinedDate();
    }
}