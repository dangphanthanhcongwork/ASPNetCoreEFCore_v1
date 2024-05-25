using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.DTOs
{
    public class ProjectDTO
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}