using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaShaolin.Framework.Persistence
{
    public interface IEntity<T>
    {
        Guid Id { get; set; }
        DateTime InsertDate { get; set; }
        DateTime LastUpdateDate { get; set; }
        
        bool SameIdentityAs(T other);
    }
}
