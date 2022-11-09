using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Taskker.Models.DAL;


namespace Taskker.Models
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        internal TaskkerContext context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(TaskkerContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            /*
             Podriamos llamar a este metodo sin argumentos, lo que devolveria toda la info
             de la DB. Pero en el caso de necesitar filtros, deberian hacerse en memoria
             lo que conlleva una sobrecarga en el servidor.
             Especificando argumentos (funcion lambda) para el metodo delegamos los filtrados
             a la base de datos.
             */

            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach(var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            } else {
                return query.ToList();
            }
        }
        public virtual TEntity GetByID(object id)
        {
            return dbSet.Find(id);
        }
        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }
        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }
        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }
        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            
            EntityState state = context.Entry(entityToUpdate).State;
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}