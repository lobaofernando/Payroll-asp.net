using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class VagasController : Controller
    {
        // GET: Vagas
        public ActionResult Adicionar()
        {
            if (Global.Logado && Global.usuario_logado.Tipo == 1)
            {
                Response.Redirect("/Home/Vagas");
                return null;
            }
            if (Global.Logado)
            {
                ViewBag.Title = "Vagas";
                ViewBag.Message = "Adicionar Vagas";
                return View();
            }
            else
            {
                Response.Redirect("/Login/Index");
                return null;
            }
        }

        public ActionResult Editar(int id)
        {
            if (Global.Logado && Global.usuario_logado.Tipo == 1)
            {
                Response.Redirect("/Home/Vagas");
                return null;
            }
            if (Global.Logado && Global.usuario_logado.Tipo == 2)
            {
                ViewBag.Title = "Vagas";
                ViewBag.Message = "Editar Vagas " + id;

                var vaga = new Vagas();
                vaga.GetVaga(id);
                if (vaga.Criador == Global.usuario_logado.Id)
                {
                    ViewBag.Vaga = vaga;
                    return View();
                }
                else
                {
                    Response.Redirect("/Home/Vagas_user");
                    return null;
                }

            }
            else
            {
                Response.Redirect("/Login/Index");
                return null;
            }

        }
        public ActionResult Visualizar(int id)
        {
            if (Global.Logado)
            {
                ViewBag.Title = "Vagas";

                var vaga = new Vagas();
                vaga.GetVaga(id);
                ViewBag.Vaga = vaga;
                ViewBag.Valor = (float)vaga.Valor;

                return View();
            }
            else
            {
                Response.Redirect("/Login/Index");
                return null;
            }

        }
        public ActionResult Excluir(int id)
        {
            if (Global.Logado && Global.usuario_logado.Tipo == 1)
            {
                Response.Redirect("/Home/Vagas");
                return null;
            }
            if (Global.Logado && Global.usuario_logado.Tipo == 2)
            {
                ViewBag.Title = "Vagas";
                ViewBag.Message = "Excluir Vagas " + id;

                var vaga = new Vagas();
                vaga.GetVaga(id);

                if (vaga.Criador == Global.usuario_logado.Id)
                {
                    ViewBag.Vaga = vaga;
                    return View();
                }
                else
                {
                    Response.Redirect("/Home/Vagas_user");
                    return null;
                }
            }
            else
            {
                Response.Redirect("/Login/Index");
                return null;
            }

        }
        public static string Regiao(string id)
        {
            string[] regioes = { "", "EUROPA", "AMERICA", "ASIA", "AFRICA", "OCEANIA" };
            return regioes[Convert.ToInt16(id)];
        }

        [HttpPost]
        public void Salvar()
        {
            var nome = Request["nome"].Trim();
            var valor = Request["valor"];
            var previa = Request["previa"].Trim();

            if (valor == "" || valor == null)
            {
                Response.Write("<script>alert('Preencha o valor!');window.location.href = '/Vagas/Adicionar';</script>");
                return;
            }

            if (previa == "" || previa == null)
            {
                Response.Write("<script>alert('Preencha o a Prévia!');window.location.href = '/Vagas/Adicionar';</script>");
                return;
            }

            var vaga = new Vagas();
            vaga.Id = Convert.ToInt16("0" + Request["id"]);
            vaga.Nome = nome;
            vaga.Descricao = Request["descricao"].Trim();
            vaga.Criador = Global.usuario_logado.Id;
            vaga.Link = Request["link"].Trim();
            vaga.Moeda = Convert.ToInt16(Request["moeda"]);
            string region = Request["regiao"];

            if (vaga.Nome == "" || vaga.Nome == null)
            {
                Response.Write("<script>alert('Preencha o Nome!');window.location.href = '/Vagas/Adicionar';</script>");
                return;
            }
            if (region.Length > 1 || region == "")
            {
                vaga.Regiao = Request["regiao"];
            }
            else
            {
                vaga.Regiao = Regiao(Request["regiao"]);
            }
            vaga.Pais = "";

            vaga.Valor = Convert.ToDecimal(valor);
            vaga.Ativo = true;
            vaga.NomeCriador = Global.usuario_logado.Nome;
            vaga.Previa = previa;

            vaga.Salvar();
            Response.Redirect("/Home/Vagas_user");
        }

        [HttpPost]
        public void Excluir()
        {
            var vaga = new Vagas();
            vaga.Id = Convert.ToInt32("0" + Request["id"]);

            vaga.Excluir();

            Response.Redirect("/Home/Vagas");
        }
    }
}