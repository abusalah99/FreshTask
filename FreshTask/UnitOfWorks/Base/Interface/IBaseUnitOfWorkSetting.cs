using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreshTask
{
    public interface IBaseUnitOfWorkSetting<TEntity> : IBaseUnitOfWork<TEntity>
       where TEntity : BaseEntitySetting
    {
        Task<IEnumerable<TEntity>> Search(string searchText);
    }
}
