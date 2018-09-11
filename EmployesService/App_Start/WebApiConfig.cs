using EmployeeService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApiContrib.Formatting.Jsonp;

namespace EmployesService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuración y servicios de API web
            
            // Rutas de API web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            //Para poder devolver desde la api en formato xml o en json
            //  config.Formatters.Remove(config.Formatters.XmlFormatter);                       
            config.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();


            //Manejo con framework "cors" permite el intercambio de datos entre dos dominios diferentes. Ya que por convencion cuando a una Web Api le solicitamos datos en formato (json) desde otro dominio
            // es el browser el que no nos va permitir hacerlo por seguridad. Con cors podemos solucionar esto

            //config.Filters.Add(new RequireHttpsAttribute());
            //  Aca le definimos los parametros segun queramos, el primer * definimos url que vamos a permitir, en el segundo el contenido que permitiremos y en el tercero que controladores o metodos ( * permite a todos)
            //EnableCorsAttribute cors = new EnableCorsAttribute("*", "*", "*");
            //config.EnableCors(cors);

            // Otra forma de permitir intercambio de datos entre distintos dominios es con jsonp (json whith padding) permite cors ( cross origin resource sharing)
            //todos los jsonp deben volver como parametro de una funcion por lo tanto hay que pasarle el nombre

            var jsonpFormatter = new JsonpMediaTypeFormatter(config.Formatters.JsonFormatter);
            config.Formatters.Insert(0, jsonpFormatter);




        }
    }
}
