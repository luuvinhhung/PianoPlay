using Autofac;
using Microsoft.AspNet.Identity.Owin;
using  PianoPlay_BE.Model;
using PianoPlay_BE.Infrastructure;
using System.Data.Entity;
using System.Web;

namespace PianoPlay_BE.Modules
{
    public class EFModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType(typeof(ApiDbContext)).As(typeof(DbContext)).InstancePerLifetimeScope();

            builder.Register<ApplicationUserManager>(c => HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>()).InstancePerRequest();
            builder.Register<ApplicationRoleManager>(c => HttpContext.Current.GetOwinContext().GetUserManager<ApplicationRoleManager>()).InstancePerRequest();
        }
    }
}