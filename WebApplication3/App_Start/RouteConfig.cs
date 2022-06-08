using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApplication3
{
    public class RouteConfig
    {   
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "VagasSalvar",
                "Vagas/Salvar",
                new { controller = "Vagas", action = "Salvar" }
                );

            routes.MapRoute(
                "VagasEditar",
                "Vagas/Editar/:id",
                new { controller = "Vagas", action = "Editar", id = 0 }
                );

            routes.MapRoute(
                "HomeFiltrar",
                "Home/Filtrar/:regiao",
                new { controller = "Home", action = "Filtrar", regiao = "0"}
                );

            routes.MapRoute(
                "VagasExcluir",
                "Vagas/Excluir/:id",
                new { controller = "Vagas", action = "Excluir", id = 0 }
                );

            routes.MapRoute(
                "VagasVisualizar",
                "Vagas/Visualizar/:id",
                new { controller = "Vagas", action = "Visualizar", id = 0 }
                );

            routes.MapRoute(
                "VagasAdicionar",
                "Vagas/Adicionar",
                new { controller = "Vagas", action = "Adicionar"}
                );

            routes.MapRoute(
                "Logout",
                "Home/Logout",
                new { controller = "Home", action = "Logout" }
                );

            routes.MapRoute(
                "Registrar",
                "Login/Registrar",
                new { controller = "Login", action = "Registrar" }
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
