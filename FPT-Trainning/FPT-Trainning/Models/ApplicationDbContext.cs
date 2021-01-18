using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace FPT_Trainning.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public DbSet<Course> Courses { get; set; }
    }
}