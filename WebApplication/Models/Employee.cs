using System.ComponentModel.DataAnnotations;

namespace WebAppication.Models
{
    public class Employee
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(50)]
        public required string Name { get; set; }
        public Guid DepartmentId { get; set; }
        public Department Department { get; set; }
        public DateTime JoinedDate { get; set; }
        public Salary Salary { get; set; }
        public ICollection<ProjectEmployee> ProjectEmployees { get; set; }
    }
}