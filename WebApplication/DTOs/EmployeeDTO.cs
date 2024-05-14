namespace WebAppication.DTOs
{
    public class EmployeeDTO
    {
        public string Name { get; set; }
        public Guid DepartmentId { get; set; }
        public DateTime JoinedDate { get; set; }
        public Guid SalaryId { get; set; }
    }
}