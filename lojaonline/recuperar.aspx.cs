using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;

namespace lojaonline
{
    public partial class recuperar : System.Web.UI.Page
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
                string query3 = "select count(c.id_utilizador) from carrinho as c inner join utilizadores as u on u.id_utilizador = c.id_utilizador where c.id_utilizador =" + id_utilizador;
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

        protected void btn_recuperar_Click(object sender, EventArgs e)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["lojaonlineConnectionString"].ConnectionString);

            SqlCommand myCommand = new SqlCommand();
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.CommandText = "verificar_conta";

            myCommand.Connection = myConn;

            myCommand.Parameters.AddWithValue("@email", tb_email.Text);

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
                string nova_passe = "";
                Random aleatorio = new Random();
                nova_passe = aleatorio.Next(111111, 1000000).ToString();

                SqlConnection myConn2 = new SqlConnection(ConfigurationManager.ConnectionStrings["lojaonlineConnectionString"].ConnectionString);

                SqlCommand myCommand2 = new SqlCommand();
                myCommand2.CommandType = CommandType.StoredProcedure;
                myCommand2.CommandText = "recuperar_passe";

                myCommand2.Connection = myConn2;

                myCommand2.Parameters.AddWithValue("@email", tb_email.Text);
                myCommand2.Parameters.AddWithValue("@pwNova", EncryptString(nova_passe));

                SqlParameter valor2 = new SqlParameter();
                valor2.ParameterName = "@retorno";
                valor2.Direction = ParameterDirection.Output;
                valor2.SqlDbType = SqlDbType.Int;

                myCommand2.Parameters.Add(valor2);


                myConn2.Open();
                myCommand2.ExecuteNonQuery();
                int respostaSP2 = Convert.ToInt32(myCommand2.Parameters["@retorno"].Value);
                myConn2.Close();

                if (respostaSP2 == 1)
                {
                    SmtpClient servidor = new SmtpClient();
                    MailMessage email = new MailMessage();

                    email.From = new MailAddress("pedro.almeida.22122@formandos.cinel.pt");
                    email.To.Add(new MailAddress(tb_email.Text));

                    email.Subject = "Recuperação de Palavra-Passe !!!";

                    email.IsBodyHtml = true;
                    email.Body = $"<b>Palavra-Passe recuperada com sucesso !!!. <br> Palavra-Passe: {nova_passe} </b>";

                    servidor.Host = ConfigurationManager.AppSettings["SMTP_URL"];
                    servidor.Port = int.Parse(ConfigurationManager.AppSettings["SMTP_PORT"]);

                    string utilizador = ConfigurationManager.AppSettings["SMTP_USER"];
                    string pw = ConfigurationManager.AppSettings["SMTP_PASSWORD"];

                    servidor.Credentials = new NetworkCredential(utilizador, pw);
                    servidor.EnableSsl = true;

                    servidor.Send(email);

                    lbl_mensagem.Text = "Foi enviado a nova Palavra-Passe para vosso email !!!";
                }

            }
            else if (respostaSP == 0)
            {
                lbl_mensagem.Text = "Utilizador não cadastrado !!!";
            }
            else if (respostaSP == 2)
            {
                lbl_mensagem.Text = "Utilizador inativo !!!";
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
