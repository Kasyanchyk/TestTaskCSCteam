﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using SoftServe.ITAcademy.BackendDubbingProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTaskCSCteam.Models;

namespace TestTaskCSCteam.Utilities
{
    public class Repository<T> : IRepository<T>
        where T : BaseEntity
    {
        private readonly DbSet<T> _entities;
        private DataContext _context;

        public Repository(DataContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public IEnumerable<T> GetAllItems(Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null)
        {
            IQueryable<T> query = _entities;

            if (includes != null)
            {
                query = includes(query);
            }

            return query.AsEnumerable();
        }

        public T GetItem(int id, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null)
        {
            IQueryable<T> query = _entities.Where(x => x.Id == id);

            if (includes != null)
            {
                query = includes(query);
            }

            return query.FirstOrDefault();
        }

        public void Create(T entity)
        {
            entity.Id = default(int);
            _entities.Add(entity);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            T exist = _entities.Find(entity.Id);
            _context.Entry(exist).CurrentValues.SetValues(entity);
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            _entities.Remove(entity);
            _context.SaveChanges();
        }
    }
}
