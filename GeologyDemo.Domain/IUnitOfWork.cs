using GeologyDemo.Domain.Common;
using System.Threading.Tasks;

namespace GeologyDemo.Domain
{
    public interface IUnitOfWork
    {
        IGenericRepository<T> Repository<T>() where T : DomainBase;
        Task<int> CommitAsync();
        void Rollback();
    }
}
