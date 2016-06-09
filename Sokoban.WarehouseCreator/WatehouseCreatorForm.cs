using System.Windows.Forms;
using Sokoban.Infrastructure;

namespace Sokoban.WarehouseCreator
{
    public class WarehouseCreatorForm : Form
    {
        private readonly TableLayoutPanel table;
        private readonly NumericUpDown heightControl;
        private readonly NumericUpDown widthControl;
        private readonly WarehouseCreatorControl warehouseCreatorControl;

        public WarehouseCreatorForm(WarehouseCreator creator)
        {
            AutoSize = true;
            table = new TableLayoutPanel();
            table.AutoSize = true;

            warehouseCreatorControl = new WarehouseCreatorControl(creator);
            table.Controls.Add(warehouseCreatorControl, 0, 0);

            heightControl = new NumericUpDown();
            widthControl = new NumericUpDown();
            heightControl.Maximum = 10;
            heightControl.Minimum = 1;
            widthControl.Maximum = 15;
            widthControl.Minimum = 1;

            var creatorControls = new TableLayoutPanel();
            creatorControls.Controls.Add(heightControl, 0, 0);
            creatorControls.AutoSize = true;
            creatorControls.Controls.Add(widthControl, 0, 1);
            table.Controls.Add(creatorControls, 1, 0);

            var saveButton = new Button();
            saveButton.Text = "Save";
            saveButton.Width = heightControl.Width;
            saveButton.Click += SaveButton_Click;

            creatorControls.Controls.Add(saveButton);

            Controls.Add(table);


            heightControl.Text = creator.Height.ToString();
            widthControl.Text = creator.Width.ToString();

            heightControl.ValueChanged += CreatorResize;
            widthControl.ValueChanged += CreatorResize;
        }

        private void SaveButton_Click(object sender, System.EventArgs e)
        {
            var saveFileDialog = new SaveFileDialog
            {
                Title = "Save warehouse",
                FileName = "super_level"
            };
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != "")
            {
                JsonHelper.Serialize(
                    warehouseCreatorControl.CurrentWarehouse, 
                    saveFileDialog.FileName);
            }
        }

        private void CreatorResize(object sender, System.EventArgs e)
        {
            var newCreator = new WarehouseCreator((int)heightControl.Value + 1, (int)widthControl.Value + 1);
            warehouseCreatorControl.SetNewCreator(newCreator);
            Size = table.Size;
        }
    }
}