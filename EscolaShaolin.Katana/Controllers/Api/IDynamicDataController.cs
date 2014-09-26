using System;
namespace EscolaShaolin.Katana.Controllers.Api
{
    interface IDynamicDataController
    {
        global::System.Threading.Tasks.Task<global::System.Web.Http.IHttpActionResult> Delete(string module, string entity, Guid id);
        global::System.Threading.Tasks.Task<global::System.Collections.Generic.IEnumerable<global::EscolaShaolin.Framework.Persistence.BaseEntity>> Get(string module, string entity);
        global::System.Threading.Tasks.Task<global::System.Web.Http.IHttpActionResult> Get(string module, string entity, Guid id);
        Type GetContextType(string module);
        global::System.Threading.Tasks.Task<global::System.Web.Http.IHttpActionResult> Post(global::EscolaShaolin.Framework.Persistence.BaseEntity content, string module, string entity);
        global::System.Threading.Tasks.Task<global::System.Web.Http.IHttpActionResult> Put(global::EscolaShaolin.Framework.Persistence.BaseEntity content, string module, string entity, string id);
    }
}
