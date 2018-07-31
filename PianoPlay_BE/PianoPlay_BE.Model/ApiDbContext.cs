using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.Data.Entity;

namespace PianoPlay_BE.Model
{
    public class ApiDbContext : IdentityDbContext
    {
        public ApiDbContext() : base("ApiConnection")
        {
        }

        static ApiDbContext()
        {
            Database.SetInitializer<ApiDbContext>(new IdentityDbInit());
        }

        public static ApiDbContext Create()
        {
            return new ApiDbContext();
        }

        public DbSet<Songs> Songs { get; set; }
        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }

    public class IdentityDbInit : DropCreateDatabaseIfModelChanges<ApiDbContext>
    {
        protected override void Seed(ApiDbContext context)
        {
            PerformInitialSetup(context);
            base.Seed(context);
        }

        public void PerformInitialSetup(ApiDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<Account>(new UserStore<Account>(context));

            roleManager.Create(new IdentityRole()
            {
                Name = "Admin"
            });

            roleManager.Create(new IdentityRole()
            {
                Name = "User"
            });

            Account account = new Account()
            {
                UserName = "admin",
                Email = "admin@test.com",
                EmailConfirmed = true
            };

            userManager.Create(account, "Abc123!!!");

            userManager.AddToRole(account.Id, "Admin");
        }
    }
}
