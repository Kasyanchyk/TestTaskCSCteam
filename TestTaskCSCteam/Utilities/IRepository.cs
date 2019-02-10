using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Linq;

namespace TestTaskCSCteam.Utilities
{
    public interface IRepository<T>
        where T : class
    {
        IQueryable<T> GetAllItems(Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null);

        T GetItem(int id, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null);

        void Create(T entity);

        void Update(T entity);

        void Delete(T entity);


    }
}
