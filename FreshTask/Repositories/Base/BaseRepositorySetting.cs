using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreshTask
{
    public class BaseRepositorySetting<TEntity> : BaseRepository<TEntity>,
        IBaseRepositorySetting<TEntity> where TEntity : BaseEntitySetting
    {
        public virtual async Task<IEnumerable<TEntity>> Search(string searchText)
            => await Task.Run(()=>dbSet.Where(e => e.Name.Contains(searchText)));
    }
}