using WebApplication.DTOs;
using WebApplication.Models;

namespace WebApplication.Services
{
    public interface IDepartmentService
    {
        Task<IEnumerable<Department>> GetDepartments();
        Task<Department> GetDepartment(Guid id);
        Task PutDepartment(Guid id, DepartmentDTO departmentDTO);
        Task PostDepartment(DepartmentDTO departmentDTO);
        Task DeleteDepartment(Guid id);
    }
}