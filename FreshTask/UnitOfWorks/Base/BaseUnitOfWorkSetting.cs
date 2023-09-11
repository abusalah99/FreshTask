using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreshTask
{
    public class BaseUnitOfWorkSetting<TEntity> : BaseUnitOfWork<TEntity> 
        ,IBaseUnitOfWorkSetting<TEntity> where TEntity : BaseEntitySetting
    {
        private readonly IBaseRepositorySetting<TEntity> _repository;

        protected BaseUnitOfWorkSetting() => _repository = new BaseRepositorySetting<TEntity>();

        public virtual async Task<IEnumerable<TEntity>> Search(string searchText) => await _repository.Search(searchText);
    }
}