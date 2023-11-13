using Employee_Profile.AutoMapper;
using Employee_Profile.Managers;
using Employee_Profile.Managers.Interfaces;
using Employee_Profile.Repository;
using Employee_Profile.Repository.Context;
using Employee_Profile.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<EmployeeProfileContext>(item => item.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(MappingProfiles));

builder.Services.AddTransient<Employee_Profile.Logger.ILogger, Employee_Profile.Logger.Logger>();
builder.Services.AddTransient<IDepartmentsManager, DepartmentsManager>();
builder.Services.AddTransient<IDepartmentsRepository, DepartmentsRepository>();
builder.Services.AddTransient<IEmployeesManager, EmployeesManager>();
builder.Services.AddTransient<IEmployeesRepository, EmployeesRepository>();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<EmployeeProfileContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();



app.Run();
