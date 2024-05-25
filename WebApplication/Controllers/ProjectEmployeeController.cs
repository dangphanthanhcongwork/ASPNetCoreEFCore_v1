using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;
using WebApplication.DTOs;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    [Route("api/project-employees")]
    [ApiController]
    public class ProjectEmployeesController : ControllerBase
    {
        private readonly IProjectEmployeeService _service;

        public ProjectEmployeesController(IProjectEmployeeService service)
        {
            _service = service;
        }

        // GET: api/project-employees
        [HttpGet]
        public async Task<IActionResult> GetProjectEmployees()
        {
            var projectEmployees = await _service.GetProjectEmployees();
            return Ok(projectEmployees);
        }

        // GET: api/project-employees/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectEmployee(Guid id)
        {
            try
            {
                var projectEmployee = await _service.GetProjectEmployee(id);
                return Ok(projectEmployee);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // PUT: api/project-employees/{id}
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProjectEmployee(Guid id, ProjectEmployeeDTO projectEmployeeDTO)
        {
            try
            {
                await _service.PutProjectEmployee(id, projectEmployeeDTO);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST: api/project-employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostProjectEmployee(ProjectEmployeeDTO projectEmployeeDTO)
        {
            await _service.PostProjectEmployee(projectEmployeeDTO);
            return CreatedAtAction(nameof(GetProjectEmployee), new { id = Guid.NewGuid() }, projectEmployeeDTO);
        }

        // DELETE: api/project-employees/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjectEmployee(Guid id)
        {
            try
            {
                await _service.DeleteProjectEmployee(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}