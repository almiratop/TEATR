using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TEATR
{
    public partial class PostSpek : Form
    {
        public byte[] imageBytes;
        public PostSpek()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog open_dialog = new OpenFileDialog();
            open_dialog.Filter = "ImageFiles(*.BMP; *.JPG; *.GIF; *.PNG)| *.BMP; *.JPG; *.GIF; *.PNG | Allfiles(*.*) | *.* ";
            if (open_dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    this.imageBytes = File.ReadAllBytes(open_dialog.FileName);
                    label6.Text = "Успешно";
                }
                catch
                {
                    label6.Text = "Ошибка";
                }

            }
        }
    }
}
