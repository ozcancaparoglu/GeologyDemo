using Autofac;
using AutoMapper;
using GeologyDemo.Application.CacheServices;
using GeologyDemo.Application.CacheServices.Redis;
using Module = Autofac.Module;

namespace GeologyDemo.Container.Modules
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RedisServer>().SingleInstance();
            builder.RegisterType(typeof(Mapper)).As(typeof(IMapper)).AsSelf().InstancePerLifetimeScope();
            builder.RegisterType(typeof(RedisCacheService)).As(typeof(ICacheService)).AsSelf().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(System.Reflection.Assembly.Load("GeologyDemo.Application"))
              .Where(t => t.Name.EndsWith("Service"))
              .AsImplementedInterfaces()
              .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(System.Reflection.Assembly.Load("GeologyDemo.Application"))
               .Where(t => t.Name.EndsWith("Configuration"))
               .AsImplementedInterfaces()
               .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
