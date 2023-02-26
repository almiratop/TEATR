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
    public partial class Main : Form
    {
        EntityModelContainer db = new EntityModelContainer();
        Spektak spektak;
        public Main()
        {
            InitializeComponent();
            int n = 3;
            string text = "";
            var selected = from p in db.Spektaks
                           where p.Actual == "+"
                           orderby p.Id descending
                           select p.Id;
            foreach (var w in selected)
            {
                text = text + "," + $"{w}";
            }
            text = text.Replace(" ", "");
            string[] m = text.Split(new char[] { ',' });
            spektak = db.Spektaks.Find(Convert.ToInt32(m[1]));
            pictureBox1.Image = (Image)new ImageConverter().ConvertFrom(spektak.Photo);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            string nam = spektak.Name + "\n" + spektak.Date;
            label4.Text = nam.Substring(0, nam.Length - 3);

            spektak = db.Spektaks.Find(Convert.ToInt32(m[2]));
            pictureBox3.Image = (Image)new ImageConverter().ConvertFrom(spektak.Photo);
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            nam = spektak.Name + "\n" + spektak.Date;
            label5.Text = nam.Substring(0, nam.Length - 3);

            spektak = db.Spektaks.Find(Convert.ToInt32(m[3]));
            pictureBox4.Image = (Image)new ImageConverter().ConvertFrom(spektak.Photo);
            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
            nam = spektak.Name + "\n" + spektak.Date;
            label6.Text = nam.Substring(0, nam.Length - 3);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Authorization form = new Authorization();
            form.Show();
            this.Hide();
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

    }
}
