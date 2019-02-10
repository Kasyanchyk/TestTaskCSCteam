using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using TestTaskCSCteam.Models;

namespace TestTaskCSCteam.Utilities
{
    public class RepositoryChild<T, P> : Repository<T>, IRepositoryChild<T, P>
        where T : BaseEntityChild<P>
        where P : BaseEntity
    {
        private readonly DbSet<P> _entitiesP;
        private DataContext _context;

        public RepositoryChild(DataContext context,
            ILogger<T> logger) : base(context, logger)
        {
            _context = context;
            _entitiesP = context.Set<P>();
        }

        public IQueryable<T> GetItemsByParentId(int id, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null)
        {
            _logger.LogInformation("Getting {0} items by {1} id", typeof(P), typeof(T));
            var objectP = _entitiesP.FirstOrDefault(x => x.Id == id);
            if (objectP == null)
            {
                return null;
            }
            IQueryable<T> query = _entities.Where(x => x.Parent == objectP);
            if (includes != null)
            {
                query = includes(query);
            }
            
            var list = query.ToList()
                .Select(c => { c.Parent = null; return c; });

            return list.AsQueryable();
        }
    }
}
