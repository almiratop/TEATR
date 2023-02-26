using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;

namespace TEATR
{
    public partial class Authorization : Form
    {
        public Authorization()
        {
            InitializeComponent();
        }

        private void Authorization_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            var form = new Registration();
            form.Show();
            this.Hide();
        }
        private void label4_Click(object sender, EventArgs e)
        {
            var form = new RecoverAcc();
            form.Show();
            this.Hide();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int x = 0;
            try
            {
                if (textBoxLogin.Text != "" && textBoxPassword.Text != "")
                {
                    using (EntityModelContainer db = new EntityModelContainer())
                    {
                        foreach (Kassir kassir in db.Kassirs)
                        {
                            if (kassir.Login == textBoxLogin.Text && kassir.Password == textBoxPassword.Text)
                            {
                                x = 1;
                                int id = kassir.Id;
                                var form = new LKkassir(kassir);
                                form.Show();
                                this.Hide();
                            }
                        }
                        foreach (Postanov post in db.Postanovs)
                        {
                            if (post.Login == textBoxLogin.Text && post.Password == textBoxPassword.Text)
                            {
                                x = 1;
                                int id = post.Id;
                                var form = new LKpostanov(post);
                                form.Show();
                                this.Hide();
                            }
                        }
                        foreach (Client client in db.Clients)
                        {
                            if (client.Login == textBoxLogin.Text && client.Password == textBoxPassword.Text)
                            {
                                x = 1;
                                int id = client.Id;
                                var form = new LKclient(client);
                                form.Show();
                                this.Hide();
                            }
                        }
                        if (x == 0) { MessageBox.Show("Не существует пользователя с такими данными"); }

                    }
                }
                else { MessageBox.Show("Заполните все поля!"); }
            }
            catch { MessageBox.Show("Непредвиденная ошибка, повторите позже"); }
            //using (EntityModelContainer db = new EntityModelContainer())
            //{
            //    foreach (Client client in db.Clients)
            //    {
            //        if (client.Login == "almira" && client.Password == "123")
            //        {
            //            var form = new LKclient(client);
            //            form.Show();
            //            this.Hide();
            //        }
            //    }

            //}
        }

        private void label6_Click(object sender, EventArgs e)
        {
            var form = new Main();
            form.Show();
            this.Hide();
        }
    }
}
