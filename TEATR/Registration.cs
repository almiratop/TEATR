using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TEATR
{
    public partial class Registration : Form
    {
        EntityModelContainer db = new EntityModelContainer();
        public Registration()
        {
            InitializeComponent();
        }

        private void Registration_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBoxName.Text != "" && textBoxNumber.Text != "" && textBoxMail.Text != "" && textBoxLogin.Text != "" && textBoxPassword.Text != "")
                {
                    int x = 0;
                    int us = db.Clients.Count();
                    using (EntityModelContainer db = new EntityModelContainer())
                    {
                        foreach (Kassir kassir in db.Kassirs)
                        {
                            if (kassir.Login == textBoxLogin.Text && kassir.Password == textBoxPassword.Text)
                            {
                                x = 1;
                                MessageBox.Show("Пользователь с таким логином существует, придумайте другой");
                                break;
                            }
                        }
                        foreach (Postanov post in db.Postanovs)
                        {
                            if (post.Login == textBoxLogin.Text && post.Password == textBoxPassword.Text)
                            {
                                x = 1;
                                MessageBox.Show("Пользователь с таким логином существует, придумайте другой");
                                break;
                            }
                        }
                        foreach (Client client in db.Clients)
                        {
                            if (client.Login == textBoxLogin.Text && client.Password == textBoxPassword.Text)
                            {
                                x = 1;
                                MessageBox.Show("Пользователь с таким логином существует, придумайте другой");
                                break;
                            }
                        }
                        if (x == 0)
                        {
                            Client client = new Client() { Name = textBoxName.Text, Number = textBoxNumber.Text, Mail = textBoxMail.Text, Login = textBoxLogin.Text, Password = textBoxPassword.Text, Skidka = "0", Card = "-" };
                            db.Clients.Add(client);
                            db.SaveChanges();
                            MessageBox.Show("Успешная регистрация");
                            var form = new Authorization();
                            form.Show();
                            this.Hide();
                        }
                    }
                }
                else { MessageBox.Show("Заполните все поля!"); }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void label6_Click(object sender, EventArgs e)
        {
            var form = new Authorization();
            form.Show();
            this.Hide();
        }
    }
}
