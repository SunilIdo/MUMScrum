using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUMScrum.Data.Initializer
{
    class ApplicationDbInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            //  InitializeIdentityForEF(context);
            base.Seed(context);
        }

        private void InitializeIdentityForEF(ApplicationDbContext context)
        {
            //const string name = "admin@example.com";
            //const string email = "sumendra@amniltech.com";
            //const string password = "Admin@123456";
            //const string roleName = "Admin";
            //if (!context.Roles.Any(role => string.Compare(role.Name, roleName, StringComparison.CurrentCultureIgnoreCase) == 0))
            //{
            //    var rolestore = new RoleStore<ApplicationRole, Guid, ApplicationUserRole>(context);
            //    var rolemanager = new RoleManager<ApplicationRole, Guid>(rolestore);
            //    rolemanager.Create(new ApplicationRole { Name = roleName });
            //}

            //if (!context.Users.Any(user => string.Compare(user.UserName, name, StringComparison.CurrentCultureIgnoreCase) == 0))
            //{
            //    var userstore = new UserStore<ApplicationUser, ApplicationRole, Guid, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>(context);
            //    var usermanager = new UserManager<ApplicationUser, Guid>(userstore);
            //    var user = new ApplicationUser { UserName = name, Email = email, EmailConfirmed = true, LockoutEnabled = false };
            //    usermanager.Create(user, password);
            //    usermanager.AddToRole(user.Id, roleName);
            //}
        }
    }
}
