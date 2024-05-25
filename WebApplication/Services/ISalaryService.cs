using WebApplication.DTOs;
using WebApplication.Models;

namespace WebApplication.Services
{
    public interface ISalaryService
    {
        Task<IEnumerable<Salary>> GetSalaries();
        Task<Salary> GetSalary(Guid id);
        Task PutSalary(Guid id, SalaryDTO salary);
        Task PostSalary(SalaryDTO salary);
        Task DeleteSalary(Guid id);
    }
}