using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Org.BouncyCastle.Crypto.Tls;

namespace lojaonline
{
    public partial class carrinho : System.Web.UI.Page
    {
        int id_utilizador = 0;
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
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
               
             //----------------------------------------------------
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

                //----------------------------------------------------
                string querySource = "SELECT carrinho.id_utilizador, carrinho.id_produto, produtos.nome_produto, carrinho.quantidade, carrinho.valor, produtos.foto, carrinho.quantidade * carrinho.valor AS total FROM carrinho INNER JOIN produtos ON carrinho.id_produto = produtos.id_produto where carrinho.id_utilizador=" + id_utilizador;

                SqlDataSource sqlDataSource1 = SqlDataSource1;
                sqlDataSource1.SelectCommand = querySource;

                //----------------------------------------------------

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

                //----------------------------------------------------
                if(lbl_itens_carrinho.Text != "0")
                {
                    SqlConnection myCon3 = new SqlConnection(ConfigurationManager.ConnectionStrings["lojaonlineConnectionString"].ConnectionString);
                    string query7 = "select sum(quantidade * valor) from carrinho where id_utilizador = " + id_utilizador;
                    SqlCommand myCommand8 = new SqlCommand(query7, myCon3);

                    myCon3.Open();
                    var subtotal = myCommand8.ExecuteReader();

                    if (subtotal.Read())
                    {
                        lbl_subtotal.Text = subtotal.GetDecimal(0).ToString();
                    }
                    myCon3.Close();
                    lbl_total.Text = lbl_subtotal.Text;

                }
                else
                {
                    lbl_subtotal.Text = "0";
                    lbl_total.Text = "0";
                }

                //----------------------------------------------------
                string query3 = "select count(c.id_utilizador) from carrinho as c inner join utilizadores as u on u.id_utilizador = c.id_utilizador where c.id_utilizador =" + id_utilizador;
                SqlCommand myCommand9 = new SqlCommand(query3, myCon);
                myCon.Open();
                var itens3 = myCommand9.ExecuteReader();
                if (itens3.Read())
                {
                    lbl_itens_carrinho.Text = itens3.GetInt32(0).ToString();
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

                string querySource2 = "SELECT carrinho.id_utilizador, carrinho.id_produto, produtos.nome_produto, carrinho.quantidade, carrinho.valor, produtos.foto, carrinho.quantidade * carrinho.valor AS total FROM carrinho INNER JOIN produtos ON carrinho.id_produto = produtos.id_produto where carrinho.id_utilizador is Null";

                SqlDataSource sqlDataSource2 = SqlDataSource1;
                sqlDataSource2.SelectCommand = querySource2;

                if(lbl_itens_carrinho.Text != "0")
                {
                    SqlConnection myCon0 = new SqlConnection(ConfigurationManager.ConnectionStrings["lojaonlineConnectionString"].ConnectionString);
                    string query0 = "select sum(quantidade * valor) from carrinho where id_utilizador is null ";
                    SqlCommand myCommand0 = new SqlCommand(query0, myCon0);

                    myCon0.Open();
                    var subtotal = myCommand0.ExecuteReader();

                    if (subtotal.Read())
                    {   
                            lbl_subtotal.Text = subtotal.GetDecimal(0).ToString();
                    }
                    myCon0.Close();
                    lbl_total.Text = lbl_subtotal.Text;
                }



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
                ((Label)e.Item.FindControl("lbl_quantidade")).Text = dr["quantidade"].ToString();
                ((Label)e.Item.FindControl("lbl_total")).Text = dr["total"].ToString();
                ((System.Web.UI.WebControls.Image)e.Item.FindControl("img_servico")).ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String((byte[])dr["foto"]);
                ((Button)e.Item.FindControl("btn_eliminar")).CommandArgument = dr["id_produto"].ToString();
                

            }
        }

        protected void Repeater1_ItemCommand1(object source, RepeaterCommandEventArgs e)
        {           
            if (e.CommandName.Equals("btn_eliminar"))
            {

                SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["lojaonlineConnectionString"].ConnectionString);

                string query = "";
                if (id_utilizador == 0)
                {
                    query = "delete from carrinho ";
                    query += "where id_produto=" + ((Label)e.Item.FindControl("lbl_cod")).Text + "and id_utilizador is null";

                    myCon.Open();
                    SqlCommand myCommand2 = new SqlCommand(query, myCon);
                    myCommand2.ExecuteNonQuery();
                    myCon.Close();

                }
                else
                {
                    query = "delete from carrinho ";
                    query += "where id_produto=" + ((Label)e.Item.FindControl("lbl_cod")).Text + "and id_utilizador=" + id_utilizador;

                    myCon.Open();
                    SqlCommand myCommand = new SqlCommand(query, myCon);
                    myCommand.ExecuteNonQuery();
                    myCon.Close();
                }
                

               
            }
            if (e.CommandName.Equals("btn_diminuir"))
            {

                int quantidade = 1;
                SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["lojaonlineConnectionString"].ConnectionString);

                string query = "";
                if (id_utilizador == 0)
                {
                    query = "update carrinho set ";
                    query += "quantidade=(quantidade-" + quantidade + ")";
                    query += "where id_produto=" + ((Label)e.Item.FindControl("lbl_cod")).Text + "and id_utilizador is null";


                    myCon.Open();
                    SqlCommand myCommand2 = new SqlCommand(query, myCon);
                    myCommand2.ExecuteNonQuery();
                    myCon.Close();

                }
                else
                {
                    query = "update carrinho set ";
                    query += "quantidade=(quantidade-" + quantidade + ")";
                    query += "where id_produto=" + ((Label)e.Item.FindControl("lbl_cod")).Text + "and id_utilizador=" + id_utilizador;
                    myCon.Open();
                    SqlCommand myCommand3 = new SqlCommand(query, myCon);
                    myCommand3.ExecuteNonQuery();
                    myCon.Close();

                }

            }
            if (e.CommandName.Equals("btn_adicionar"))
            {
                
                int quantidade = 1;
                SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["lojaonlineConnectionString"].ConnectionString);

                string query = "";
                if(id_utilizador == 0)
                {
                    query = "update carrinho set ";
                    query += "quantidade=(quantidade+" + quantidade + ")";
                    query += "where id_produto=" + ((Label)e.Item.FindControl("lbl_cod")).Text + "and id_utilizador is null";
                    myCon.Open();
                    SqlCommand myCommand3 = new SqlCommand(query, myCon);
                    myCommand3.ExecuteNonQuery();
                    myCon.Close();
                }
                else
                {
                    query = "update carrinho set ";
                    query += "quantidade=(quantidade+" + quantidade + ")";
                    query += "where id_produto=" + ((Label)e.Item.FindControl("lbl_cod")).Text + "and id_utilizador=" + id_utilizador;
                    myCon.Open();
                    SqlCommand myCommand3 = new SqlCommand(query, myCon);
                    myCommand3.ExecuteNonQuery();
                    myCon.Close();
                }

            }
        }

        protected void btn_verificar_cupom_Click(object sender, EventArgs e)
        {
            if (tb_cupom_desconto.Text == "")
            {
                Response.Write("Insira um cupom de desconto !!!!");
            }
            else
            {
                SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["lojaonlineConnectionString"].ConnectionString);


                SqlConnection myCon3 = new SqlConnection(ConfigurationManager.ConnectionStrings["lojaonlineConnectionString"].ConnectionString);
                string query4 = "select desconto from descontos where chave='" + tb_cupom_desconto.Text + "'";
                SqlCommand myCommand8 = new SqlCommand(query4, myCon3);
                string valor_desconto = "";
                string percentual_desconto = "";
                myCon3.Open();
                var desconto = myCommand8.ExecuteReader();
               
                if(desconto.Equals(null))
                {
                    Response.Write("Cupom de desconto inválido !!!!");
                }
                else if(desconto.Read())
                {
                    percentual_desconto = desconto.GetInt32(0).ToString("F");
                    valor_desconto = ((double.Parse(lbl_subtotal.Text) * double.Parse(percentual_desconto)) / 100).ToString("F");
                    lbl_desconto.Text = valor_desconto;
                    lbl_total.Text = (double.Parse(lbl_subtotal.Text) - double.Parse(valor_desconto)).ToString("F");
                }
                myCon3.Close();

                
               
            }
                
        }

        protected void btn_finalizar_compra_Click(object sender, EventArgs e)
        {
            if (lbl_utilizador.Text == "Utilizador")
            {
                Response.Write("Precisa realizar o Login para finalizar a compra !!!");
            }
            else if (lbl_itens_carrinho.Text == "0")
            {
                Response.Write("Carrinho vazio !!!");
            }
            else
            {
                SqlConnection myCon4 = new SqlConnection(ConfigurationManager.ConnectionStrings["lojaonlineConnectionString"].ConnectionString);

                string query5 = "select c.quantidade, p.nome_produto, c.valor, (c.quantidade * c.valor)as total from carrinho as c inner join produtos as p on c.id_produto = p.id_produto where c.id_utilizador=" + id_utilizador;

                SqlCommand myCommand7 = new SqlCommand(query5, myCon4);

                int qtd_recibo = 0;
                string servico_recibo = "";
                string valor_servico = "";
                string valor_com_desconto = "";
                string valor_desconto_recibo = "";
                string percentual_desconto = "";
                myCon4.Open();
                var recibo = myCommand7.ExecuteReader();
                if (recibo.Read())
                {
                    qtd_recibo = recibo.GetInt32(0);
                    servico_recibo = recibo.GetString(1);
                    valor_servico = recibo.GetDecimal(3).ToString("F");
                }
                myCon4.Close();


                if(lbl_desconto.Text !="0") 
                {
                    string query4 = "select desconto from descontos where chave='" + tb_cupom_desconto.Text + "'";
                    SqlCommand myCommand8 = new SqlCommand(query4, myCon4);

                    myCon4.Open();
                    var desconto = myCommand8.ExecuteReader();
                    if (desconto.Read())
                    {
                        percentual_desconto = desconto.GetInt32(0).ToString();
                    }
                    myCon4.Close();

                    valor_desconto_recibo = ((double.Parse(valor_servico) * double.Parse(percentual_desconto)) / 100).ToString("F");
                    valor_com_desconto = (double.Parse(valor_servico) - double.Parse(valor_desconto_recibo)).ToString("F");
                }
                valor_desconto_recibo = "0,00";
                valor_com_desconto = valor_servico;

                string caminhoPDFS = ConfigurationManager.AppSettings["PathPDFS"];

                string siteURL = ConfigurationManager.AppSettings["SiteURL"];

                string pdfTemplate = caminhoPDFS + "Template\\Template_Recibo.pdf";

                string nomePDF = (DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace(" ", "")) + ".pdf";

                string novoFicheiro = caminhoPDFS + "Gerados\\" + nomePDF;

                PdfReader preader = new PdfReader(pdfTemplate);
                PdfStamper pstamper = new PdfStamper(preader, new FileStream(novoFicheiro, FileMode.Create));
                AcroFields pdfFields = pstamper.AcroFields;
                string n = "0";
               
                    pdfFields.SetField("qtd" + n, qtd_recibo.ToString());
                    pdfFields.SetField("servico" + n, servico_recibo.ToString());
                    pdfFields.SetField("valor" + n, valor_com_desconto.ToString());
                    pdfFields.SetField("subtotal" + n, valor_servico.ToString());
                    pdfFields.SetField("desconto" + n, valor_desconto_recibo.ToString());
                    pdfFields.SetField("total", valor_com_desconto.ToString());
                


                pstamper.Close();

                SmtpClient servidor = new SmtpClient();
                MailMessage email = new MailMessage();

                string arquivo = "PDFS/Gerados/" + nomePDF;

                email.From = new MailAddress("pedro.almeida.22122@formandos.cinel.pt");
                email.To.Add(new MailAddress(lbl_utilizador.Text));

                email.Subject = "Registo de compra !!!";
                email.Attachments.Add(new Attachment(novoFicheiro));
                email.IsBodyHtml = true;
                email.Body = "<b>Obrigado por aderiri nossos serviços. <br> Segue em anexo vosso recibo<a></b>";

                servidor.Host = ConfigurationManager.AppSettings["SMTP_URL"];
                servidor.Port = int.Parse(ConfigurationManager.AppSettings["SMTP_PORT"]);

                string utilizador = ConfigurationManager.AppSettings["SMTP_USER"];
                string pw = ConfigurationManager.AppSettings["SMTP_PASSWORD"];

                servidor.Credentials = new NetworkCredential(utilizador, pw);
                servidor.EnableSsl = true;

                servidor.Send(email);
                Response.Write("Foi envido o recibo para o e-mail de registo: " + lbl_utilizador.Text);
            }
            
            }
    }
}