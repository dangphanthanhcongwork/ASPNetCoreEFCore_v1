using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.DTOs
{
    public class EmployeeDTO
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public Guid DepartmentId { get; set; }

        public DateTime JoinedDate { get; set; }
    }
}