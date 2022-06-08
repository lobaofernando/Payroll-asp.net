using System;
using System.Web.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Register()
        {
            if (Global.Logado)
            {
                Response.Redirect("/Home/Vagas");
                return null;
            }
            else
            {
                ViewBag.Title = "Registrar Usuário";
                ViewBag.Message = "Preencha os dados abaixo";
                return View();
            }
        }
        [HttpPost]
        public void ChecarLogin()
        {
            var usuario = new Usuarios();
            usuario.Email = Request["Email"];
            usuario.Senha = Request["PassWord"];

            if (usuario.Login())
            {
                Session["Autorizado"] = "OK";
                Global.Logado = true;
                Session["id_user"] = usuario.Id;
                if (usuario.Tipo == 2)
                {
                    Session["CPNJ"] = "empresa";
                }
                Global.usuario_logado = usuario;
                Response.Redirect("/Home/Index");
            }
            else
            {
                Response.Write("<script>alert('Login ou Senha incorretos!');window.location.href = '/Login/Index';</script>");
            }

        }

        [HttpPost]
        public void Registrar()
        {
            Global.Erros = "";
            var user = new Usuarios();
            user.Nome = Request["nome"].Trim();
            user.Email = Request["email"].Trim();
            user.Tipo = Convert.ToInt16(Request["tipo"]);
            user.Senha = Request["senha"].Trim();

            if (user.Nome == "" || user.Nome == null ||
                user.Email == "" || user.Email == null ||
                user.Senha == "" || user.Senha == null)
            {
                Response.Write("<script>alert('Preencha todos os valores');window.location.href = '/Login/Register';</script>");
                return;
            }

            if (user.Check())
            {
                Response.Write("<script>alert('E-mail há Cadastrado!');window.location.href = '/Login/Register';</script>");
                return;
            }
            else
            {
                user.Salvar();
                Response.Redirect("/Login/Index");
            }
        }
    }
}