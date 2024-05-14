using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppication.DTOs;
using WebAppication.Models;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalariesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SalariesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Salaries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalaryDTO>>> GetSalaries()
        {
            return await _context.Salaries
                .Select(s => new SalaryDTO
                {
                    EmployeeId = s.EmployeeId,
                    SalaryAmount = s.SalaryAmount
                }) // Map Salary to SalaryDTO
                .ToListAsync();
        }

        // GET: api/Salaries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SalaryDTO>> GetSalary(Guid id)
        {
            var salary = await _context.Salaries.FindAsync(id);

            if (salary == null)
            {
                return NotFound();
            }

            var dto = new SalaryDTO
            {
                EmployeeId = salary.EmployeeId,
                SalaryAmount = salary.SalaryAmount
            }; // Map Salary to SalaryDTO
            return dto;
        }

        // PUT: api/Salaries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSalary(Guid id, SalaryDTO salaryDTO)
        {
            var salary = await _context.Salaries.FindAsync(id);
            if (salary == null)
            {
                return NotFound();
            }

            salary.EmployeeId = salaryDTO.EmployeeId;
            salary.SalaryAmount = salaryDTO.SalaryAmount; // Map SalaryDTO to Salary

            _context.Entry(salary).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalaryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Salaries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SalaryDTO>> PostSalary(SalaryDTO salaryDTO)
        {
            var salary = new Salary
            {
                EmployeeId = salaryDTO.EmployeeId,
                SalaryAmount = salaryDTO.SalaryAmount
            }; // Map SalaryDTO to Salary

            _context.Salaries.Add(salary);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSalary", new { id = salary.Id }, new SalaryDTO
            {
                EmployeeId = salary.EmployeeId,
                SalaryAmount = salary.SalaryAmount
            }); // Return SalaryDTO
        }

        // DELETE: api/Salaries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalary(Guid id)
        {
            var salary = await _context.Salaries.FindAsync(id);
            if (salary == null)
            {
                return NotFound();
            }

            _context.Salaries.Remove(salary);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SalaryExists(Guid id)
        {
            return _context.Salaries.Any(e => e.Id == id);
        }
    }
}
