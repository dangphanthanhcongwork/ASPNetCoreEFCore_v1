using System.ComponentModel.DataAnnotations;

namespace WebAppication.Models
{
    public class Project
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(50)]
        public required string Name { get; set; }
        public ICollection<ProjectEmployee> ProjectEmployees { get; set; }
    }
}