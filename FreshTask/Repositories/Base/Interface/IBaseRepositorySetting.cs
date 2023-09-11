using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreshTask
{
    public interface IBaseRepositorySetting<TEntity> 
        : IBaseRepository<TEntity> where TEntity : BaseEntitySetting
    {
        Task<IEnumerable<TEntity>> Search(string searchText);
    }
}
