using Ninject.Modules;
using Quiz.BLL.Interfaces;
using Quiz.BLL.Services;

namespace Quiz.Web.API.Utils
{
    public class UserModule : NinjectModule
    {
        public override void Load()
        {
			Bind<IUserService>().To<UserService>();
		}
    }
}
