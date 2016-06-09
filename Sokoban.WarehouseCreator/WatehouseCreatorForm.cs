using System.Windows.Forms;

namespace Sokoban.WarehouseCreator
{
    public class WarehouseCreatorForm : Form
    {
        private readonly TableLayoutPanel table;

        public WarehouseCreatorForm(WarehouseCreator creator)
        {
            table = new TableLayoutPanel();
            table.AutoSize = true;
            var images = new WarehouseCreatorControl(creator);
            table.Controls.Add(images, 0, 0);

            Controls.Add(table);

            AutoSize = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
        }
    }
}