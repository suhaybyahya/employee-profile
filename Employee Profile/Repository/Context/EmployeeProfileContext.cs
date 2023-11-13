using Employee_Profile.Models;
using Microsoft.EntityFrameworkCore;

namespace Employee_Profile.Repository.Context
{
    public class DbInitializer
    {
        private readonly ModelBuilder modelBuilder;

        public DbInitializer(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
        }

        public void Seed()
        {
            modelBuilder.Entity<Department>().HasData(
                    new Department() { Id = 1, Name = "HR department" },
                    new Department() { Id = 2, Name = "Development department" }
             );
        }
    }

    public class EmployeeProfileContext : DbContext
    {
        public EmployeeProfileContext(DbContextOptions<EmployeeProfileContext> options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new DbInitializer(modelBuilder).Seed();
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
    }
}
