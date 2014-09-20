using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;
using System.Net.Http;
using Newtonsoft.Json;
using System.Web.Http;
using System.ComponentModel.DataAnnotations;

namespace AeC.Hospitale.WebApi
{
    public class DynamicModelBinder : IModelBinder
    {
        private static string GetRouteData(HttpRequestMessage request, string token)
        {
            var data = request.GetRouteData();
            if (data.Route.DataTokens == null)
            {
                return null;
            }
            else
            {
                object areaName;
                return data.Values.TryGetValue(token, out areaName) ? areaName.ToString() : null;
            }
        }

        public static Type GetModelType(string module, string entity)
        {
            var assembly = (from e in AppDomain.CurrentDomain.GetAssemblies()
                             where e.FullName.StartsWith("EscolaShaolin.Dominio")
                             select e).FirstOrDefault();

            return assembly.GetTypes().Where(t =>
                        !t.IsAbstract &&
                        (t.FullName.StartsWith(string.Format("EscolaShaolin.Dominio.{0}", module), StringComparison.OrdinalIgnoreCase)) &&
                        (t.Name.Equals(entity, StringComparison.OrdinalIgnoreCase)))
                .ToList().FirstOrDefault();
        }

        public bool BindModel(System.Web.Http.Controllers.HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            string formContent = actionContext.Request.Content.ReadAsStringAsync().Result;


            var type = GetModelType(GetRouteData(actionContext.Request, "module"), GetRouteData(actionContext.Request, "entity"));

            if (actionContext.Request.Content.Headers.ContentType.MediaType != "application/json")
            {
                throw new Exception("This method only accepts json");
            }

            bindingContext.Model = JsonConvert.DeserializeObject(formContent, type, GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings);
            if (bindingContext.Model != null)
            {
                IValidatableObject validableObject = bindingContext.Model as IValidatableObject;
                if (validableObject != null)
                {
                    bool validateAllProperties = true;

                    var validations = new List<ValidationResult>();

                    bool isValid = Validator.TryValidateObject(
                        validableObject,
                        new ValidationContext(validableObject, null, null),
                        validations,
                        validateAllProperties);

                    if (!isValid)
                    {
                        foreach (var validation in validations)
                        {
                            bindingContext.ModelState.AddModelError(validation.MemberNames.FirstOrDefault(), validation.ErrorMessage);
                        }
                    }
                }
                return true;
            }
            else return false;
        }
    }
}