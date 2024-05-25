using AutoMapper;
using WebApplication.DTOs;
using WebApplication.Models;
using WebApplication.Repositories;

namespace WebApplication.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _repository;
        private readonly IMapper _mapper;

        public ProjectService(IProjectRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Project>> GetProjects()
        {
            return await _repository.GetProjects();
        }

        public async Task<Project> GetProject(Guid id)
        {
            try
            {
                return await _repository.GetProject(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task PutProject(Guid id, ProjectDTO projectDTO)
        {
            try
            {
                var project = _mapper.Map<Project>(projectDTO);
                await _repository.PutProject(id, project);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task PostProject(ProjectDTO projectDTO)
        {
            var project = _mapper.Map<Project>(projectDTO);
            await _repository.PostProject(project);
        }

        public async Task DeleteProject(Guid id)
        {
            try
            {
                await _repository.DeleteProject(id);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}