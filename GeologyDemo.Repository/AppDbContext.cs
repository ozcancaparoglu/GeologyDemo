using GeologyDemo.Domain.Common;
using GeologyDemo.Domain.ExperimentAggregate;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace GeologyDemo.Repository
{
    public class AppDbContext : DbContext
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        protected AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        #region Domains

        public DbSet<Area> Areas { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Experiment> Experiments { get; set; }

        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => !string.IsNullOrEmpty(type.Namespace))
                .Where(type => type.BaseType != null && type.BaseType.IsGenericType
                && type.BaseType.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>));
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(configurationInstance);
            }
            base.OnModelCreating(modelBuilder);
        }

        private void PreInsertListener()
        {
            foreach (var entity in ChangeTracker.Entries<DomainBase>().Where(x => x.State == EntityState.Added).ToList())
            {
                entity.Entity.CreatedDate = DateTime.Now;
                entity.Entity.State = (int)State.Active;
                try
                {
                    entity.Entity.ProcessedBy = int.Parse(httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value);
                }
                catch
                {
                    entity.Entity.ProcessedBy = 0;
                }

            }
        }

        private void UpdateListener()
        {
            foreach (var entity in ChangeTracker.Entries<DomainBase>().Where(x => x.State == EntityState.Modified).ToList())
            {
                entity.Entity.UpdatedDate = DateTime.Now;
                try
                {
                    entity.Entity.ProcessedBy = int.Parse(httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value);
                }
                catch
                {
                    entity.Entity.ProcessedBy = 0;
                }
            }
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            PreInsertListener();
            UpdateListener();
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
