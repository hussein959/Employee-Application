using Microsoft.EntityFrameworkCore;
using webWithSQL.Models;
namespace webWithSQL.Models
{
    //DbContext is Entity that conect the files with the db
    public class HRDatabaseContext:DbContext
    {
        public DbSet<Department> Departments { get; set; }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"data source=HUSSEIN-DESKTOP\MSSQLSERVER01; initial catalog=EmployeesDB; integrated security=SSPI; encrypt = false;");
        }

    }
}


