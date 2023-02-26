using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TEATR
{
    public partial class RecoverAcc : Form
    {
        public RecoverAcc()
        {
            InitializeComponent();
        }

        private void RecoverAcc_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            var form = new Authorization();
            form.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                MailAddress from = new MailAddress("38_g@inbox.ru", "Восстановление данных");
                MailAddress to = new MailAddress(textBoxMail.Text);
                MailMessage m = new MailMessage(from, to);
                m.Subject = "Цефея";
                using (EntityModelContainer db = new EntityModelContainer())
                {
                    foreach (Kassir kassir in db.Kassirs)
                    {
                        if (textBoxMail.Text == kassir.Mail)
                        {
                            m.Body = "<h1>Логин: " + kassir.Login + "</h1>" + "<h1>Пароль: " + kassir.Password + "</h1>";
                        }
                    }
                    foreach (Client client in db.Clients)
                    {
                        if (textBoxMail.Text == client.Mail)
                        {
                            m.Body = "<h1>Логин: " + client.Login + "</h1>" + "<h1>Пароль: " + client.Password + "</h1>";
                        }
                    }
                    foreach (Postanov postanov in db.Postanovs)
                    {
                        if (textBoxMail.Text == postanov.Mail)
                        {
                            m.Body = "<h1>Логин: " + postanov.Login + "</h1>" + "<h1>Пароль: " + postanov.Password + "</h1>";
                        }
                    }
                }
                m.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient("smtp.mail.ru", 587);
                smtp.Credentials = new NetworkCredential("38_g@inbox.ru", "pPydF1U0ePfCmTXXTaum");
                smtp.EnableSsl = true;
                smtp.Send(m);
                MessageBox.Show("Логин и пароль отправлены на почту");
                var form = new Authorization();
                form.Show();
                this.Hide();
            }
            catch
            {
                MessageBox.Show("Пользователя с такой почтой не существует");
            }

        }
    }
}
