﻿using EscolaShaolin.Dominio.Academia.Entidade;
using EscolaShaolin.Framework.Persistence;
using EscolaShaolin.Framework.Persistence.EntityFramework;
using EscolaShaolin.Infraestrutura;
using EscolaShaolin.Katana;
using EscolaShaolin.Framework.Util;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.ModelBinding;
using System.Web.Routing;

namespace EscolaShaolin.Katana.Controllers.Api
{
    public class DynamicDataController : ApiController, IDynamicDataController
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
            
            //Mock
            //return Task.Run(() =>
            //{
            //    return new[] {
            //        new Aluno {Id = Guid.NewGuid(),
            //        Nome = "rafael"},                    
            //    };
            //}).Result;
        }

        private object AddEnumSources(object result)
        {
            var enums = result.GetType().GetProperties().Where(e => e.PropertyType.IsEnum || (e.PropertyType.IsGenericType && e.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) && typeof(Enum).IsAssignableFrom(e.PropertyType.GetGenericArguments()[0])));

            if (enums.Any())
            {
                var xpando = new ExpandoObject() as IDictionary<string, Object>;

                foreach (var type in result.GetType().GetProperties())
                    xpando.Add(type.Name, type.GetValue(result));

                foreach (var enumProp in enums)
                {
                    var dict = new List<KeyValuePair<string, string>>();
                    foreach (Enum enumValue in Enum.GetValues(enumProp.PropertyType.IsGenericType ? enumProp.PropertyType.GetGenericArguments()[0] : enumProp.PropertyType))
                    {
                        dict.Add(new KeyValuePair<string, string>(enumValue.ToString(), enumValue.GetDescription()));
                    }
                    xpando.Add(new KeyValuePair<string, object>(enumProp.Name + "Source", dict));
                }
                return (dynamic)xpando;
            }
            else
                return result;
        }

        [ResponseType(typeof(BaseEntity))]
        [Route("api/DynamicData/{module}/{entity}/{id}")]
        public async Task<IHttpActionResult> Get(string module, string entity, Guid id)
        {            
            var entityType = DynamicModelBinder.GetModelType(module, entity);
            if (id == Guid.Empty)
            {
                return Ok(AddEnumSources(Activator.CreateInstance(entityType)));
            }

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

            return Ok(AddEnumSources(result));

            //Mock
            //return Ok(new Aluno
            //{
            //    Id = id,
            //    Nome = "rafael"
            //});
        }        

        // PUT: api/produtcts/5
        [ResponseType(typeof(BaseEntity))]
        [Route("api/DynamicData/{module}/{entity}/{id}")]
        public async Task<IHttpActionResult> Put([ModelBinder(typeof(DynamicModelBinder))]BaseEntity content, string module, string entity, string id)
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
                    await repository.SaveCopyAsync(content);
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
            return CreatedAtRoute("DefaultApi", new { controller = "DynamicData", id = content.Id }, content);
            
            //Mock
            //return StatusCode(HttpStatusCode.NoContent);
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
            //return CreatedAtRoute("DefaultApi", new { controller = "DynamicData", id = content.Id }, content);
            return Created(string.Format("api/DynamicData/{0}/{1}/{2}", module, entity, content.Id), content);
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

                if (result != null)
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