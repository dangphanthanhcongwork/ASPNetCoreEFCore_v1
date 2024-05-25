using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.DTOs
{
    public class SalaryDTO
    {
        public Guid EmployeeId { get; set; }

        public decimal SalaryAmount { get; set; }
    }
}