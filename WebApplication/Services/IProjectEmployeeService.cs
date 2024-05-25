using WebApplication.DTOs;
using WebApplication.Models;

namespace WebApplication.Services
{
    public interface IProjectEmployeeService
    {
        Task<IEnumerable<ProjectEmployee>> GetProjectEmployees();
        Task<ProjectEmployee> GetProjectEmployee(Guid id);
        Task PutProjectEmployee(Guid id, ProjectEmployeeDTO projectEmployee);
        Task PostProjectEmployee(ProjectEmployeeDTO projectEmployee);
        Task DeleteProjectEmployee(Guid id);
    }
}