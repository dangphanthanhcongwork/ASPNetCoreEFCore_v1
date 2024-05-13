using System.ComponentModel.DataAnnotations;

namespace WebAppication.Models
{
    public class Department
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}