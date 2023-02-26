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
using static System.Net.Mime.MediaTypeNames;

namespace TEATR
{
    public partial class LKkassir : Form
    {
        Kassir Kassir;
        EntityModelContainer db = new EntityModelContainer();
        public LKkassir(Kassir kassir)
        {
            InitializeComponent();
            Kassir = kassir;
            var selected = from p in db.Bilets
                           join m in db.Spektaks
                           on p.id_Spektak equals m.Id
                           orderby p.Id descending
                           select new { p.Id, m.Name, m.Date, p.Oplata, p.Status }; ;
            dataGridView1.DataSource = selected.ToArray();
        }

        private void LKkassir_FormClosed(object sender, FormClosedEventArgs e)
        {
            var form = new Main();
            form.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var form = new KassirBuy(Kassir);
            form.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var form = new KassirClients(Kassir);
            form.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                int idd = Convert.ToInt32(textBox1.Text);
                var selected = from p in db.Bilets
                               join m in db.Spektaks
                               on p.id_Spektak equals m.Id
                               where p.Id == idd
                               select new { p.Id, m.Name, m.Date, p.Oplata, p.Status }; ;
                dataGridView1.DataSource = selected.ToArray();
            }
            catch
            {
                var selected = from p in db.Bilets
                               join m in db.Spektaks
                               on p.id_Spektak equals m.Id
                               orderby p.Id descending
                               select new { p.Id, m.Name, m.Date, p.Oplata, p.Status }; ;
                dataGridView1.DataSource = selected.ToArray();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                int index = dataGridView1.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;

                Bilet bilet = db.Bilets.Find(id);

                bilet.Status = "возврат";

                db.SaveChanges();
                dataGridView1.Refresh();
                MessageBox.Show("Изменения сохранены");
                var form = new LKkassir(Kassir);
                form.Show();
                this.Hide();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int sum = 0;
            var dt = DateTime.Now;
            dt.AddMonths(-1);
            var selected = from p in db.Bilets
                           join m in db.Spektaks
                           on p.id_Spektak equals m.Id
                           where p.Status != "возврат" && m.Date > dt
                           orderby p.Id descending
                           select p.Oplata ;
            foreach (var w in selected)
                sum = sum + Convert.ToInt32($"{w}");
            MessageBox.Show(Convert.ToString(sum));
        }
    }
}
