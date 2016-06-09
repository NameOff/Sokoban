using System;
using System.Windows.Forms;

namespace Sokoban.WarehouseCreator
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new WarehouseCreatorForm(new WarehouseCreator(10, 10)));
        }
    }
}