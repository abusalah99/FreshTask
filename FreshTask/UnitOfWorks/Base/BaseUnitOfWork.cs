using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace FreshTask
{
    public class BaseUnitOfWork<TEntity> : IBaseUnitOfWork<TEntity> where TEntity : BaseEntity
    {
        private readonly IBaseRepository<TEntity> _repository;
        
        protected BaseUnitOfWork() => _repository = new BaseRepository<TEntity>();

        public virtual async Task<IEnumerable<TEntity>> Read() => await _repository.Get();
        public virtual async Task<TEntity> Read(Guid id) => await _repository.Get(id);

        public async Task Create(TEntity entity)
        {
            DbContextTransaction transaction = await _repository.GetTransaction();

            try
            {
                await _repository.Add(entity);

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
        }

        public async Task Update(TEntity entity)
        {
            DbContextTransaction transaction = await _repository.GetTransaction();

            try
            {
                await _repository.Update(entity);

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
        }

        public async Task Delete(Guid id)
        {
            DbContextTransaction transaction = await _repository.GetTransaction();

            try
            {
                await _repository.Remove(id);

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
        }
    }
}
