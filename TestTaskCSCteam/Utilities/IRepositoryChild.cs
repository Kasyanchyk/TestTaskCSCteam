using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTaskCSCteam.Utilities
{
    public interface IRepositoryChild<T,P>: IRepository<T>
        where T : class
    {
        IQueryable<T> GetItemsByParentId(int id, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null);
    }
}
