using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace lojaonline
{
    public partial class registar_desconto : System.Web.UI.Page
    {
        int id_utilizador = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SqlConnection myConInicio = new SqlConnection(ConfigurationManager.ConnectionStrings["lojaonlineConnectionString"].ConnectionString);

                string queryInicio = "delete from carrinho ";
                queryInicio += "where id_utilizador is null";

                myConInicio.Open();
                SqlCommand myCommandInicio = new SqlCommand(queryInicio, myConInicio);
                myCommandInicio.ExecuteNonQuery();
                myConInicio.Close();

            }
            if (Session["utilizador"] != null)
            {
                lbl_utilizador.Text = Session["utilizador"].ToString();
                Session["perfil"] = Session["perfil"];

                SqlConnection myCon2 = new SqlConnection(ConfigurationManager.ConnectionStrings["lojaonlineConnectionString"].ConnectionString);

                string query2 = "Select id_utilizador from utilizadores where email = '" + lbl_utilizador.Text + "'";
                SqlCommand myCommand7 = new SqlCommand(query2, myCon2);
                myCon2.Open();
                var usuario = myCommand7.ExecuteReader();
                if (usuario.Read())
                {
                    id_utilizador = int.Parse(usuario.GetInt32(0).ToString());
                }
                myCon2.Close();

                if (Session["perfil"].ToString() == "1")
                {
                    Panel1.Visible = true;
                }
                else
                {
                    Panel1.Visible = false;
                }

                SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["lojaonlineConnectionString"].ConnectionString);

                string query = "select count(c.id_utilizador) from carrinho as c inner join utilizadores as u on u.id_utilizador = c.id_utilizador where c.id_utilizador = " + id_utilizador;
                SqlCommand myCommand6 = new SqlCommand(query, myCon);
                myCon.Open();
                var itens = myCommand6.ExecuteReader();
                if (itens.Read())
                {
                    lbl_itens_carrinho.Text = itens.GetInt32(0).ToString();
                }

                myCon.Close();

                string query3 = "select count(c.id_utilizador) from carrinho as c inner join utilizadores as u on u.id_utilizador = c.id_utilizador where c.id_utilizador = " + id_utilizador;
                SqlCommand myCommand9 = new SqlCommand(query3, myCon);
                myCon.Open();
                var itens2 = myCommand9.ExecuteReader();
                if (itens2.Read())
                {
                    lbl_itens_carrinho.Text = itens2.GetInt32(0).ToString();
                }

                myCon.Close();

            }
            if (id_utilizador == 0)
            {
                SqlConnection myCon5 = new SqlConnection(ConfigurationManager.ConnectionStrings["lojaonlineConnectionString"].ConnectionString);
                string query4 = "select count(c.id_utilizador) from carrinho as c inner join utilizadores as u on u.id_utilizador = c.id_utilizador where c.id_utilizador = Null";
                SqlCommand myCommand10 = new SqlCommand(query4, myCon5);
                myCon5.Open();
                var itens2 = myCommand10.ExecuteReader();
                if (itens2.Read())
                {
                    lbl_itens_carrinho.Text = itens2.GetInt32(0).ToString();
                }
                myCon5.Close();
                Response.Redirect("index.aspx");
            }
        }

        protected void btn_gerar_cupom_Click(object sender, EventArgs e)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var CharSar = new char[10];
            Random aleatorio = new Random();

            for (int i = 0; i < CharSar.Length; i++)
            {
                CharSar[i] = chars[aleatorio.Next(chars.Length)];
            }

            var cupom = new String(CharSar);

            tb_cod_desconto.Text = cupom.ToString();
        }

        protected void btn_registar_desconto_Click(object sender, EventArgs e)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["lojaonlineConnectionString"].ConnectionString);
            SqlCommand myCommand = new SqlCommand();
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.CommandText = "inserir_desconto";

            myCommand.Connection = myConn;
            //Passar os parametros para a SP
            myCommand.Parameters.AddWithValue("@chave", tb_cod_desconto.Text);
            myCommand.Parameters.AddWithValue("@dt_inicio", tb_dt_inicio.Text);
            myCommand.Parameters.AddWithValue("@dt_fim", tb_dt_fim.Text);
            myCommand.Parameters.AddWithValue("@desconto", tb_valor_desconto.Text);



            myConn.Open();
            myCommand.ExecuteNonQuery();
            myConn.Close();

            Response.Write(lbl_mensagem.Text = "Serviço Registado com Sucesso !!!");


            Response.Redirect("registar_desconto.aspx");
        }
    }
}