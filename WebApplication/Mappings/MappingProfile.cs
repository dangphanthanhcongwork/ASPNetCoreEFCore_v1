using AutoMapper;
using WebAppication.DTOs;
using WebAppication.Models;

namespace WebApplication.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Department, DepartmentDTO>();
            CreateMap<Employee, EmployeeDTO>();
            CreateMap<Project, ProjectDTO>();
            CreateMap<Salary, SalaryDTO>();
        }
    }
}