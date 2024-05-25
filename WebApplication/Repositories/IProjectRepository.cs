using WebApplication.Models;

namespace WebApplication.Repositories
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetProjects();
        Task<Project> GetProject(Guid id);
        Task PutProject(Guid id, Project project);
        Task PostProject(Project project);
        Task DeleteProject(Guid id);
        Task<bool> ProjectExists(Guid id);
    }
}