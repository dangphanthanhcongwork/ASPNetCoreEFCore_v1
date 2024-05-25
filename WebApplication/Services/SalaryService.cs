using AutoMapper;
using WebApplication.DTOs;
using WebApplication.Models;
using WebApplication.Repositories;

namespace WebApplication.Services
{
    public class SalaryService : ISalaryService
    {
        private readonly ISalaryRepository _repository;
        private readonly IMapper _mapper;

        public SalaryService(ISalaryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Salary>> GetSalaries()
        {
            return await _repository.GetSalaries();
        }

        public async Task<Salary> GetSalary(Guid id)
        {
            try
            {
                return await _repository.GetSalary(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task PutSalary(Guid id, SalaryDTO salaryDTO)
        {
            try
            {
                var salary = _mapper.Map<Salary>(salaryDTO);
                await _repository.PutSalary(id, salary);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task PostSalary(SalaryDTO salaryDTO)
        {
            var salary = _mapper.Map<Salary>(salaryDTO);
            await _repository.PostSalary(salary);
        }

        public async Task DeleteSalary(Guid id)
        {
            try
            {
                await _repository.DeleteSalary(id);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}