using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using WebApplication.Models;

namespace WebApplication.Repositories
{
    public class SalaryRepository : ISalaryRepository
    {
        private readonly ApplicationDbContext _context;

        public SalaryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Salary>> GetSalaries()
        {
            return await _context.Salaries.ToListAsync();
        }

        public async Task<Salary> GetSalary(Guid id)
        {
            var salary = await _context.Salaries.FindAsync(id) ?? throw new Exception("Not found!!!");
            return salary;
        }

        public async Task PutSalary(Guid id, Salary salary)
        {
            _context.Salaries.Entry(salary).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await SalaryExists(id))
                {
                    throw new Exception("Not found!!!");
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task PostSalary(Salary salary)
        {
            _context.Salaries.Add(salary);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSalary(Guid id)
        {
            var salary = await _context.Salaries.FindAsync(id) ?? throw new Exception("Not found!!!");
            _context.Salaries.Remove(salary);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> SalaryExists(Guid id)
        {
            return await _context.Salaries.AnyAsync(e => e.Id == id);
        }
    }
}