using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;
using WebApplication.DTOs;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    [Route("api/departments")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentService _service;

        public DepartmentsController(IDepartmentService service)
        {
            _service = service;
        }

        // GET: api/departments
        [HttpGet]
        public async Task<IActionResult> GetDepartments()
        {
            var departments = await _service.GetDepartments();
            return Ok(departments);
        }

        // GET: api/departments/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartment(Guid id)
        {
            try
            {
                var department = await _service.GetDepartment(id);
                return Ok(department);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // PUT: api/departments/{id}
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartment(Guid id, DepartmentDTO departmentDTO)
        {
            try
            {
                await _service.PutDepartment(id, departmentDTO);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST: api/departments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostDepartment(DepartmentDTO departmentDTO)
        {
            await _service.PostDepartment(departmentDTO);
            return CreatedAtAction(nameof(GetDepartment), new { id = Guid.NewGuid() }, departmentDTO);
        }

        // DELETE: api/departments/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(Guid id)
        {
            try
            {
                await _service.DeleteDepartment(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}