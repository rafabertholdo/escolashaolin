﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace EscolaShaolin.Katana
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            ConfigureJsonSettings(config.Formatters.JsonFormatter.SerializerSettings);                        

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        public static void ConfigureJsonSettings(JsonSerializerSettings settings)
        {
            settings.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.IsoDateFormat;
            settings.ObjectCreationHandling = Newtonsoft.Json.ObjectCreationHandling.Replace;
            settings.Converters.Add(
                new StringEnumConverter { CamelCaseText = false });
            settings.Converters.Add(
                new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });
            settings.Error += (sender, args) =>
            {                
                args.ErrorContext.Handled = true;
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    System.Diagnostics.Debugger.Break();
                }
            };
            settings.TypeNameHandling = TypeNameHandling.All;
            settings.TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Full;
#if DEBUG
            settings.Formatting = Formatting.Indented;
#endif
        }
    }
}