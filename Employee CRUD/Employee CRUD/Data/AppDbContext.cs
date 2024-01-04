using Employee_CRUD.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Employee_CRUD.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }
    }
}
