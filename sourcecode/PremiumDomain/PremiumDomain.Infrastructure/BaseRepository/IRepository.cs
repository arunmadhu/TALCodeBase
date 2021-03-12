using System.Linq;

namespace PremiumDomain.Infrastructure
{
    public interface IRepository<TEntity> where TEntity : class, new()
    {
        IQueryable<TEntity> GetAll();
    }
}
