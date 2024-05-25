using WebApplication.Models;

namespace WebApplication.Repositories
{
    public interface IProjectEmployeeRepository
    {
        Task<IEnumerable<ProjectEmployee>> GetProjectEmployees();
        Task<ProjectEmployee> GetProjectEmployee(Guid id);
        Task PutProjectEmployee(Guid id, ProjectEmployee department);
        Task PostProjectEmployee(ProjectEmployee department);
        Task DeleteProjectEmployee(Guid id);
        Task<bool> ProjectEmployeeExists(Guid id);
    }
}