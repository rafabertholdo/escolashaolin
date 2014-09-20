using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaShaolin.Framework.Persistence
{
    public interface IRepository
    {
        Task SaveCopyAsync(IEntity<BaseEntity> entity);
        Task SaveAsync(IEntity<BaseEntity> entity, bool isInsert);
        Task DeleteAsync(IEntity<BaseEntity> entity);
        Task<bool> LoadCopyAsync(object entity, params string[] includes);
        Task<object> LoadAsync(object entity, params string[] includes);
        Task<IEnumerable<BaseEntity>> LoadAllAsync(Type entityType);
    }
}
