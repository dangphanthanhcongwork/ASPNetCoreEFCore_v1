using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public class ProjectEmployee
    {
        public Guid Id { get; set; }

        public Guid ProjectId { get; set; }

        public Project Project { get; set; }

        public Guid EmployeeId { get; set; }

        public Employee Employee { get; set; }

        public bool Enable { get; set; }
    }
}