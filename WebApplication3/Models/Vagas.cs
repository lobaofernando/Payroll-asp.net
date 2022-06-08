using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class Vagas
    {

        private readonly static string _conn = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AgenciaAuto;
                Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;
                ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Criador { get; set; }
        public string Link { get; set; }
        public int Moeda { get; set; }
        public string Regiao { get; set; }
        public string Pais { get; set; }
        public decimal Valor { get; set; }
        public bool Ativo { get; set; }
        public string NomeCriador { get; set; }
        public string Previa { get; set; }

        public Vagas() { }

        public Vagas(int id, string nome, string descricao, int criador, string link,
            int moeda, string regiao, string pais, decimal valor, bool ativo, string nomecriador, string previa)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
            Criador = criador;
            Link = link;
            Moeda = moeda;
            Regiao = regiao;
            Pais = pais;
            Valor = valor;
            Ativo = ativo;
            NomeCriador = nomecriador;
            Previa = previa;
        }

        public static List<Vagas> GetVagas()
        {

            var listavagas = new List<Vagas>();
            var sql = "SELECT * FROM tb_Vagas";

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
                                while (dr.Read())
                                {
                                    listavagas.Add(new Vagas(
                                        Convert.ToInt16(dr["Id"]),
                                        dr["Nome"].ToString(),
                                        dr["Descricao"].ToString(),
                                        Convert.ToInt16(dr["Criador"]),
                                        dr["Link"].ToString(),
                                        Convert.ToInt16(dr["Moeda"]),
                                        dr["Regiao"].ToString(),
                                        dr["Pais"].ToString(),
                                        Convert.ToDecimal(dr["Valor"]),
                                        Convert.ToBoolean(dr["Ativo"]),
                                        dr["NomeCriador"].ToString(),
                                        dr["Previa"].ToString()));

                                    ;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Falha: " + ex.Message);
                Debug.WriteLine(ex.Message, ex.InnerException);
            }
            return listavagas;
        }

        public static List<Vagas> GetVagas_user(int id_user)
        {
            string[] regioes = { "", "EUROPA", "AMERICA", "ASIA", "AFRICA", "OCEANIA" };
            var listavagas = new List<Vagas>();
            var sql = "SELECT * FROM tb_Vagas WHERE Criador=" + id_user;

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
                                while (dr.Read())
                                {
                                    listavagas.Add(new Vagas(
                                        Convert.ToInt16(dr["Id"]),
                                        dr["Nome"].ToString(),
                                        dr["Descricao"].ToString(),
                                        Convert.ToInt16(dr["Criador"]),
                                        dr["Link"].ToString(),
                                        Convert.ToInt16(dr["Moeda"]),
                                        dr["Regiao"].ToString(),
                                        dr["Pais"].ToString(),
                                        Convert.ToDecimal(dr["Valor"]),
                                        Convert.ToBoolean(dr["Ativo"]),
                                        dr["NomeCriador"].ToString(),
                                        dr["Previa"].ToString()));
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Falha: " + ex.Message);
                Debug.WriteLine(ex.Message, ex.InnerException);
            }
            return listavagas;
        }

        public static List<Vagas> GetVagas_Region(string regiao, string pais=null)
        {
            string[] regioes = { "", "EUROPA", "AMERICA", "ASIA", "AFRICA", "OCEANIA" };
            var listavagas = new List<Vagas>();
            var sql = "SELECT * FROM tb_Vagas WHERE Regiao='" + regiao+"'";

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
                                while (dr.Read())
                                {
                                    var vaga = new Vagas(
                                        Convert.ToInt16(dr["Id"]),
                                        dr["Nome"].ToString(),
                                        dr["Descricao"].ToString(),
                                        Convert.ToInt16(dr["Criador"]),
                                        dr["Link"].ToString(),
                                        Convert.ToInt16(dr["Moeda"]),
                                        dr["Regiao"].ToString(),
                                        dr["Pais"].ToString(),
                                        Convert.ToDecimal(dr["Valor"]),
                                        Convert.ToBoolean(dr["Ativo"]),
                                        dr["NomeCriador"].ToString(),
                                        dr["Previa"].ToString());

                                    if (pais == null)
                                    {
                                        listavagas.Add(vaga);
                                    } else if (vaga.Pais == pais)
                                    {
                                        listavagas.Add(vaga);
                                    }

                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Falha: " + ex.Message);
                Debug.WriteLine(ex.Message, ex.InnerException);
            }
            return listavagas;
        }

        public void Salvar() 
        {
            var sql = "";

            if (Id == 0)
                sql = "INSERT INTO tb_Vagas (nome, descricao, criador, " +
                "link, moeda, regiao, pais, valor," +
                " ativo, nomecriador, previa) VALUES (@nome, @descricao, @criador," +
                " @link, @moeda, @regiao, @pais, @valor," +
                " @ativo, @nomecriador, @previa)";
            else
                sql = "UPDATE tb_Vagas SET nome=@nome, descricao=@descricao, criador=@criador, " +
                "link=@link, moeda=@moeda, regiao=@regiao, pais=@pais, valor=@valor," +
                " ativo=@ativo, nomecriador=@nomecriador, previa=@previa WHERE id=" + Id;

            try
            {
                using(var cn = new SqlConnection(_conn))
                {
                    cn.Open();
                    using (var cmd = new SqlCommand(sql, cn))
                    {
                        cmd.Parameters.AddWithValue("@nome", Nome);
                        cmd.Parameters.AddWithValue("@descricao", Descricao);
                        cmd.Parameters.AddWithValue("@criador", Criador);
                        cmd.Parameters.AddWithValue("@link", Link);
                        cmd.Parameters.AddWithValue("@moeda", Moeda);
                        cmd.Parameters.AddWithValue("@regiao", Regiao);
                        cmd.Parameters.AddWithValue("@pais", Pais);
                        cmd.Parameters.AddWithValue("@valor", Valor);
                        cmd.Parameters.AddWithValue("@ativo", Ativo);
                        cmd.Parameters.AddWithValue("@nomecriador", NomeCriador);
                        cmd.Parameters.AddWithValue("@previa", Previa);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message, ex.InnerException);
            }
        }

        public void GetVaga(int id)
        {
            var sql = "SELECT nome, descricao, criador, " +
                "link, moeda, regiao, pais, valor," +
                " ativo, nomecriador, previa FROM tb_Vagas WHERE id=" + id;

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
                                    Id = id;
                                    Nome = dr["Nome"].ToString();
                                    Descricao = dr["Descricao"].ToString();
                                    Criador = Convert.ToInt16(dr["Criador"]);
                                    Link = dr["Link"].ToString();
                                    Moeda = Convert.ToInt16(dr["Moeda"]);
                                    Regiao = dr["Regiao"].ToString();
                                    Pais = dr["Pais"].ToString();
                                    Valor = Convert.ToDecimal(dr["Valor"]);
                                    Ativo = Convert.ToBoolean(dr["Ativo"]);
                                    NomeCriador = dr["NomeCriador"].ToString();
                                    Previa = dr["Previa"].ToString();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Nome = "Falha: " + ex.Message;
                Console.WriteLine("Falha: " + ex.Message);
            }
        }

        public void Excluir()
        {
            var sql = "DELETE FROM tb_Vagas WHERE id=" + this.Id;

            try
            {
                using (var cn = new SqlConnection(_conn))
                {
                    cn.Open();
                    using (var cmd = new SqlCommand(sql, cn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Falha: " + ex.Message);
            }
        }
    }
}