using AutoMapper;
using WebApplication.DTOs;
using WebApplication.Models;
using WebApplication.Repositories;

namespace WebApplication.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _repository;
        private readonly IMapper _mapper;

        public DepartmentService(IDepartmentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Department>> GetDepartments()
        {
            return await _repository.GetDepartments();
        }

        public async Task<Department> GetDepartment(Guid id)
        {
            try
            {
                return await _repository.GetDepartment(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task PutDepartment(Guid id, DepartmentDTO departmentDTO)
        {
            try
            {
                var department = _mapper.Map<Department>(departmentDTO);
                await _repository.PutDepartment(id, department);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task PostDepartment(DepartmentDTO departmentDTO)
        {
            var department = _mapper.Map<Department>(departmentDTO);
            await _repository.PostDepartment(department);
        }

        public async Task DeleteDepartment(Guid id)
        {
            try
            {
                await _repository.DeleteDepartment(id);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}