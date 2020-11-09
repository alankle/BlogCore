using BlogCore.AccesoDatos.Data;
using BlogCore.AccesosDatos.Data.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BlogCore.AccesosDatos.Data
{
    public class Repository<T> : IRepository<T> where T : class
    {

        protected readonly DbContext Context;
        internal DbSet<T> dbset;

        public Repository(DbContext context)
        {
            Context = context;
            this.dbset = context.Set<T>();
        }

        public void Add(T entity)
        {
            dbset.Add(entity);
        }
        public T get(int id)
        {
            return dbset.Find(id);

        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, string includeProperties = null)
        {

            IQueryable<T> query = dbset;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

            }
            if (orderby != null)
            {
                return orderby(query).ToList();
            }

            return query.ToList();

        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<T> query = dbset;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

            }

            return query.FirstOrDefault();

        }

        public void Remove(int id)
        {
            T entityToRemove = dbset.Find(id);
            Remove(entityToRemove);
        }

        public void Remove(T entity)
        {
            dbset.Remove(entity);
        }
    }
}
