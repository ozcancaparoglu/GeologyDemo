using Autofac;
using GeologyDemo.Domain;
using GeologyDemo.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Module = Autofac.Module;

namespace GeologyDemo.Container.Modules
{
    public class RepositoryModule : Module
    {
        private static string _connectionString;
        public static void AddDbContext(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            _connectionString = configuration["DbConnString"];
            serviceCollection.AddDbContext<AppDbContext>(options => options.UseNpgsql(_connectionString));
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType(typeof(UnitOfWork)).As(typeof(IUnitOfWork)).AsSelf().InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).AsSelf().InstancePerLifetimeScope();
            builder.RegisterType(typeof(HttpContextAccessor)).As(typeof(IHttpContextAccessor)).AsSelf().InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
