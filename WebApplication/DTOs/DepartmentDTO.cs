using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.DTOs
{
    public class DepartmentDTO
    {
        [Required]
        public string Name { get; set; }
    }
}