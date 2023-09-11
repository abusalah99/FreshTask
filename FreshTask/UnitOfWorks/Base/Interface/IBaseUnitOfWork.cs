using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace FreshTask
{
    public interface IBaseUnitOfWork<TEntity> where TEntity : BaseEntity
    {
        Task Create(TEntity entity);
        Task<IEnumerable<TEntity>> Read();
        Task<TEntity> Read(Guid id);
        Task Update(TEntity entity);
        Task Delete(Guid id);
    }
}
