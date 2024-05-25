using WebApplication.Models;

namespace WebApplication.Repositories
{
    public interface ISalaryRepository
    {
        Task<IEnumerable<Salary>> GetSalaries();
        Task<Salary> GetSalary(Guid id);
        Task PutSalary(Guid id, Salary salary);
        Task PostSalary(Salary salary);
        Task DeleteSalary(Guid id);
        Task<bool> SalaryExists(Guid id);
    }
}