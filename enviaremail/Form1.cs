using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;

namespace enviaremail
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            EmailEnviar();
        }

        private void LimparCampos()
        {
            
            txtDestinatario.Text = string.Empty;
            txtAssunto.Text = string.Empty;
            txtMensagem.Text = string.Empty;
            lblAnexo.Text = string.Empty;
            txtDestinatario.Focus();
           
        }

        private void EmailEnviar()
        {
            try
            {
                using (SmtpClient smtp = new SmtpClient())
                {
                    using (MailMessage email = new MailMessage())
                    {
                        //Servidor SMTP
                        smtp.Host = "smtpi.valebuscard.com.br";
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential("luiz.carlos@valebuscard.com.br", "Lu1z#C4rl0s");
                        smtp.Port = 587;
                        smtp.EnableSsl = false;

                        //Email (Mensagem)
                        email.From = new MailAddress("luiz.carlos@valebuscard.com.br");
                        email.To.Add(txtDestinatario.Text);

                        //Mandar Email
                        email.Subject = txtAssunto.Text;
                        email.IsBodyHtml = true;
                        //email.Body = "<html>\r\n<head>\r\n    <title>Template</title>\r\n</head>\r\n<body>\r\n    <table>\r\n        <tr>\r\n            <td>\r\n                <img src=\"https://sitranscg.com.br/wp-content/uploads/img/cliente3.png\" alt=\"\" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <td>\r\n                ##Mensagem##\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <td>\r\n                Cesar Cassiano Schimanco\r\n                <a href=\"http://www.cbsa.com.br\">www.cbsa.com.br</a>\r\n            </td>\r\n        </tr>\r\n    </table>\r\n</body>\r\n</html>";
                        email.Body = "<html>" +
                                      "<body>" +
                                      "<table>" +
                                      "<tr>" +
                                      "<td>" +
                                      "Uma homenagem aos melhores clientes." +
                                      "</td>" +
                                      "</tr>" +
                                      "<tr>" +
                                      "<td>" +
                                      "<img src=\"https://www.sitranscg.com.br/wp-content//uploads/img/cliente3.png\" alt=\"\" />" +
                                      "</td>" +
                                      "</tr>" +
                                      " </table>" +
                                      "</body>" +
                                      "</html>";
                       
                        //email.Body = txtMensagem.Text;

                        if (lblAnexo.Text != "")
                        {
                            var anexo = lblAnexo.Text.ToString().Split(';');
                            for (int i = 0; i < anexo.Count(); i++)
                                email.Attachments.Add(new Attachment(anexo[i]));
                        }


                        //Enviar email
                        smtp.Send(email);

                    }
                }
                LimparCampos();
                MessageBox.Show("Email enviado.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }

        private void lblAnexo_Click(object sender, EventArgs e)
        {
            var anexo = new OpenFileDialog();
            
            anexo.Multiselect = true;
            anexo.Title = "Anexar Arquivo";

            if (anexo.ShowDialog() == DialogResult.OK)
                for (int i = 0; i < anexo.FileNames.Count(); i++)
                    if (i == 0)
                lblAnexo.Text = anexo.FileNames[i];
            else
                lblAnexo.Text = lblAnexo.Text + ";" + anexo.FileNames[i];
        }
    }
}
