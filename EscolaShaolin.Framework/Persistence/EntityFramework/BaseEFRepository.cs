//using Arcanjo.Util.BusinessEvents;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EscolaShaolin.Framework.Persistence.EntityFramework
{
    public abstract class BaseEFRepository : IRepository
    {
        //protected abstract DbContext BaseContext { get; }
        protected abstract IUnitOfWork UnitOfWork { get; }

        private static object _lockAddVersion = new object();

        public async Task SaveCopyAsync(IEntity<BaseEntity> entity)
        {
            var set = GetDbSet(entity.GetType());
            //DbSet set = BaseContext.Set(entity.GetType());

            IEntity<BaseEntity> efEntity = (IEntity<BaseEntity>)set.Find(((BaseEntity)entity).GetKeys());
            if (efEntity == null)
            {
                entity.InsertDate = DateTime.Now;
                entity.LastUpdateDate = entity.InsertDate;
                set.Add(entity as dynamic);                
            }
            else
            {
                entity.LastUpdateDate = DateTime.Now;
                if (entity.InsertDate == DateTime.MinValue)
                    entity.InsertDate = DateTime.Now;
                //throw new Exception("Can not save an unloaded entity");
                else
                    entity.InsertDate = efEntity.InsertDate;
                UnitOfWork.UpdateEntry(efEntity, entity);                
            }
            await UnitOfWork.SaveAsync();
        }

        private dynamic GetDbSet(Type EntityType)
        {
            return UnitOfWork.GetType().GetProperties().Where(e =>
            {
                var GenericArgument = e.PropertyType.GetGenericArguments().FirstOrDefault();

                if (GenericArgument == null)
                    return false;
                else
                    return EntityType.IsAssignableFrom(GenericArgument);
            }).FirstOrDefault().GetValue(UnitOfWork) as dynamic; 
        }

        public async Task SaveAsync(IEntity<BaseEntity> entity, bool isInsert)
        {
            //DbSet set = BaseContext.Set(entity.GetType());
            var set = GetDbSet(entity.GetType());

            entity.LastUpdateDate = DateTime.Now;
            if (isInsert)
            {
                if (entity.Id == default(Guid))
                    entity.Id = Guid.NewGuid();
                entity.InsertDate = entity.LastUpdateDate;
                set.Add(entity as dynamic);
            }
            try
            {
                await UnitOfWork.SaveAsync();
            }
            catch (Exception)
            {
                UnitOfWork.RejectChanges();
                throw;
            }
        }        

        public async Task DeleteAsync(IEntity<BaseEntity> entity)
        {
            //DbSet set = BaseContext.Set(entity.GetType());
            var set = GetDbSet(entity.GetType());
            IEntity<BaseEntity> efEntity = (IEntity<BaseEntity>)set.Find(((BaseEntity)entity).GetKeys());
            set.Remove(efEntity as dynamic);
            await UnitOfWork.SaveAsync();
        }


        public async Task<object> LoadAsync(object entity, params string[] includes)
        {
            if (includes == null)
                includes = new string[0];

            if (!(entity is IEntity<BaseEntity>))
            {
                throw new BaseEFRepositoryException("Só é permitido carregar objetos do tipo IEntidade<EntidadeBase>");
            }
            else
            {
                //DbSet set = BaseContext.Set(entity.GetType());
                var set = GetDbSet(entity.GetType());
                foreach (var key in ((BaseEntity)entity).GetKeys())
                {
                    if (key == null ||
                        (typeof(string) == key.GetType() && string.IsNullOrEmpty((string)key)) ||
                        (typeof(Guid) == key.GetType() && (Guid)key == default(Guid)) ||
                        (new Type[] { typeof(int), typeof(byte), typeof(short), typeof(Int64) }.Contains(key.GetType()) && Convert.ToInt64(key) == 0))
                    {
                        return null;
                        //throw new BaseEFRepositoryException("In order to load and entity the Id property must be set to a non-default value.");
                    }
                }

                object newEntity = null;

                if (includes.Count() != 0)
                {
                    Guid id = ((IEntity<BaseEntity>)entity).Id;
                    //Só funciona quando o objeto está salvo no banco de dados, quando está salvo somente no contexto, retorna nulo (não sei porque)
                    var query = set as IQueryable<BaseEntity>;

                    foreach (string include in includes)
                    {
                        query = query.Include(include);
                    }

                    newEntity = await (from e in query
                                       where e.Id == id
                                       select e).FirstOrDefaultAsync();
                }
                else
                    newEntity = await set.FindAsync(((BaseEntity)entity).GetKeys());

                return newEntity;
            }
        }

        public async Task<bool> LoadCopyAsync(object entidade, params string[] includes)
        {
            if (includes == null)
                includes = new string[0];

            var novaEntidade = await LoadAsync(entidade, includes);

            if (novaEntidade != null)
            {
                //entity = ((BaseEntity)NewEntity).ShallowCopy();
                ShallowCopy(novaEntidade, entidade, novaEntidade.GetType());
            }
            else
                return false;
            return true;
        }               

        protected void ShallowCopy(object source, object destination, Type type)
        {
            FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            foreach (FieldInfo fi in fields)
            {
                if ((!fi.IsLiteral) && (!typeof(IRepository).IsAssignableFrom(fi.FieldType)))
                {
                    try
                    {
                        fi.SetValue(destination, fi.GetValue(source));
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            if (type != typeof(Object))
                ShallowCopy(source, destination, type.BaseType);
        }

        [Serializable]
        public class BaseEFRepositoryException : Exception
        {
            public BaseEFRepositoryException() { }
            public BaseEFRepositoryException(string message) : base(message) { }
            public BaseEFRepositoryException(string message, Exception inner) : base(message, inner) { }
            protected BaseEFRepositoryException(
              System.Runtime.Serialization.SerializationInfo info,
              System.Runtime.Serialization.StreamingContext context)
                : base(info, context) { }
        }

        public async Task<IEnumerable<BaseEntity>> LoadAllAsync(Type entityType)
        {
            var set = GetDbSet(entityType);
            var query = set as IQueryable<BaseEntity>;
            return await query.ToListAsync();
        }
    }
}
