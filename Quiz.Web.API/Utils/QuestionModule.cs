using Ninject.Modules;
using Quiz.BLL.Interfaces;
using Quiz.BLL.Services;

namespace Quiz.Web.API.Utils
{
    public class QuestionModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IQuestionService>().To<QuestionService>();
        }
    }
}