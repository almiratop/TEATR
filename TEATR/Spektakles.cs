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

    public partial class Spektakles : Form
    {
        FlowLayoutPanel _fpanel;
        EntityModelContainer db = new EntityModelContainer();
        Client Client;
        public Spektakles(Client client)
        {
            InitializeComponent();

            VScrollBar vScrollBar1 = new VScrollBar();
            vScrollBar1.Dock = DockStyle.Right;
            _fpanel = new FlowLayoutPanel();
            _fpanel.Dock = DockStyle.Fill;
            _fpanel.AutoScroll = true;
            _fpanel.BackColor = Color.Transparent;

            this.Controls.Add(_fpanel);
            Client = client;
        }

        private void Spektakles_Load(object sender, EventArgs e)
        {
            using (EntityModelContainer db = new EntityModelContainer())
            {
                foreach (Spektak spektak in db.Spektaks)
                {
                    if (spektak.Actual == "+" && spektak.Date > DateTime.Now)
                    {
                        var pb = new PictureBox();
                        pb.Size = new Size(200, 300);
                        pb.Margin = new Padding(100, 100, 0, 0);
                        pb.SizeMode = PictureBoxSizeMode.StretchImage;
                        pb.Image = (Image)new ImageConverter().ConvertFrom(spektak.Photo);
                        _fpanel.Controls.Add(pb);

                        Button button = new Button();
                        button.BackColor = Color.White;
                        button.Cursor = Cursors.Hand;
                        button.Font = new Font("Monotype Corsiva", 12F, ((FontStyle)((FontStyle.Bold | FontStyle.Italic))), GraphicsUnit.Point, ((byte)(204)));
                        button.ForeColor = Color.Maroon;
                        //button.Location = new Point(542, 12);
                        button.Size = new Size(147, 35);
                        button.Margin = new Padding(100, 100, 0, 0);
                        button.Text = "Подробнее";
                        button.Click += new EventHandler(this.button_Click);
                        button.Tag = spektak.Id;
                        _fpanel.Controls.Add(button);

                    }
                }

            }
        }

        private void button_Click(object sender, EventArgs e)
        {
            Spektak spektak = db.Spektaks.Find(((Control)sender).Tag);
            var form = new ClientBuy(Client, spektak);
            form.textBox1.Text = spektak.Name;
            form.dateTimePicker1.Value = spektak.Date;
            form.richTextBox1.Text = spektak.Actors;
            string sum = Convert.ToString(spektak.Price);
            if(Client.Skidka == "15") { sum = Convert.ToString(spektak.Price * 0.85); }
            form.textBox2.Text = sum;
            form.Show();
            this.Hide();
        }

        private void Spektakles_FormClosed(object sender, FormClosedEventArgs e)
        {
            var form = new LKclient(Client);
            form.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            var form = new LKclient(Client);
            form.Show();
            this.Hide();
        }
    }
}
