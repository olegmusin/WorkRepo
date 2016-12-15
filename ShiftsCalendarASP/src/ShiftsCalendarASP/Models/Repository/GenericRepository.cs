using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftsCalendar.Models.Repository
{

    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private ShiftsCalendarContext _entities;
        private ILogger<GenericRepository<T>> _logger;

        public GenericRepository(ShiftsCalendarContext context, ILogger<GenericRepository<T>> logger)
        {
            _entities = context;
            _logger = logger;
        }


        public virtual IQueryable<T> GetAll()
        {
            _logger.LogInformation($"Getting all {typeof(T).Name}"+"s from database");
            IQueryable<T> query = _entities.Set<T>();
            return query;
        }

        public virtual IQueryable<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {

            IQueryable<T> query = _entities.Set<T>().Where(predicate);
            return query;
        }

        public virtual void Add(T entity)
        {
            _entities.Set<T>().Add(entity);
        }

        public virtual void Delete(T entity)
        {
            _entities.Set<T>().Remove(entity);
        }

        public virtual void Edit(T entity)
        {
            _entities.Entry(entity).State = EntityState.Modified;
        }
    

        public virtual async Task<bool> SaveChangesAsync()
        {
            return (await _entities.SaveChangesAsync()) > 0;
        }
    }
}
