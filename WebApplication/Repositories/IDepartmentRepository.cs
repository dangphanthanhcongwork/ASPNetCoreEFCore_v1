using WebApplication.Models;

namespace WebApplication.Repositories
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetDepartments();
        Task<Department> GetDepartment(Guid id);
        Task PutDepartment(Guid id, Department department);
        Task PostDepartment(Department department);
        Task DeleteDepartment(Guid id);
        Task<bool> DepartmentExists(Guid id);
    }
}