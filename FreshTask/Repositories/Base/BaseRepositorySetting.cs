using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreshTask
{
    public class BaseRepositorySetting<TEntity> : BaseRepository<TEntity>,
        IBaseRepositorySetting<TEntity> where TEntity : BaseEntitySetting
    {
        private static BaseRepositorySetting<TEntity> instance;
        private static readonly object lockObject = new object();

        protected BaseRepositorySetting() { }
        public static BaseRepositorySetting<TEntity> BaseRepositorySettingInstance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockObject)
                    {
                        if (instance == null)
                        {
                            instance = new BaseRepositorySetting<TEntity>();
                        }
                    }
                }
                return instance;
            }
        }
        public virtual async Task<IEnumerable<TEntity>> Search(string searchText)
            => await Task.Run(()=>dbSet.Where(e => e.Name.Contains(searchText)));
    }
}