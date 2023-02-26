using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TEATR
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //EntityModelContainer db = new EntityModelContainer();
            //Kassir kassir = db.Kassirs.Find(1);
            //Application.Run(new LKkassir(kassir));
            Application.Run(new Main());
        }
    }
}
