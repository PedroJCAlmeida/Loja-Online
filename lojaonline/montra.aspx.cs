using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Image = System.Web.UI.WebControls.Image;

namespace lojaonline
{
    public partial class montra : System.Web.UI.Page
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

                string query2 = "Select id_utilizador from utilizadores where email = '"+lbl_utilizador.Text+"'";
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
                string query4 = "select count(id_produto) from carrinho where id_utilizador is null";
                SqlCommand myCommand10 = new SqlCommand(query4, myCon5);
                myCon5.Open();
                var itens2 = myCommand10.ExecuteReader();
                if (itens2.Read())
                {
                    lbl_itens_carrinho.Text = itens2.GetInt32(0).ToString();
                }
                myCon5.Close();
            }
                if (!IsPostBack)  
            {
                
                Repeater1.Visible = true;
                Repeater2.Visible = false;
                Repeater3.Visible = false;
                Repeater4.Visible = false;
                Repeater5.Visible = false;
                
            }
            if (ddl_ordem.Text == "------------")
            {
                    
                    Repeater1.Visible = true;
                    Repeater2.Visible = false;
                    Repeater3.Visible = false;
                    Repeater4.Visible = false;
                    Repeater5.Visible = false;
                

            }
            if (ddl_ordem.Text == "Serviço A-Z")
            {
                Repeater1.Visible = false;
                Repeater2.Visible = true;
                Repeater3.Visible = false;
                Repeater4.Visible = false;
                Repeater5.Visible = false;
                
            }
            if (ddl_ordem.Text == "Serviço Z-A")
            {
                Repeater1.Visible = false;
                Repeater2.Visible = false;
                Repeater3.Visible = true;
                Repeater4.Visible = false;
                Repeater5.Visible = false;
                
            }
            if (ddl_ordem.Text == "Preço Menor - Maior")
            {
                Repeater1.Visible = false;
                Repeater2.Visible = false;
                Repeater3.Visible = false;
                Repeater4.Visible = true;
                Repeater5.Visible = false;
                
            }
            if (ddl_ordem.Text == "Preço Maior - Menor")
            {
                Repeater1.Visible = false;
                Repeater2.Visible = false;
                Repeater3.Visible = false;
                Repeater4.Visible = false;
                Repeater5.Visible = true;
                
            }
            
        }

        protected void Repeater1_ItemDataBound1(object sender, RepeaterItemEventArgs e)
        {
            
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView dr = (DataRowView)e.Item.DataItem;
                ((Label)e.Item.FindControl("lbl_cod")).Text = dr["id_produto"].ToString();
                ((Label)e.Item.FindControl("lbl_nome_servico")).Text = dr["nome_produto"].ToString();
                ((Label)e.Item.FindControl("lbl_valor")).Text = dr["valor"].ToString();
                ((Image)e.Item.FindControl("img_servico")).ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String((byte[])dr["foto"]);
                //((TextBox)e.Item.FindControl("tb_descricao")).Text = dr["descricao"].ToString();
                ((Button)e.Item.FindControl("btn_add_carrinho")).CommandArgument = dr["id_produto"].ToString();
                ((Button)e.Item.FindControl("btn_comprar")).CommandArgument = dr["id_produto"].ToString();
               
            }
        }

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            
            if (e.CommandName.Equals("btn_add_carrinho"))
            {

                SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["lojaonlineConnectionString"].ConnectionString);

                // Seleccionar a SP
                SqlCommand myCommand = new SqlCommand();
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "verificar_carrinho";

                myCommand.Connection = myConn;
                //Passar os parametros para a SP
                myCommand.Parameters.AddWithValue("@id_produto", ((Label)e.Item.FindControl("lbl_cod")).Text);
                myCommand.Parameters.AddWithValue("@id_utilizador", id_utilizador);

                SqlParameter valor = new SqlParameter();
                valor.ParameterName = "@retorno";
                valor.Direction = ParameterDirection.Output;
                valor.SqlDbType = SqlDbType.Int;

                myCommand.Parameters.Add(valor);

                myConn.Open();
                myCommand.ExecuteNonQuery();
                int respostaSP = Convert.ToInt32(myCommand.Parameters["@retorno"].Value);
                myConn.Close();

                if (respostaSP == 1)
                {
                    if (id_utilizador == 0)
                    {
                        string quantidade1 = "1";
                        SqlConnection myCon2 = new SqlConnection(ConfigurationManager.ConnectionStrings["lojaonlineConnectionString"].ConnectionString);

                        string query2 = "insert into carrinho (id_produto, quantidade, valor) values (";

                        query2 += ((Label)e.Item.FindControl("lbl_cod")).Text + ", ";
                        query2 += quantidade1 + ",";
                        query2 += "parse('" + ((Label)e.Item.FindControl("lbl_valor")).Text + "'as numeric(6,2) using 'PT-pt'))";


                        myCon2.Open();
                        SqlCommand myCommand3 = new SqlCommand(query2, myCon2);
                        myCommand3.ExecuteNonQuery();
                        myCon2.Close();
                    }
                    else
                    {
                        string quantidade = "1";
                        SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["lojaonlineConnectionString"].ConnectionString);

                        string query = "insert into carrinho (id_produto, id_utilizador, quantidade, valor) values (";

                        query += ((Label)e.Item.FindControl("lbl_cod")).Text + ", ";
                        query += id_utilizador + ", ";
                        query += quantidade + ",";
                        query += "parse('" + ((Label)e.Item.FindControl("lbl_valor")).Text + "'as numeric(6,2) using 'PT-pt'))";


                        myCon.Open();
                        SqlCommand myCommand2 = new SqlCommand(query, myCon);
                        myCommand2.ExecuteNonQuery();
                        myCon.Close();
                    }

                }
                else
                {
                    int quantidade2 = 1;
                    SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["lojaonlineConnectionString"].ConnectionString);

                    string query = "update carrinho set ";

                    
                    query += "quantidade=(quantidade+"+ quantidade2 + ")";
                    query += "where id_produto=" + ((Label)e.Item.FindControl("lbl_cod")).Text + "and id_utilizador="+id_utilizador;


                    myCon.Open();
                    SqlCommand myCommand3 = new SqlCommand(query, myCon);
                    myCommand3.ExecuteNonQuery();
                    myCon.Close();
                    
                    
                }

               
            }

            if (e.CommandName.Equals("btn_comprar"))
            {
                SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["lojaonlineConnectionString"].ConnectionString);

                // Seleccionar a SP
                SqlCommand myCommand = new SqlCommand();
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "verificar_carrinho";

                myCommand.Connection = myConn;
                //Passar os parametros para a SP
                myCommand.Parameters.AddWithValue("@id_produto", ((Label)e.Item.FindControl("lbl_cod")).Text);

                SqlParameter valor = new SqlParameter();
                valor.ParameterName = "@retorno";
                valor.Direction = ParameterDirection.Output;
                valor.SqlDbType = SqlDbType.Int;

                myCommand.Parameters.Add(valor);

                myConn.Open();
                myCommand.ExecuteNonQuery();
                int respostaSP = Convert.ToInt32(myCommand.Parameters["@retorno"].Value);
                myConn.Close();

                if (respostaSP == 1)
                {

                    if (id_utilizador == 0)
                    {
                        string quantidade1 = "1";
                        SqlConnection myCon2 = new SqlConnection(ConfigurationManager.ConnectionStrings["lojaonlineConnectionString"].ConnectionString);

                        string query2 = "insert into carrinho (id_produto, quantidade, valor) values (";

                        query2 += ((Label)e.Item.FindControl("lbl_cod")).Text + ", ";
                        query2 += quantidade1 + ",";
                        query2 += "parse('" + ((Label)e.Item.FindControl("lbl_valor")).Text + "'as numeric(6,2) using 'PT-pt'))";


                        myCon2.Open();
                        SqlCommand myCommand3 = new SqlCommand(query2, myCon2);
                        myCommand3.ExecuteNonQuery();
                        myCon2.Close();
                    }
                    else
                    {
                        string quantidade = "1";
                        SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["lojaonlineConnectionString"].ConnectionString);

                        string query = "insert into carrinho (id_produto, id_utilizador, quantidade, valor) values (";

                        query += ((Label)e.Item.FindControl("lbl_cod")).Text + ", ";
                        query += id_utilizador + ", ";
                        query += quantidade + ",";
                        query += "parse('" + ((Label)e.Item.FindControl("lbl_valor")).Text + "'as numeric(6,2) using 'PT-pt'))";


                        myCon.Open();
                        SqlCommand myCommand2 = new SqlCommand(query, myCon);
                        myCommand2.ExecuteNonQuery();
                        myCon.Close();
                    }
                }
                else
                {
                    int quantidade2 = 1;
                    SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["lojaonlineConnectionString"].ConnectionString);

                    string query = "update carrinho set ";


                    query += "quantidade=(quantidade+" + quantidade2 + ")";
                    query += "where id_produto=" + ((Label)e.Item.FindControl("lbl_cod")).Text + "and id_utilizador=" + id_utilizador; 


                    myCon.Open();
                    SqlCommand myCommand3 = new SqlCommand(query, myCon);
                    myCommand3.ExecuteNonQuery();
                    myCon.Close();

                }

                Response.Redirect("carrinho.aspx");

            }

        }
        
    }
}