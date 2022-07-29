using employeeApp6._0.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace employeeApp6._0.Data
{
    public class mvcDemoDbContext:DbContext
    {
        public mvcDemoDbContext(DbContextOptions options):base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
    }
}
