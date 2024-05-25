using AutoMapper;
using WebApplication.DTOs;
using WebApplication.Models;
using WebApplication.Repositories;

namespace WebApplication.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await _repository.GetEmployees();
        }

        public async Task<Employee> GetEmployee(Guid id)
        {
            try
            {
                return await _repository.GetEmployee(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task PutEmployee(Guid id, EmployeeDTO employeeDTO)
        {
            try
            {
                var employee = _mapper.Map<Employee>(employeeDTO);
                await _repository.PutEmployee(id, employee);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task PostEmployee(EmployeeDTO employeeDTO)
        {
            var employee = _mapper.Map<Employee>(employeeDTO);
            await _repository.PostEmployee(employee);
        }

        public async Task DeleteEmployee(Guid id)
        {
            try
            {
                await _repository.DeleteEmployee(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<EmployeeDepartmentDTO>> GetEmployeesWithDepartments()
        {
            return await _repository.GetEmployeesWithDepartments();
        }

        public async Task<IEnumerable<EmployeeProjectDTO>> GetEmployeesWithProjects()
        {
            return await _repository.GetEmployeesWithProjects();
        }

        public async Task<IEnumerable<EmployeeSalaryDTO>> FilterEmployeesBySalaryAndJoinedDate()
        {
            return await _repository.FilterEmployeesBySalaryAndJoinedDate();
        }
    }
}