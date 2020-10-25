using BasicXamarin.Contract;
using BasicXamarin.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using XamarinBasic.Core;

namespace BasicXamarin.Data
{
    public class Repository: IRepository
    {
        private readonly BasicDbContext _dbContext;
        private readonly IPlatformSettingsProvider _platformSettingsProvider;

       public Repository(IPlatformSettingsProvider platformSettingsProvider)
       {
            _platformSettingsProvider = platformSettingsProvider;
            _dbContext = new BasicDbContext(_platformSettingsProvider.ConnectionString);
       }

        protected virtual IQueryable<TEntity> Set<TEntity>() where TEntity : class
        {
            return _dbContext.Set<TEntity>();
        }

        public async Task<TEntity> AddAsync<TEntity>(TEntity entity) where TEntity : class
        {
            var ent = _dbContext.Set<TEntity>().Add(entity);

            foreach (var navigation in ent.Navigations)
            {
                navigation.Load();
            }

            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> DeleteAsync<TEntity>(int id) where TEntity : class
        {
            var entity = await _dbContext.Set<TEntity>().FindAsync(id);
            if (entity == null)
            {
                return null;
            }

            _dbContext.Set<TEntity>().Remove(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<TEntity> GetAsync<TEntity>(Expression<Func<TEntity, bool>> whereExpression, IEnumerable<string> includes = null) where TEntity : class
        {
            IQueryable<TEntity> result = _dbContext.Set<TEntity>().AsNoTracking();

            if (whereExpression != null)
            {
                result = result.Where(whereExpression);
            }

            if (includes != null)
            {
                foreach (string include in includes)
                {
                    result = result.Include(include);

                }
            }

            return  await result.FirstOrDefaultAsync();
        }

        public async Task<TEntity> GetRandomAsync<TEntity>(IEnumerable<string> includes = null) where TEntity : class
        {
            var random = new Random();
            var skip = random.Next(0, _dbContext.Set<TEntity>().Count());
            IQueryable <TEntity> result = _dbContext.Set<TEntity>().AsNoTracking();

            if (includes != null)
            {
                foreach (string include in includes)
                {
                    result = result.Include(include);

                }
            }

            return await result.Skip(skip).Take(1).FirstAsync();
        }

        public async Task<IEnumerable<TEntity>> GetManyAsync<TEntity>(Expression<Func<TEntity, bool>> whereExpression = null, OrderElementDescription orderElementDescriptor = null, IEnumerable<string> includes = null) where TEntity : class
        {
            IQueryable<TEntity> result = _dbContext.Set<TEntity>().AsNoTracking();

            if (whereExpression != null)
            {
                result = result.Where(whereExpression);
            }

            if (orderElementDescriptor != null)
            {
                result = result.OrderBy(orderElementDescriptor);
            }

            if (includes != null)
            {
                foreach (string include in includes)
                {
                    result = result.Include(include);

                }
            }
      
            return await result.ToListAsync();
        }

        public async Task<TEntity> UpdateAsync<TEntity>(TEntity entity) where TEntity : class
        {
            var entry = _dbContext.Entry(entity);
            entry.State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
