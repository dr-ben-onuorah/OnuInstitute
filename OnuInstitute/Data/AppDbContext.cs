using OnuInstitute.Models.Entities;
using Microsoft.EntityFrameworkCore;
using OnuInstitute.Models;

namespace OnuInstitute.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Content> Contents { get; set; }
    }
}