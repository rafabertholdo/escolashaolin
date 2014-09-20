using EscolaShaolin.Framework.Persistence;
using EscolaShaolin.Framework.Persistence.EntityFramework;
using EscolaShaolin.Infraestrutura;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.ModelBinding;
using System.Web.Routing;

namespace AeC.Hospitale.WebApi.Controllers
{
    public class DynamicDataController : ApiController
    {
        public DynamicDataController()
        {

        }        

        public Type GetContextType(string module)
        {
            return typeof(ContextoEscolaShaolin);
        }

        protected DynamicEFRepository GetRepository(string module, Type entityType)
        {
            if (entityType != null)
            {
                var contextType = GetContextType(module);
                if (contextType != null)
                {
                    //var context = ServiceLocator.Current.GetInstance(contextType) as IUnitOfWork;
                    var context = new ContextoEscolaShaolin();
                    return new DynamicEFRepository(context);
                }
            }
            return null;
        }

        [Route("api/DynamicData/{module}/{entity}")]
        public async Task<IEnumerable<BaseEntity>> Get(string module, string entity)
        {
            var entityType = DynamicModelBinder.GetModelType(module, entity);
            var repository = GetRepository(module, entityType);
            if (repository != null)
            {
                return await repository.LoadAllAsync(entityType);
            }
            else return null;
        }

        [ResponseType(typeof(BaseEntity))]
        [Route("api/DynamicData/{module}/{entity}/{id}")]
        public async Task<IHttpActionResult> Get(string module, string entity, Guid id)
        {
            var entityType = DynamicModelBinder.GetModelType(module, entity);
            var repository = GetRepository(module, entityType);
            BaseEntity baseEntity = null;
            object result = null;
            if (repository != null)
            {
                baseEntity = (BaseEntity)Activator.CreateInstance(entityType);
                baseEntity.Id = id;
                result = await repository.LoadAsync(baseEntity);
            }

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // PUT: api/produtcts/5
        [ResponseType(typeof(BaseEntity))]
        [Route("api/DynamicData/{module}/{entity}")]
        public async Task<IHttpActionResult> Put([ModelBinder(typeof(DynamicModelBinder))]BaseEntity content, string module, string entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entityType = DynamicModelBinder.GetModelType(module, entity);
            var repository = GetRepository(module, entityType);

            if (repository != null)
            {
                try
                {
                    await repository.SaveAsync(content, false);
                }
                catch (DbUpdateException)
                {
                    var searchEntity = (BaseEntity)Activator.CreateInstance(entityType);
                    searchEntity.Id = content.Id;
                    if (repository.LoadCopyAsync(searchEntity).Result)
                    {
                        return Conflict();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return CreatedAtRoute("DefaultApi", new { id = content.Id }, content);
        }

        // POST: api/produtcts
        [ResponseType(typeof(BaseEntity))]
        [Route("api/DynamicData/{module}/{entity}")]
        public async Task<IHttpActionResult> Post([ModelBinder(typeof(DynamicModelBinder))]BaseEntity content, string module, string entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entityType = DynamicModelBinder.GetModelType(module, entity);
            var repository = GetRepository(module, entityType);

            if (repository != null)
            {
                try
                {
                    await repository.SaveAsync(content, true);
                }
                catch (DbUpdateException ex)
                {
                    var searchEntity = (BaseEntity)Activator.CreateInstance(entityType);
                    searchEntity.Id = content.Id;
                    if (repository.LoadCopyAsync(searchEntity).Result)
                    {
                        return Conflict();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return CreatedAtRoute("DefaultApi", new { id = content.Id }, content);
        }

        // DELETE: api/produtcts/5
        [ResponseType(typeof(BaseEntity))]
        [Route("api/DynamicData/{module}/{entity}/{id}")]
        public async Task<IHttpActionResult> Delete(string module, string entity, Guid id)
        {

            var entityType = DynamicModelBinder.GetModelType(module, entity);
            var repository = GetRepository(module, entityType);
            BaseEntity baseEntity = null;
            object result = null;
            if (repository != null)
            {
                baseEntity = (BaseEntity)Activator.CreateInstance(entityType);
                baseEntity.Id = id;
                result = await repository.LoadAsync(baseEntity);

                if (result == null)
                {
                    await repository.DeleteAsync((BaseEntity)result);
                }
            }

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        //protected override void Dispose(bool disposing)
        //{
        //    //if (disposing)
        //    //{
        //    //    db.Dispose();
        //    //}
        //    //base.Dispose(disposing);
        //    throw new NotImplementedException();
        //}

        private bool produtctExists(Guid id)
        {
            //return db.produtcts.Count(e => e.Id == id) > 0;
            throw new NotImplementedException();
        }

    }
}