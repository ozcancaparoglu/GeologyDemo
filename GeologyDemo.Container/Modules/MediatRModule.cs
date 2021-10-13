using Autofac;
using GeologyDemo.Application.Handler;
using GeologyDemo.Container.Decorator;
using GeologyDemo.Contract;
using MediatR;
using System.Reflection;
using Module = Autofac.Module;

namespace GeologyDemo.Container.Modules
{
    public class MediatRModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();

            builder.Register<ServiceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });


            builder.RegisterAssemblyTypes(typeof(HealthCheckQuery).Assembly)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(HealthCheckQueryHandler).Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(ExceptionHandler<,>)).As(typeof(IPipelineBehavior<,>));

            base.Load(builder);
        }
    }
}
