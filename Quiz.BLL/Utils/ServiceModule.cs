using Ninject.Modules;
using Quiz.DAL.Interfaces;
using Quiz.DAL.Repositories;

namespace Quiz.BLL.Utils
{
    public class ServiceModule : NinjectModule
    {
        private readonly string connectionString;

        public ServiceModule(string connection)
        {
            connectionString = connection;
        }

        public override void Load()
        {
            Bind<IUnitOfWork>()
                .To<UnitOfWork>()
                .WithConstructorArgument(connectionString);
        }
    }
}
