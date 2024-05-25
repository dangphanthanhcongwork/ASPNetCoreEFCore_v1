using AutoMapper;
using WebApplication.DTOs;
using WebApplication.Models;
using WebApplication.Repositories;

namespace WebApplication.Services
{
    public class ProjectEmployeeService : IProjectEmployeeService
    {
        private readonly IProjectEmployeeRepository _repository;
        private readonly IMapper _mapper;

        public ProjectEmployeeService(IProjectEmployeeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProjectEmployee>> GetProjectEmployees()
        {
            return await _repository.GetProjectEmployees();
        }

        public async Task<ProjectEmployee> GetProjectEmployee(Guid id)
        {
            try
            {
                return await _repository.GetProjectEmployee(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task PutProjectEmployee(Guid id, ProjectEmployeeDTO projectEmployeeDTO)
        {
            try
            {
                var projectEmployee = _mapper.Map<ProjectEmployee>(projectEmployeeDTO);
                await _repository.PutProjectEmployee(id, projectEmployee);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task PostProjectEmployee(ProjectEmployeeDTO projectEmployeeDTO)
        {
            var projectEmployee = _mapper.Map<ProjectEmployee>(projectEmployeeDTO);
            await _repository.PostProjectEmployee(projectEmployee);
        }

        public async Task DeleteProjectEmployee(Guid id)
        {
            try
            {
                await _repository.DeleteProjectEmployee(id);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}