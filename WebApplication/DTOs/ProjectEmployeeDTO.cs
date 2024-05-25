using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.DTOs
{
    public class ProjectEmployeeDTO
    {
        public Guid ProjectId { get; set; }

        public Guid EmployeeId { get; set; }

        public bool Enable { get; set; }
    }
}