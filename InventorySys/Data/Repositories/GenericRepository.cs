using Data.Context;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repositories
{
    public class GenericRepository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        private readonly SqlDbContext _dbContext;
        internal DbSet<TEntity> dbSet;

        //public GenericRepository()
        //{
        //    this._context = new DTADbContext();
        //    this.dbSet = _dbContext.Set<TEntity>();
        //}

        public GenericRepository(SqlDbContext dbContext)
        {
            _dbContext = dbContext;
            this.dbSet = _dbContext.Set<TEntity>();
        }
        public IQueryable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>();
        }

        public async Task<TEntity> GetById(TPrimaryKey id)
        {
            return await _dbContext.Set<TEntity>()
                .FirstOrDefaultAsync(e => e.Id.Equals(id));
        }

        public virtual IEnumerable<TEntity> GetWithRawSql(string query,
       params object[] parameters)
        {
            return dbSet.FromSqlRaw(query, parameters).ToList();
        }

        public async Task<TPrimaryKey> Create(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task Update(TPrimaryKey id, TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(TPrimaryKey id)
        {

            var entity = await _dbContext.Set<TEntity>().FindAsync(id);
            _dbContext.Set<TEntity>().Remove(entity);
            await _dbContext.SaveChangesAsync();

        }

        public async Task<ICollection<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match)
        {
            return await _dbContext.Set<TEntity>().Where(match).ToListAsync();
        }

        public async Task<ICollection<TEntity>> GetAllAsyn()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(TPrimaryKey id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match)
        {
            return await _dbContext.Set<TEntity>().SingleOrDefaultAsync(match);
        }
    }
    public class GenericRepository<TEntity> : GenericRepository<TEntity, long>, IRepository<TEntity> where TEntity : class, IEntity
    {
        public GenericRepository(SqlDbContext _dbContext) : base(_dbContext)
        {

        }
    }
}
