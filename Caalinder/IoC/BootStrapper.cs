using Caalinder.Models;
using Caalinder.AppService;
using Caalinder.AppService.Interfaces;
using Caalinder.Controllers;
using Caalinder.Controllers.Interfaces;
using Caalinder.Data;
using Caalinder.Service;
using SimpleInjector;
using Caalinder.Data.Repositories;
using Caalinder.Data.Context;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin;
using System.Web;

namespace Caalinder.IoC
{
    public static class BootStrapper
    {
        public static Container Register(Container container, Lifestyle hybridLifestyle)
        {

            #region Service
            container.Register<IGenericService<HorseModel>, GenericService<HorseModel>>(hybridLifestyle);
            container.Register<IGenericService<MatchModel>, GenericService<MatchModel>>(hybridLifestyle);
            #endregion
            //Identity

            container.Register<ApplicationDbContext>(Lifestyle.Transient);
            container.Register<IRoleStore<IdentityRole, string>>(() => new RoleStore<IdentityRole>(), Lifestyle.Transient);
            container.Register<ApplicationUserManager>(Lifestyle.Transient);
            container.Register<ApplicationRoleManager>(Lifestyle.Transient);
            container.Register<ApplicationSignInManager>(Lifestyle.Transient);
            container.Register<IAuthenticationManager>(() => HttpContext.Current.GetOwinContext().Authentication);
            container.Register<IUserStore<ApplicationUser>>(() => new UserStore<ApplicationUser>(new ApplicationDbContext()), Lifestyle.Transient);

            #region APP
            container.Register<IHorseAppService, HorseAppService>(hybridLifestyle);
            #endregion

            //#region Service
            //container.Register<IGenericService<UserModel>, GenericService<UserModel>>(hybridLifestyle);
            //container.Register<IGenericService<HorseModel>, GenericService<HorseModel>>(hybridLifestyle);
            //#endregion

            #region Infra_Data
            container.Register<IHorseRepository, HorseRepository>(hybridLifestyle);
            container.Register<IUnitOfWork, UnitOfWork>(hybridLifestyle);
            container.Register<IGenericRepository<HorseModel>, GenericRepository<HorseModel>>(hybridLifestyle);
            container.Register<IGenericRepository<MatchModel>, GenericRepository<MatchModel>>(hybridLifestyle);

            //container.Register<IUserStore<ApplicationUser>>

            #endregion
            container.Register<ApplicationContext>(hybridLifestyle);


            return container;
        }
    }
}