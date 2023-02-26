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
    public partial class ClientBuy : Form
    {
        Client Client;
        Spektak Spektak;
        EntityModelContainer db = new EntityModelContainer();

        public ClientBuy(Client client, Spektak spektak)
        {
            InitializeComponent();
            Client = client;
            Spektak = spektak;
        }

        private void ClientBuy_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            var form = new LKclient(Client);
            form.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bilet bilet = new Bilet() { Date = DateTime.Now, Oplata = textBox2.Text, Buyer = "Клиент", id_Buyer = Client.Id, id_Spektak = Spektak.Id, Status = "активно"};
            db.Bilets.Add(bilet);
            db.SaveChanges();
            MessageBox.Show("Билет куплен");
            var form = new LKclient(Client);
            form.Show();
            this.Hide();
        }
    }
}
