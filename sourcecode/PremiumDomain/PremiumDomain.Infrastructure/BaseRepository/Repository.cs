using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PremiumDomain.Infrastructure
{
    /// <summary>
    /// Base repository implementation
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        protected readonly PremiumDbContext _repositoryContext;

        public Repository(PremiumDbContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public IQueryable<TEntity> GetAll()
        {
            return _repositoryContext.Set<TEntity>();
        }
    }
}
