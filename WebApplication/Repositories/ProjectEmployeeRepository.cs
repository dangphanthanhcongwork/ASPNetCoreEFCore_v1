using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using WebApplication.Models;

namespace WebApplication.Repositories
{
    public class ProjectEmployeeRepository : IProjectEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public ProjectEmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProjectEmployee>> GetProjectEmployees()
        {
            return await _context.ProjectEmployees.ToListAsync();
        }

        public async Task<ProjectEmployee> GetProjectEmployee(Guid id)
        {
            var projectEmployee = await _context.ProjectEmployees.FindAsync(id) ?? throw new Exception("Not found!!!");
            return projectEmployee;
        }

        public async Task PutProjectEmployee(Guid id, ProjectEmployee projectEmployee)
        {
            _context.ProjectEmployees.Entry(projectEmployee).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ProjectEmployeeExists(id))
                {
                    throw new Exception("Not found!!!");
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task PostProjectEmployee(ProjectEmployee projectEmployee)
        {
            _context.ProjectEmployees.Add(projectEmployee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProjectEmployee(Guid id)
        {
            var projectEmployee = await _context.ProjectEmployees.FindAsync(id) ?? throw new Exception("Not found!!!");
            _context.ProjectEmployees.Remove(projectEmployee);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ProjectEmployeeExists(Guid id)
        {
            return await _context.ProjectEmployees.AnyAsync(e => e.Id == id);
        }
    }
}