using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using WebApplication.Models;

namespace WebApplication.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDbContext _context;

        public ProjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Project>> GetProjects()
        {
            return await _context.Projects.ToListAsync();
        }

        public async Task<Project> GetProject(Guid id)
        {
            var project = await _context.Projects.FindAsync(id) ?? throw new Exception("Not found!!!");
            return project;
        }

        public async Task PutProject(Guid id, Project project)
        {
            _context.Projects.Entry(project).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ProjectExists(id))
                {
                    throw new Exception("Not found!!!");
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task PostProject(Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProject(Guid id)
        {
            var project = await _context.Projects.FindAsync(id) ?? throw new Exception("Not found!!!");
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ProjectExists(Guid id)
        {
            return await _context.Projects.AnyAsync(e => e.Id == id);
        }
    }
}