using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace EscolaShaolin.Framework.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        void Init();
        void Save();
        Task SaveAsync();
        void Open();
        void Close();
        void UpdateEntry(object oldEntry, object newEntry);
        void EnlistTransaction(Transaction transaction);
        void AddMappingConfigurations(object modelBuilder);
        void RejectChanges();
    }
}
