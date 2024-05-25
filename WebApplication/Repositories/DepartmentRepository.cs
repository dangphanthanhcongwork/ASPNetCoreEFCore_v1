using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using WebApplication.Models;

namespace WebApplication.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _context;

        public DepartmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Department>> GetDepartments()
        {
            return await _context.Departments.ToListAsync();
        }

        public async Task<Department> GetDepartment(Guid id)
        {
            var department = await _context.Departments.FindAsync(id) ?? throw new Exception("Not found!!!");
            return department;
        }

        public async Task PutDepartment(Guid id, Department department)
        {
            _context.Departments.Entry(department).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await DepartmentExists(id))
                {
                    throw new Exception("Not found!!!");
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task PostDepartment(Department department)
        {
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDepartment(Guid id)
        {
            var department = await _context.Departments.FindAsync(id) ?? throw new Exception("Not found!!!");
            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DepartmentExists(Guid id)
        {
            return await _context.Departments.AnyAsync(e => e.Id == id);
        }
    }
}