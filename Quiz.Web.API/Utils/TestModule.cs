using Ninject.Modules;
using Quiz.BLL.Interfaces;
using Quiz.BLL.Services;

namespace Quiz.Web.API.Utils
{
    public class TestModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ITestService>().To<TestService>();
        }
    }
}