using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using WebApplication.Models;
using WebApplication.Profiles;
using WebApplication.Repositories;
using WebApplication.Services;

var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<ApplicationDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();

builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IProjectService, ProjectService>();

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

builder.Services.AddScoped<IProjectEmployeeRepository, ProjectEmployeeRepository>();
builder.Services.AddScoped<IProjectEmployeeService, ProjectEmployeeService>();

builder.Services.AddScoped<ISalaryRepository, SalaryRepository>();
builder.Services.AddScoped<ISalaryService, SalaryService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

SeedData(app);

app.Run();

void SeedData(Microsoft.AspNetCore.Builder.WebApplication app)
{
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        // context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        if (!context.Departments.Any()
            && !context.Projects.Any()
            && !context.Employees.Any()
            && !context.ProjectEmployees.Any()
            && !context.Salaries.Any())
        {
            // Seeding Departments
            Guid softwareDevelopmentId = Guid.NewGuid();
            Guid financeId = Guid.NewGuid();
            Guid accountantId = Guid.NewGuid();
            Guid hrId = Guid.NewGuid();

            context.Departments.AddRange(
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

            context.Employees.AddRange(
                new Employee { Id = employee1Id, Name = "Đặng Phan Thành Công", DepartmentId = softwareDevelopmentId, JoinedDate = new DateTime(2024, 4, 1) },
                new Employee { Id = employee2Id, Name = "Nguyễn Mỹ Linh", DepartmentId = financeId, JoinedDate = new DateTime(2024, 1, 1) },
                new Employee { Id = employee3Id, Name = "Trần Mai Phương", DepartmentId = accountantId, JoinedDate = new DateTime(2023, 7, 1) },
                new Employee { Id = employee4Id, Name = "Phạm Thu Hà", DepartmentId = hrId, JoinedDate = new DateTime(2023, 1, 1) }
            );

            // Seeding Salaries
            context.Salaries.AddRange(
                new Salary { Id = Guid.NewGuid(), EmployeeId = employee1Id, SalaryAmount = 100 },
                new Salary { Id = Guid.NewGuid(), EmployeeId = employee2Id, SalaryAmount = 120 },
                new Salary { Id = Guid.NewGuid(), EmployeeId = employee3Id, SalaryAmount = 150 },
                new Salary { Id = Guid.NewGuid(), EmployeeId = employee4Id, SalaryAmount = 200 }
            );

            // Seeding Projects
            Guid projectAId = Guid.NewGuid();
            Guid projectBId = Guid.NewGuid();

            context.Projects.AddRange(
                new Project { Id = projectAId, Name = "Project A" },
                new Project { Id = projectBId, Name = "Project B" }
            );

            // Seeding ProjectEmployee
            context.ProjectEmployees.AddRange(
                new ProjectEmployee { Id = Guid.NewGuid(), ProjectId = projectAId, EmployeeId = employee1Id, Enable = true },
                new ProjectEmployee { Id = Guid.NewGuid(), ProjectId = projectAId, EmployeeId = employee2Id, Enable = true },
                new ProjectEmployee { Id = Guid.NewGuid(), ProjectId = projectAId, EmployeeId = employee3Id, Enable = true },
                new ProjectEmployee { Id = Guid.NewGuid(), ProjectId = projectAId, EmployeeId = employee4Id, Enable = true },

                new ProjectEmployee { Id = Guid.NewGuid(), ProjectId = projectBId, EmployeeId = employee2Id, Enable = true },
                new ProjectEmployee { Id = Guid.NewGuid(), ProjectId = projectBId, EmployeeId = employee3Id, Enable = true },
                new ProjectEmployee { Id = Guid.NewGuid(), ProjectId = projectBId, EmployeeId = employee4Id, Enable = true }
            );
        }
        context.SaveChanges();
    }
}