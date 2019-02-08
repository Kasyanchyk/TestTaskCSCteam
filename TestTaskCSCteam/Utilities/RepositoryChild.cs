using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTaskCSCteam.Models;

namespace TestTaskCSCteam.Utilities
{
    public class RepositoryChild<T,P> : Repository<T>, IRepositoryChild<T,P>
        where T : BaseEntityChild<P>
        where P : BaseEntity
    {
        private readonly DbSet<P> _entitiesP;
        private DataContext _context;

        public RepositoryChild(DataContext context) :base(context)
        {
            _context = context;
            _entitiesP = context.Set<P>();
        }

        public IQueryable<T> GetItemsByParentId(int id, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null)
        {
            var objectP = _entitiesP.FirstOrDefault(x=>x.Id==id);
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
