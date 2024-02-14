using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace lojaonline
{
    public partial class gestao_servicos : System.Web.UI.Page
    {
        int id_utilizador = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            
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

        protected void btn_inserir_servico_Click(object sender, EventArgs e)
        {
            Response.Redirect("registar_servico.aspx");
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView dr = (DataRowView)e.Item.DataItem;
                ((Label)e.Item.FindControl("lbl_cod")).Text = dr["id_produto"].ToString();
                ((TextBox)e.Item.FindControl("tb_nome_servico")).Text = dr["nome_produto"].ToString();
                ((TextBox)e.Item.FindControl("tb_valor")).Text = dr["valor"].ToString();
                ((Image)e.Item.FindControl("img_servico")).ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String((byte[])dr["foto"]);
                ((TextBox)e.Item.FindControl("tb_descricao")).Text = dr["descricao"].ToString();

                ((ImageButton)e.Item.FindControl("btn_grava")).CommandArgument = dr["id_produto"].ToString();
                ((ImageButton)e.Item.FindControl("btn_delet")).CommandArgument = dr["id_produto"].ToString();
            }
        }

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("btn_grava"))
            {

                SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["lojaonlineConnectionString"].ConnectionString);

                string query = "update produtos set ";

                query += "nome_produto='" + ((TextBox)e.Item.FindControl("tb_nome_servico")).Text + "',";
                query += "valor=parse('" + ((TextBox)e.Item.FindControl("tb_valor")).Text + "' as numeric(6,2) using 'PT-pt') ;";

                query += "descricao='" + ((TextBox)e.Item.FindControl("tb_descricao")).Text + "' ";

                query += "where id_produto=" + ((ImageButton)e.Item.FindControl("btn_grava")).CommandArgument;

                myCon.Open();
                SqlCommand myCommand = new SqlCommand(query, myCon);
                myCommand.ExecuteNonQuery();
                myCon.Close();

            }

            if (e.CommandName.Equals("btn_delet"))
            {

                SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["lojaonlineConnectionString"].ConnectionString);

                string query = "delete from produtos ";
                query += "where id_produto=" + ((ImageButton)e.Item.FindControl("btn_delet")).CommandArgument;

                myCon.Open();
                SqlCommand myCommand = new SqlCommand(query, myCon);
                myCommand.ExecuteNonQuery();
                
                myCon.Close();

                Response.Redirect("gestao_servicos.aspx");
            }
        }
    }
}