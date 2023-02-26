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
    public partial class KassirClients : Form
    {
        Kassir Kassir;
        EntityModelContainer db = new EntityModelContainer();
        public KassirClients(Kassir kassir)
        {
            InitializeComponent();
            Kassir = kassir;
            var selected = from p in db.Clients
                           where p.Skidka == "15" && p.Card == "-"
                           select new { p.Id, p.Name, p.Number, p.Skidka, p.Card }; ;
            dataGridView1.DataSource = selected.ToArray();
        }

        private void KassirClients_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            var form = new LKkassir(Kassir);
            form.Show();
            this.Hide();
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

                Client client = db.Clients.Find(id);

                client.Card = "+";

                db.SaveChanges();
                dataGridView1.Refresh();
                MessageBox.Show("Изменения сохранены");
                var form = new KassirClients(Kassir);
                form.Show();
                this.Hide();
            }
        }
    }
}
