using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Calcular()
        {
            return View();
        }

        public ActionResult Vagas()
        {
            ViewBag.Title = "Vagas ativas";

            if (Global.Logado)
            {
                var lista = Models.Vagas.GetVagas();
                ViewBag.Lista = lista;

                return View();
            }
            else
            {
                Response.Redirect("/Login/Index");
                return null;
            }
        }
        public ActionResult Filtrar(int id)
        {
            Debug.WriteLine(id);
            string[] regioes = { "", "EUROPA", "AMERICA", "ASIA", "AFRICA", "OCEANIA" };
            if (id == 0)
            {
                Response.Write("<script>alert('primeiro if!');window.location.href = '/Home/Vagas';</script>");
                return null;
            }
            else if (Global.Logado)
            {
                ViewBag.Regioes = regioes;
                ViewBag.Title = "Vagas na " + regioes[id];
                var lista = Models.Vagas.GetVagas_Region(regioes[id]);
                ViewBag.Lista = lista;
                return View();
            }
            else
            {
                Response.Redirect("/Login/Index");
                return null;
            }
        }
        public ActionResult Vagas_user()
        {
            ViewBag.Title = "Minhas Vagas";

            if (Global.Logado && Global.usuario_logado.Tipo == 2)
            {
                var lista = Models.Vagas.GetVagas_user(Global.usuario_logado.Id);
                ViewBag.Lista = lista;

                return View();
            }
            else
            {
                Response.Redirect("/home/vagas");
                return null;
            }
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Erro()
        {
            return View();
        }

        public void Logout()
        {
            Global.Logout();
            Response.Redirect("/");
        }
    }
}