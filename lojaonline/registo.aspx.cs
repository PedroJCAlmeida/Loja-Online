using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Net;

namespace lojaonline
{
    public partial class registo : System.Web.UI.Page
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
            }
        }


        protected void btn_registar_Click1(object sender, EventArgs e)
        {
            if (tb_pw.ToString() != tb_pw_confirm.ToString())
            {
                lbl_mensagem.Text = "Confirmação de palavra-passe incorreta !!!";
            }
            else
            {
                string situacao = "Forte";

                Regex maiuscula = new Regex("[A-Z]");
                Regex minuscula = new Regex("[a-z]");
                Regex digitos = new Regex("[0-9]");
                Regex especiais = new Regex("[^a-zA-Z0-9]");
                Regex plica = new Regex("'");

                if (tb_pw.Text.Length < 6)
                {
                    situacao = "Fraco";
                }
                if (maiuscula.Matches(tb_pw.Text).Count == 0)
                {
                    situacao = "Fraco";
                }
                if (minuscula.Matches(tb_pw.Text).Count == 0)
                {
                    situacao = "Fraco";
                }
                if (digitos.Matches(tb_pw.Text).Count == 0)
                {
                    situacao = "Fraco";
                }
                if (especiais.Matches(tb_pw.Text).Count == 0)
                {
                    situacao = "Fraco";
                }
                if (plica.Matches(tb_pw.Text).Count > 0)
                {
                    situacao = "Fraco";
                }

                string fraco = "A senha tem que conter: " + "<br/>" + "Mais de 6 caracteres" + "<br/>" + "1 Letra Maíscula" + "<br/>" + "1 Letra Minúscula" + "<br/>" + "1 Número" + "<br/>" + "1 Caracter especial";
                if (situacao == "Fraco")
                {
                    lbl_mensagem.Text = fraco;

                }
                else if (situacao == "Forte")
                {

                    SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["lojaonlineConnectionString"].ConnectionString);

                    SqlCommand myCommand = new SqlCommand();
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandText = "registar_utilizador";

                    myCommand.Connection = myConn;

                    myCommand.Parameters.AddWithValue("@nome", tb_nome.Text);
                    myCommand.Parameters.AddWithValue("@data_nascimento", tb_data_nascimento.Text);
                    myCommand.Parameters.AddWithValue("@genero", rbtn_genero.SelectedItem.ToString());
                    myCommand.Parameters.AddWithValue("@telefone", tb_telemovel.Text);
                    myCommand.Parameters.AddWithValue("@endereco", tb_endereco.Text);
                    myCommand.Parameters.AddWithValue("@email", tb_email.Text);
                    myCommand.Parameters.AddWithValue("@nif", tb_nif.Text);
                    myCommand.Parameters.AddWithValue("@palavra_passe", EncryptString(tb_pw.Text));

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

                        lbl_mensagem.Text = "Verifique a sua conta de email para ativar a conta de registo !!!";

                    SmtpClient servidor = new SmtpClient();
                    MailMessage email = new MailMessage();

                    email.From = new MailAddress("pedro.almeida.22122@formandos.cinel.pt");
                    email.To.Add(new MailAddress(tb_email.Text));

                    email.Subject = "Ativação de conta !!!";

                    email.IsBodyHtml = true;
                    email.Body = "<b>Obrigado pelo registo na nossa aplicação. <br> Para ativar sua conta clique <a href='https://localhost:44303/ativacao.aspx?user=" + EncryptString(tb_email.Text) + "'>aqui<a></b>";

                    servidor.Host = ConfigurationManager.AppSettings["SMTP_URL"];
                    servidor.Port = int.Parse(ConfigurationManager.AppSettings["SMTP_PORT"]);

                    string utilizador = ConfigurationManager.AppSettings["SMTP_USER"];
                    string pw = ConfigurationManager.AppSettings["SMTP_PASSWORD"];

                    servidor.Credentials = new NetworkCredential(utilizador, pw);
                    servidor.EnableSsl = true;

                    servidor.Send(email);
                }
                else
                {
                    lbl_mensagem.Text = "E-mail já cadastrado !!!";
                }

            

            }
        }

        public static string EncryptString(string Message)
        {
            string Passphrase = "analu";
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();



            // Step 1. We hash the passphrase using MD5
            // We use the MD5 hash generator as the result is a 128 bit byte array
            // which is a valid length for the TripleDES encoder we use below



            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));



            // Step 2. Create a new TripleDESCryptoServiceProvider object
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();



            // Step 3. Setup the encoder
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;



            // Step 4. Convert the input string to a byte[]
            byte[] DataToEncrypt = UTF8.GetBytes(Message);



            // Step 5. Attempt to encrypt the string
            try
            {
                ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
                Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
            }
            finally
            {
                // Clear the TripleDes and Hashprovider services of any sensitive information
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }



            // Step 6. Return the encrypted string as a base64 encoded string



            string enc = Convert.ToBase64String(Results);
            enc = enc.Replace("+", "KLKLK");
            enc = enc.Replace("/", "JLJLJL");
            enc = enc.Replace("\\", "IOIOIO");
            return enc;
        }
    }
}
    
    
