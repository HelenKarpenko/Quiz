using Ninject.Modules;
using Quiz.BLL.Interfaces;
using Quiz.BLL.Services;

namespace Quiz.Web.API.Utils
{
    public class AnswerModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IAnswerService>().To<AnswerService>();
        }
    }
}