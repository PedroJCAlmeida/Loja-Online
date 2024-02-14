using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace lojaonline
{
    public partial class registar_servico : System.Web.UI.Page
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

                if (Session["perfil"].ToString() == "1")
                {
                    Panel1.Visible = true;
                }
                else
                {
                    Panel1.Visible = false;
                    
                }
                string query3 = "select count(c.id_utilizador) from carrinho as c inner join utilizadores as u on u.id_utilizador = c.id_utilizador where c.id_utilizador ="+id_utilizador;
                SqlCommand myCommand9 = new SqlCommand(query3, myCon2);
                myCon2.Open();
                var itens2 = myCommand9.ExecuteReader();
                if (itens2.Read())
                {
                    lbl_itens_carrinho.Text = itens2.GetInt32(0).ToString();
                }

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

        protected void btn_registar_servico_Click(object sender, EventArgs e)
        {
            //Apanhar o ficheiro que foi selecionado
            Stream imgStream = FileUpload1.PostedFile.InputStream;

            //Identificar o tamanho do ficheiro
            int tamanhoFich = FileUpload1.PostedFile.ContentLength;

            //Identificar o contentType (tipo de ficheiro)
            string contentType = FileUpload1.PostedFile.ContentType;

            //Arrey para armazernar dados binarios
            byte[] imgBinaryData = new byte[tamanhoFich];

            //Preencher o arrey 
            imgStream.Read(imgBinaryData, 0, tamanhoFich);

            //Conexão a base de dados
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["lojaonlineConnectionString"].ConnectionString);

            // Seleccionar a SP
            SqlCommand myCommand = new SqlCommand();
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.CommandText = "inserir_servico";

            myCommand.Connection = myConn;
            //Passar os parametros para a SP
            myCommand.Parameters.AddWithValue("@nome_produto", tb_nome_servico.Text);
            myCommand.Parameters.AddWithValue("@valor", Convert.ToDouble(tb_valor.Text));
            myCommand.Parameters.AddWithValue("@ct", contentType);
            myCommand.Parameters.AddWithValue("@foto", imgBinaryData);
            myCommand.Parameters.AddWithValue("@descricao", tb_descricao.Text);


            myConn.Open();
            myCommand.ExecuteNonQuery();
            myConn.Close();

            Response.Write(lbl_mensagem.Text = "Serviço Registado com Sucesso !!!");
            

            Response.Redirect("registar_servico.aspx");


        }

        
    }
}