using WebApplication.DTOs;
using WebApplication.Models;

namespace WebApplication.Services
{
    public interface IProjectService
    {
        Task<IEnumerable<Project>> GetProjects();
        Task<Project> GetProject(Guid id);
        Task PutProject(Guid id, ProjectDTO projectDTO);
        Task PostProject(ProjectDTO projectDTO);
        Task DeleteProject(Guid id);
    }
}