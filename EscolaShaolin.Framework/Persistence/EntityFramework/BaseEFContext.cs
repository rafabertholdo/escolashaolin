using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace EscolaShaolin.Framework.Persistence.EntityFramework
{
    public abstract class BaseEFContext : DbContext, IUnitOfWork
    {
        /// <summary>
        /// Para proposito de migração
        /// </summary>
        public BaseEFContext()
            : base("ctxHospitale")
        {
            Init();
        }

        public BaseEFContext(string connString)
            : base(connString)
        {
            Init();
        }

        [Dependency]
        public IUnityContainer IoCContainer { get; set; }

        public string SchemaName { get; set; }

        #region Metodos interface IUnitOfWork

        public void Init()
        {
            this.SetInitializer();            
            this.Configuration.ProxyCreationEnabled = false;
            this.Configuration.LazyLoadingEnabled = false;
            ((IObjectContextAdapter)this).ObjectContext.ObjectMaterialized += ObjectContext_ObjectMaterialized;
        }

        public abstract void SetInitializer();
        
        public void Open()
        {
            var connection = ((IObjectContextAdapter)this).ObjectContext.Connection;
            if (connection.State == System.Data.ConnectionState.Closed)
                ((IObjectContextAdapter)this).ObjectContext.Connection.Open();
        }

        public void Close()
        {
            ((IObjectContextAdapter)this).ObjectContext.Connection.Close();
        }

        public void Save()
        {
            this.SaveChanges();
        }

        public virtual async Task SaveAsync()
        {
            await this.SaveChangesAsync();
        }

        public void EnlistTransaction(Transaction transaction)
        {
            ((IObjectContextAdapter)this).ObjectContext.Connection.EnlistTransaction(transaction);
        }

        public virtual void AddMappingConfigurations(object modelBuilder)
        {
            ((DbModelBuilder)modelBuilder).Conventions.Remove<PluralizingTableNameConvention>();
            ((DbModelBuilder)modelBuilder).Conventions.Remove<OneToManyCascadeDeleteConvention>();            
        }

        public virtual void UpdateEntry(object oldEntry, object newEntry)
        {
            this.Entry(oldEntry).CurrentValues.SetValues(newEntry);
        }

        #endregion Metodos interface IUnitOfWork


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            if (this.SchemaName != null)
            {
                modelBuilder.HasDefaultSchema(this.SchemaName);
            }

            AddMappingConfigurations(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        protected void ObjectContext_ObjectMaterialized(object sender, ObjectMaterializedEventArgs e)
        {
            if (this.IoCContainer != null)
                this.IoCContainer.BuildUp(e.Entity);
        }


        public void RejectChanges()
        {
            var context = ((IObjectContextAdapter)this).ObjectContext;
            foreach (var change in this.ChangeTracker.Entries())
            {
                if (change.State == EntityState.Modified)
                {
                    context.Refresh(RefreshMode.StoreWins, change.Entity);
                }
                if (change.State == EntityState.Added)
                {
                    context.Detach(change.Entity);
                }
            }
        }
    }
}
