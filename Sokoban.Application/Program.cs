using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Sokoban.Model;
using System.IO;
using System.Linq;
using Sokoban.Infrastructure;

namespace Sokoban.Application
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 

        public static IEnumerable<Level> LoadLevels()
        {
            return Directory
                .GetFiles("Levels")
                .OrderBy(file => file)
                .Select(file => new Level(JsonHelper.Deserialize<Warehouse>(file)));
        }

        [STAThread]
        static void Main()
        {
            var levels = LoadLevels().ToList();
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            System.Windows.Forms.Application.Run(new WarehouseGameForm(new XboxGamepad(), levels));
        }
    }
}
