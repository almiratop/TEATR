using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xceed.Document.NET;
using Xceed.Words.NET;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TEATR
{
    public partial class KassirBuy : Form
    {
        Kassir Kassir;
        EntityModelContainer db = new EntityModelContainer();
        public KassirBuy(Kassir kassir)
        {
            InitializeComponent();
            Kassir = kassir;
            string text = "";
            var query = from n in db.Spektaks
                        where n.Actual == "+" && n.Date > DateTime.Now
                        orderby n.Id
                        select new { n.Id, n.Name, n.Date };
            foreach (var w in query)
            {
                text = Convert.ToString(w);
                text = text.Replace("Id", "");
                text = text.Replace("Name", "");
                text = text.Replace("Date", "");
                text = text.Replace("=", "");
                text = text.Replace("{", "");
                text = text.Replace("}", "");
                text = text.Replace("  ", " ");
                text = text.Substring(2);
                text = text.Substring(0, text.Length - 4);
                comboBox1.Items.Add(text);
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            var form = new LKkassir(Kassir);
            form.Show();
            this.Hide();
        }

        private void KassirBuy_FormClosed(object sender, FormClosedEventArgs e)
        {
            var form = new LKkassir(Kassir);
            form.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string fu = comboBox1.SelectedItem.ToString();
            string[] zayavk = fu.Split(new char[] { ',' });
            Spektak spektak = db.Spektaks.Find(Convert.ToInt32(zayavk[0]));
            double price = spektak.Price;
            if (radioButton1.Checked == true) { price = price * 0.85; }
            Bilet bilet = new Bilet() { Date = DateTime.Now, Oplata = Convert.ToString(price), Buyer = "Кассир", id_Buyer = Kassir.Id, id_Spektak = spektak.Id, Status = "активно" };
            db.Bilets.Add(bilet);
            db.SaveChanges();

            string datt = Convert.ToString(spektak.Date);
            datt = datt.Substring(0, datt.Length - 3);
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
            document.InsertParagraph("\n + Название спектакля: " + Convert.ToString(spektak.Name) + "\n + Дата: " + datt + "\n + Статус: " + Convert.ToString(bilet.Status) + "\n + Оплачено: " + Convert.ToString(bilet.Oplata))
                .Bold().Font("Times New Roman").FontSize(20).Alignment = Alignment.left;
            document.Save();
            MessageBox.Show("Билет куплен и скачан в папку загрузки");
            var form = new LKkassir(Kassir);
            form.Show();
            this.Hide();
        }
    }
}
