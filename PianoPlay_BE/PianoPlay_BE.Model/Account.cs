using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PianoPlay_BE.Model
{
    public class Account : IdentityUser
    {
        public Account()
            : base()
        {
            FullName = "";
        }

        public Account(string userName) : base(userName)
        {
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<Account> manager, string authenticationType)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }

        public bool Status { get; set; }
        public string FullName { get; set; }

        public virtual Songs Songs { get; set; }
    }
}