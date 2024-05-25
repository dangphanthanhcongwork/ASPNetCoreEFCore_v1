using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public class Salary
    {
        public Guid Id { get; set; }

        public Guid EmployeeId { get; set; }

        public decimal SalaryAmount { get; set; }

        public Employee Employee { get; set; }
    }
}