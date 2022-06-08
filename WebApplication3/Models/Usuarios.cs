using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class Usuarios
    {
        private readonly static string _conn = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AgenciaAuto;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public int Tipo { get; set; }

        public bool Login()
        {
            var result = false;
            var sql = "SELECT Id, Email, Nome, Senha, Tipo FROM Usuarios WHERE Email = '" + this.Email + "'";

            try
            {
                using (var cn = new SqlConnection(_conn))
                {
                    cn.Open();
                    using (var cmd = new SqlCommand(sql, cn))
                    {
                        using (var dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                if (dr.Read())
                                {
                                    if(this.Senha == dr["senha"].ToString())
                                        {
                                        this.Tipo = Convert.ToInt32(dr["tipo"]);
                                        this.Id = Convert.ToInt32(dr["id"]);
                                        this.Nome = dr["nome"].ToString();
                                        this.Email = dr["email"].ToString();
                                        result = true;
                                    }
                                    
                                }
                            }
                        }
                    }
                }
            } catch (Exception ex)
            {
                Console.WriteLine("Falha: " + ex.Message);
            }
            return result;
        }
        public void Salvar()
        {
            var sql = "INSERT INTO Usuarios (nome, email, senha, " +
                "tipo) VALUES (@nome, @email, @senha, @tipo)";

            try
            {
                using (var cn = new SqlConnection(_conn))
                {
                    cn.Open();
                    using (var cmd = new SqlCommand(sql, cn))
                    {
                        cmd.Parameters.AddWithValue("@nome", Nome);
                        cmd.Parameters.AddWithValue("@email", Email);
                        cmd.Parameters.AddWithValue("@senha", Senha);
                        cmd.Parameters.AddWithValue("@tipo", Tipo);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, ex.InnerException);
            }
        }
        public bool Check()
        {
            var result = false;
            var sql = "SELECT Id, Email FROM Usuarios WHERE Email = '" + this.Email + "'";

            try
            {
                using (var cn = new SqlConnection(_conn))
                {
                    cn.Open();
                    using (var cmd = new SqlCommand(sql, cn))
                    {
                        using (var dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                if (dr.Read())
                                {
                                    result = true;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Falha: " + ex.Message);
            }
            return result;
        }
    }
}