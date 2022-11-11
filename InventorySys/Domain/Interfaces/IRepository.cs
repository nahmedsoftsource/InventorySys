using Domain.Entities;
using System.Linq.Expressions;

namespace Domain.Interfaces
{
    public interface IRepository<TEntity, TPrimaryKey> where TEntity : class
    {
        IQueryable<TEntity> GetAll();
        Task<ICollection<TEntity>> GetAllAsyn();
        Task<ICollection<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match);
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match);
        IEnumerable<TEntity> GetWithRawSql(string query,
        params object[] parameters);
        Task<TEntity> GetById(TPrimaryKey id);

        Task<TEntity> GetByIdAsync(TPrimaryKey id);

        Task<TPrimaryKey> Create(TEntity entity);

        Task Update(TPrimaryKey id, TEntity entity);

        Task Delete(TPrimaryKey id);

    }

    public interface IRepository<TEntity> : IRepository<TEntity, long> where TEntity : class, IEntity
    {

    }

}
