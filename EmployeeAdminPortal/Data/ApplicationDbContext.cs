using EmployeeAdminPortal.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAdminPortal.Data
{
    //public class ApplicationDbContext: DbContext
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        //DbContextOptions <type> name: pass the name to the base class
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) //ctor (enter, to create a constructor)
        {
            
        }

        //<The Employee class created in the Entity>
        public DbSet<Employee> Employees { get; set; }

    }

}
