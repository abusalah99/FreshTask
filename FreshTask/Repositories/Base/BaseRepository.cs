using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace FreshTask
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly ApplicationDbContext _context;
        private static readonly object lockObject = new object();
        private static BaseRepository<TEntity> instance;
        protected DbSet<TEntity> dbSet;

        protected BaseRepository()
        {
            _context = ApplicationDbContext.Instance;
            dbSet = _context.Set<TEntity>();
        }

        public static BaseRepository<TEntity> BaseRepositoryInstance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockObject)
                    {
                        if (instance == null)
                        {
                            instance = new BaseRepository<TEntity>();
                        }
                    }
                }
                return instance;
            }
        }
        public virtual async Task Add(TEntity entity)
        {
            ThrowExceptionIfParameterNotSupplied(entity);

            await Task.Run(() => dbSet.Add(entity));
            await SaveChangesAsync();
        }

        public virtual async Task Remove(Guid id)
        {
            TEntity entityFromDb = await ThrowExceptionIfEntityNotExistsInDatabase(id);

            await Task.Run(() => dbSet.Remove(entityFromDb));
            await SaveChangesAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> Get() => await dbSet.ToListAsync();

        public virtual async Task<TEntity> Get(Guid id)
            => await dbSet.FirstOrDefaultAsync(e => e.Id == id);

        public virtual async Task Update(TEntity entity)
        {
            ThrowExceptionIfParameterNotSupplied(entity);
            TEntity entityFromDb = await ThrowExceptionIfEntityNotExistsInDatabase(entity);

            await Task.Run(() => _context.Entry(entityFromDb).CurrentValues.SetValues(entity));
            await SaveChangesAsync();
        }

        protected async Task SaveChangesAsync() => await _context.SaveChangesAsync();
        private static void ThrowExceptionIfParameterNotSupplied(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentException($"{typeof(TEntity).Name} was not provided.");
        }

        protected async Task<TEntity> ThrowExceptionIfEntityNotExistsInDatabase(TEntity entity)
        {
            TEntity entityFromDb = await Get(entity.Id);
            if (entityFromDb == null)
                throw new ArgumentException($"{typeof(TEntity).Name} was not found in DB");

            return entityFromDb;
        }

        protected async Task<TEntity> ThrowExceptionIfEntityNotExistsInDatabase(Guid id)
        {
            TEntity entityFromDb = await Get(id);
            if (entityFromDb == null)
                throw new ArgumentException($"{typeof(TEntity).Name} was not found in DB");

            return entityFromDb;
        }

        public async Task<DbContextTransaction> GetTransaction()
            => await Task.Run(() => _context.Database.BeginTransaction());
    }
}