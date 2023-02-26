using System;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Xceed.Document.NET;
using Xceed.Words.NET;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace TEATR
{
    public partial class LKclient : Form
    {
        Client Client;
        EntityModelContainer db = new EntityModelContainer();
        public LKclient(Client client)
        {
            InitializeComponent();
            Client = client;
            var selected = from p in db.Bilets
                           join m in db.Spektaks
                           on p.id_Spektak equals m.Id
                           where p.id_Buyer == Client.Id && p.Buyer == "Клиент"
                           orderby p.Id descending
                           select new { p.Id, m.Name, m.Date, p.Oplata, p.Status}; ;
            dataGridView1.DataSource = selected.ToArray();
        }

        private void LKclient_FormClosed(object sender, FormClosedEventArgs e)
        {
            var form = new Main();
            form.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var form = new Spektakles(Client);
            form.Show();
            this.Hide();
        }

        private void LKclient_Load(object sender, EventArgs e)
        {
            string text = "";
            var q = from n in db.Bilets
                        where (n.Buyer == "Клиент") && (n.id_Buyer == Client.Id) && (n.Status != "возврат")
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
                string[] m = text.Split(new char[] { ',' }); //[1, 2, 3]
                int dl = m.Length;
                int sch = 0;
                while (dl != 0)
                {
                    int id = Convert.ToInt32(m[sch]);
                    Bilet bilet = db.Bilets.Find(Convert.ToInt32(id));
                    Spektak spektak = db.Spektaks.Find(bilet.id_Spektak);
                    if (spektak.Date < DateTime.Now)
                    {
                        bilet.Status = "истек";
                        spektak.Actual = "-";
                    }
                    dl = dl - 1;
                    sch = sch + 1;
                }
            }

            int kol = 0;
            var query = from n in db.Bilets
                        where (n.Buyer == "Клиент") && (n.id_Buyer == Client.Id) && (n.Status != "возврат")
                        orderby n.Id
                        select n.Id;
            foreach (var w in query)
                kol = kol + 1;
            if (kol >= 10)
            {
                Client client = db.Clients.Find(Client.Id);
                client.Skidka = "15";
                db.SaveChanges();
                Client = client;
                label3.Text = "Ваша скидка: 15%";
            }
        }

        private void button1_Click(object sender, EventArgs e)
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
                var form = new LKclient(Client);
                form.Show();
                this.Hide();
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
                Spektak spectac = db.Spektaks.Find(bilet.id_Spektak);
                string datt = Convert.ToString(spectac.Date);
                datt = datt.Substring(0, datt.Length - 3);
                string n = Convert.ToString(bilet.Id) + ": " + spectac.Name + " " + datt;
                string namedoc = Convert.ToString(bilet.Id) + ".docx";
                string pathDocument = @"C:\Users\1234\Downloads\" + namedoc;
                DocX document = DocX.Create(pathDocument);
                document.MarginLeft = 60.0f;
                document.MarginRight = 60.0f;
                document.MarginTop = 60.0f;
                document.MarginBottom = 60.0f;
                document.AddImage(@"C:\Users\1234\Desktop\TEATR\TEATR\bin\Debug\head.PNG");
                document.InsertParagraph("\n" + "Билет").Bold().Font("Times New Roman").FontSize(14).Alignment = Alignment.center;
                document.InsertParagraph("\n" + Convert.ToString(bilet.Id)).Bold().Font("Times New Roman").FontSize(20).Alignment = Alignment.center;
                document.InsertParagraph("\n + Название спектакля: " + Convert.ToString(spectac.Name) + "\n + Дата: " + datt + "\n + Статус: " + Convert.ToString(bilet.Status) + "\n + Оплачено: " + Convert.ToString(bilet.Oplata))
                    .Bold().Font("Times New Roman").FontSize(20).Alignment = Alignment.left;
                document.Save();
                MessageBox.Show("Проверьте папку загрузки");

                //string s = "Билет" + "\n" + Convert.ToString(bilet.Id) + "\n + Название спектакля: " + Convert.ToString(spectac.Name) + "\n + Дата: " + Convert.ToString(spectac.Date) + "\n + Статус: " + Convert.ToString(bilet.Status) + "\n + Оплачено: " + Convert.ToString(bilet.Oplata);
                //using (StreamWriter writer = new StreamWriter(bilet.Id + ": " + spectac.Name + " " + spectac.Date + ".docx", false))
                //{
                //    writer.WriteLineAsync(s);
                //}

            }
        }
    }
}
