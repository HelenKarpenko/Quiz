using Ninject.Modules;
using Quiz.BLL.Interfaces;
using Quiz.BLL.Services;

namespace Quiz.Web.API.Utils
{
	public class TestResultsModule : NinjectModule
	{
		public override void Load()
		{
			Bind<ITestResultService>().To<TestResultService>();
		}
	}
}