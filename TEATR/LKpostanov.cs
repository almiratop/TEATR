using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TEATR
{
    public partial class LKpostanov : Form
    {
        Postanov Postanov;
        EntityModelContainer db = new EntityModelContainer();
        public LKpostanov(Postanov postanov)
        {
            InitializeComponent();
            Postanov = postanov;
            db.Spektaks.Load();
            dataGridView1.DataSource = db.Spektaks.Local.ToBindingList();
        }

        private void LKpostanov_FormClosed(object sender, FormClosedEventArgs e)
        {
            var form = new Main();
            form.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PostSpek form = new PostSpek();
            DialogResult result = form.ShowDialog(this);

            if (result == DialogResult.Cancel)
                return;

            Spektak spektak = new Spektak() { Name = form.textBoxName.Text, Price = (int)form.numericUpDown1.Value, Date = form.dateTimePicker1.Value, Actors = form.richTextBox1.Text, Photo = form.imageBytes, Actual = "+", PostanovId = Postanov.Id };
            db.Spektaks.Add(spektak);
            db.SaveChanges();
            MessageBox.Show("Спектакль добавлен");
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int index = dataGridView1.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;

                Spektak spektak = db.Spektaks.Find(id);

                PostSpek form2 = new PostSpek();

                form2.textBoxName.Text = spektak.Name;
                form2.numericUpDown1.Value = spektak.Price;
                form2.dateTimePicker1.Value = spektak.Date;
                form2.richTextBox1.Text = spektak.Actors;
                form2.imageBytes = spektak.Photo;
                form2.label6.Text = "Успешно";

                DialogResult result = form2.ShowDialog(this);

                if (result == DialogResult.Cancel)
                    return;

                spektak.Name = form2.textBoxName.Text;
                spektak.Price = (int)form2.numericUpDown1.Value;
                spektak.Date = form2.dateTimePicker1.Value;
                spektak.Actors = form2.richTextBox1.Text;
                spektak.Photo = form2.imageBytes;

                db.SaveChanges();
                dataGridView1.Refresh();
                MessageBox.Show("Изменения сохранены");

            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int index = dataGridView1.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;

                Spektak spektak = db.Spektaks.Find(id);
                if (spektak.Actual == "-") { MessageBox.Show("Афиша уже удалена"); }
                else
                {
                    spektak.Actual = "-";
                    db.SaveChanges();
                    dataGridView1.Refresh();
                    MessageBox.Show("Афиша удалена");
                }

            }

        }

        private void LKpostanov_Load(object sender, EventArgs e)
        {
            var dat = DateTime.Now;
            string text = "";
            var q = from n in db.Spektaks
                    orderby n.Id
                    select new { n.Id };
            foreach (var w in q)
                text = text + "," + $"{w}";

            if (text.Contains("Id"))
            {
                text = text.Replace("Id", "");
                text = text.Replace("=", "");
                text = text.Replace(" ", "");
                text = text.Replace("{", "");
                text = text.Replace("}", "");
                text = text.Substring(1);
                string[] m = text.Split(new char[] { ',' });
                int dl = m.Length;
                int sch = 0;
                while (dl != 0)
                {
                    int id = Convert.ToInt32(m[sch]);
                    Spektak spektak = db.Spektaks.Find(id);
                    if (spektak.Date < DateTime.Now)
                    {
                        spektak.Actual = "-";
                    }
                    dl = dl - 1;
                    sch = sch + 1;
                }
            }
        }
    }
}
