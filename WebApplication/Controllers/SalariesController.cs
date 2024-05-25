using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;
using WebApplication.DTOs;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    [Route("api/salaries")]
    [ApiController]
    public class SalariesController : ControllerBase
    {
        private readonly ISalaryService _service;

        public SalariesController(ISalaryService service)
        {
            _service = service;
        }

        // GET: api/salaries
        [HttpGet]
        public async Task<IActionResult> GetSalaries()
        {
            var salaries = await _service.GetSalaries();
            return Ok(salaries);
        }

        // GET: api/salaries/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSalary(Guid id)
        {
            try
            {
                var salary = await _service.GetSalary(id);
                return Ok(salary);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // PUT: api/salaries/{id}
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSalary(Guid id, SalaryDTO salaryDTO)
        {
            try
            {
                await _service.PutSalary(id, salaryDTO);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST: api/salaries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostSalary(SalaryDTO salaryDTO)
        {
            await _service.PostSalary(salaryDTO);
            return CreatedAtAction(nameof(GetSalary), new { id = Guid.NewGuid() }, salaryDTO);
        }

        // DELETE: api/salaries/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalary(Guid id)
        {
            try
            {
                await _service.DeleteSalary(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}