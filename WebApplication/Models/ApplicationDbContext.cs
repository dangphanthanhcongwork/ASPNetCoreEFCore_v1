using Microsoft.EntityFrameworkCore;
using WebAppication.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Department> Departments { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<ProjectEmployee> ProjectEmployees { get; set; }
    public DbSet<Salary> Salaries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // One-to-One relationship between Employee and Salary
        modelBuilder.Entity<Employee>()
            .HasOne(e => e.Salary)
            .WithOne(s => s.Employee)
            .HasForeignKey<Salary>(s => s.EmployeeId);

        // One-to-Many relationship between Department and Employee
        modelBuilder.Entity<Employee>()
            .HasOne(e => e.Department)
            .WithMany(d => d.Employees)
            .HasForeignKey(e => e.DepartmentId);

        // Many-to-Many relationship between Project and Employee
        modelBuilder.Entity<ProjectEmployee>()
            .HasKey(pe => new { pe.ProjectId, pe.EmployeeId });
        modelBuilder.Entity<ProjectEmployee>()
            .HasOne(pe => pe.Project)
            .WithMany(p => p.ProjectEmployees)
            .HasForeignKey(pe => pe.ProjectId);
        modelBuilder.Entity<ProjectEmployee>()
            .HasOne(pe => pe.Employee)
            .WithMany(e => e.ProjectEmployees)
            .HasForeignKey(pe => pe.EmployeeId);

        // Seeding Departments
        Guid softwareDevelopmentId = Guid.NewGuid();
        Guid financeId = Guid.NewGuid();
        Guid accountantId = Guid.NewGuid();
        Guid hrId = Guid.NewGuid();

        modelBuilder.Entity<Department>().HasData(
            new Department { Id = softwareDevelopmentId, Name = "Software Development" },
            new Department { Id = financeId, Name = "Finance" },
            new Department { Id = accountantId, Name = "Accountant" },
            new Department { Id = hrId, Name = "HR" }
        );

        // Seeding Employees
        Guid employee1Id = Guid.NewGuid();
        Guid employee2Id = Guid.NewGuid();
        Guid employee3Id = Guid.NewGuid();
        Guid employee4Id = Guid.NewGuid();

        modelBuilder.Entity<Employee>().HasData(
            new Employee { Id = employee1Id, Name = "Đặng Phan Thành Công", DepartmentId = softwareDevelopmentId, JoinedDate = new DateTime(2024, 4, 1) },
            new Employee { Id = employee2Id, Name = "Nguyễn Mỹ Linh", DepartmentId = financeId, JoinedDate = new DateTime(2024, 1, 1) },
            new Employee { Id = employee3Id, Name = "Trần Thị Minh Phương", DepartmentId = accountantId, JoinedDate = new DateTime(2023, 7, 1) },
            new Employee { Id = employee4Id, Name = "Phan Thị Thu Hà", DepartmentId = hrId, JoinedDate = new DateTime(2023, 1, 1) }
        );

        // Seeding Salaries
        modelBuilder.Entity<Salary>().HasData(
            new Salary { Id = Guid.NewGuid(), EmployeeId = employee1Id, SalaryAmount = 100 },
            new Salary { Id = Guid.NewGuid(), EmployeeId = employee2Id, SalaryAmount = 120 },
            new Salary { Id = Guid.NewGuid(), EmployeeId = employee3Id, SalaryAmount = 150 },
            new Salary { Id = Guid.NewGuid(), EmployeeId = employee4Id, SalaryAmount = 200 }
        );

        // Seeding Projects
        Guid projectAId = Guid.NewGuid();
        Guid projectBId = Guid.NewGuid();

        modelBuilder.Entity<Project>().HasData(
            new Project { Id = projectAId, Name = "Project A" },
            new Project { Id = projectBId, Name = "Project B" }
        );

        // Seeding ProjectEmployee
        modelBuilder.Entity<ProjectEmployee>().HasData(
            new ProjectEmployee { ProjectId = projectAId, EmployeeId = employee1Id },
            new ProjectEmployee { ProjectId = projectAId, EmployeeId = employee2Id },
            new ProjectEmployee { ProjectId = projectAId, EmployeeId = employee3Id },
            new ProjectEmployee { ProjectId = projectAId, EmployeeId = employee4Id },

            new ProjectEmployee { ProjectId = projectBId, EmployeeId = employee2Id },
            new ProjectEmployee { ProjectId = projectBId, EmployeeId = employee3Id },
            new ProjectEmployee { ProjectId = projectBId, EmployeeId = employee4Id }

        );
    }
}