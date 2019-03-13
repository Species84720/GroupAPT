using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Security;
//using System.Web.Security.Roles.Enable;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Test2.Models.DBModels;

namespace Test2.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string NickName { get; set; }
        public int? DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public virtual Department RelatedDepartment { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    

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

        
        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().ToTable("AllUsers");
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.UserId, r.RoleId }).ToTable("UserRole");
            modelBuilder.Entity<IdentityRole>().ToTable("Role");

            

        } //changes  default name of database tables

        public System.Data.Entity.DbSet<Test2.Models.DBModels.Department> Departments { get; set; }

        public System.Data.Entity.DbSet<Test2.Models.DBModels.Student> Students { get; set; }

        public System.Data.Entity.DbSet<Test2.Models.DBModels.Enrollment> Enrollments { get; set; }

        public System.Data.Entity.DbSet<Test2.Models.DBModels.Subject> Subjects { get; set; }

        public System.Data.Entity.DbSet<Test2.Models.DBModels.Location> Locations { get; set; }

        // if any scaffold adds an ApplicationUser DbSet here remove it.
        // in the new Controller change all references for ApplicationUsers to just Users
        // public System.Data.Entity.DbSet<Test2.Models.ApplicationUser> ApplicationUsers { get; set; }
    }
}