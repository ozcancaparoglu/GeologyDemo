using Autofac;
using GeologyDemo.Container.Modules;

namespace GeologyDemo.Container
{
    public class Bootstrapper
    {
        public static ILifetimeScope Container { get; private set; }

        public static void RegisterModules(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterModule(new MediatRModule());
            containerBuilder.RegisterModule(new RepositoryModule());
            containerBuilder.RegisterModule(new ApplicationModule());
        }

        public static void SetContainer(ILifetimeScope autofacContainer)
        {
            Container = autofacContainer;
        }
    }
}
