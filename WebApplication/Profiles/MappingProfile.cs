using AutoMapper;
using WebApplication.Models;
using WebApplication.DTOs;

namespace WebApplication.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Department, DepartmentDTO>();
            CreateMap<DepartmentDTO, Department>();

            CreateMap<Project, ProjectDTO>();
            CreateMap<ProjectDTO, Project>();

            CreateMap<Employee, EmployeeDTO>();
            CreateMap<EmployeeDTO, Employee>();

            CreateMap<ProjectEmployee, ProjectEmployeeDTO>();
            CreateMap<ProjectEmployeeDTO, ProjectEmployee>();

            CreateMap<Salary, SalaryDTO>();
            CreateMap<SalaryDTO, Salary>();
        }
    }
}