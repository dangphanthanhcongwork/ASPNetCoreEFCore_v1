namespace WebAppication.DTOs
{
    public class EmployeeProjectDTO
    {
        public string EmployeeName { get; set; }
        public IEnumerable<ProjectDTO> Projects { get; set; }
    }

}