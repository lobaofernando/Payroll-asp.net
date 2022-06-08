using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public static class Global
    {
        public static Usuarios usuario_logado { get; set; }
        public static bool Logado { get; set; }
        public static string Erros { get; set; }
        public static List<string> Regioes { get; set; }
        public static void Logout()
        {
            Logado = false;
            usuario_logado = new Usuarios();
        }
    }
}