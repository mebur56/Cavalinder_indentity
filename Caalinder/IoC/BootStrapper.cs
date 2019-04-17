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

namespace Caalinder.IoC
{
    public static class BootStrapper
    {
        public static Container Register(Container container, Lifestyle hybridLifestyle)
        {

            //Identity
            container.Register<ApplicationDbContext>(Lifestyle.Scoped);
            container.Register<IRoleStore<IdentityRole, string>>(() => new RoleStore<IdentityRole>(), Lifestyle.Scoped);
            container.Register<ApplicationUserManager>(Lifestyle.Scoped);
            //container.Register<ApplicationRoleManager>(Lifestyle.Scoped);
            container.Register<ApplicationSignInManager>(Lifestyle.Scoped);
            container.Register<IUserStore<ApplicationUser>>(
            () => new CustomUserStore(container.GetInstance<ApplicationDbContext>()), Lifestyle.Scoped);
            container.Register(
                () => new UserManager<ApplicationUser, int>(
                    new CustomUserStore(
                        container.GetInstance<ApplicationDbContext>())), Lifestyle.Scoped);

            #region APP
            container.Register<IUsuarioAppService, UsuarioAppService>(hybridLifestyle);
            container.Register<IHorseAppService, HorseAppService>(hybridLifestyle);
            #endregion

            #region Service
            container.Register<IGenericService<UserModel>, GenericService<UserModel>>(hybridLifestyle);
            container.Register<IGenericService<HorseModel>, GenericService<HorseModel>>(hybridLifestyle);
            #endregion

            #region Infra_Data
            container.Register<IUsuarioRepository, UsuarioRepository>(hybridLifestyle);
            container.Register<IHorseRepository, HorseRepository>(hybridLifestyle);
            container.Register<IUnitOfWork, UnitOfWork>(hybridLifestyle);
            container.Register<IGenericRepository<UserModel>, GenericRepository<UserModel>>(hybridLifestyle);
            container.Register<IGenericRepository<HorseModel>, GenericRepository<HorseModel>>(hybridLifestyle);

            //container.Register<IUserStore<ApplicationUser>>

            #endregion
            container.Register<ApplicationContext>(hybridLifestyle);


            return container;
        }
    }
}