using BasicXamarin.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BasicXamarin.Contract
{
    public interface IRepository
    {
        Task<IEnumerable<TEntity>> GetManyAsync<TEntity>(
            Expression<Func<TEntity, bool>> whereExpression = null,
            OrderElementDescription orderElementDescriptor = null,
            IEnumerable<string> includes = null
            )
            where TEntity : class;

        Task<TEntity> GetAsync<TEntity>(
            Expression<Func<TEntity, bool>> whereExpression = null,
            IEnumerable<string> includes = null)
            where TEntity : class;

        Task<TEntity> GetRandomAsync<TEntity>(
            IEnumerable<string> includes = null)
            where TEntity : class;

        Task<TEntity> AddAsync<TEntity>(TEntity entity)
             where TEntity : class;

        Task<TEntity> UpdateAsync<TEntity>(TEntity entity)
             where TEntity : class;

        Task<TEntity> DeleteAsync<TEntity>(int id)
             where TEntity : class;

        Task SaveAsync();
    }
}
