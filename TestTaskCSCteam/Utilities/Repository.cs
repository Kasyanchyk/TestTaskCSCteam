using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TestTaskCSCteam.Models;

namespace TestTaskCSCteam.Utilities
{
    public class Repository<T> : IRepository<T>
        where T : BaseEntity
    {
        protected readonly DbSet<T> _entities;
        private DataContext _context;
        public readonly ILogger _logger;

        public Repository(DataContext context,
            ILogger<T> logger)
        {
            _context = context;
            _entities = context.Set<T>();
            _logger = logger;
        }

        public IQueryable<T> GetAllItems(Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null)
        {
            _logger.LogInformation("Getting {0} items", typeof(T));

            IQueryable<T> query = _entities;

            if(query==null)
            {
                _logger.LogWarning("{0} items NOT FOUND", typeof(T));
            }

            if (includes != null)
            {
                query = includes(query);
            }

            return query;
        }

        public T GetItem(int id, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null)
        {
            _logger.LogInformation("Getting {0} item by id", typeof(T));

            IQueryable<T> query = _entities.Where(x => x.Id == id);

            if (query == null)
            {
                _logger.LogWarning("{0} item NOT FOUND", typeof(T));
            }

            if (includes != null)
            {
                query = includes(query);
            }

            return query.FirstOrDefault();
        }


        public void Create(T entity)
        {
            _logger.LogInformation("Creating {0} item", typeof(T));
            _entities.Add(entity);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _logger.LogInformation("Updating {0} item", typeof(T));
            T exist = _entities.Find(entity.Id);
            _context.Entry(exist).CurrentValues.SetValues(entity);
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            _logger.LogInformation("Deleting {0} item", typeof(T));
            _entities.Remove(entity);
            _context.SaveChanges();
        }
    }
}
