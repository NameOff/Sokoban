using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sokoban.Model.GameObjects;
using Sokoban.Model.Interfaces;

namespace Sokoban.WarehouseCreator
{
    public class WarehouseCreatorView : Control
    {
        private WarehouseCreator creator;
        private int ElementSize = 40;
        public WarehouseCreatorView(WarehouseCreator creator)
        {
            this.creator = creator;
            DoubleBuffered = true;
            Size = new Size(creator.Width * ElementSize, creator.Height * ElementSize);
        }

        private Bitmap GetImage(IGameObject obj)
        {
            if (obj is Wall)
                return new Bitmap("Wall.png");
            if (obj is Floor)
                return new Bitmap("Floor.png");
            if (obj is Storage)
                return new Bitmap("Storage.png");
            if (obj is WarehouseKeeper)
                return new Bitmap("WarehouseKeeper.png");
            if (obj is Box && creator.GetObject(obj.Location.X, obj.Location.Y) is Storage)
                return new Bitmap("BoxOnStorage.png");
            return new Bitmap("Box.png");
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.Gray, 0, 0, ElementSize * creator.Width, ElementSize * creator.Height);

            foreach (var staticsObject in creator.StaticsObjects)
            {
                PaintObject(e, staticsObject);
            }

            foreach (var box in creator.Boxes)
            {
                PaintObject(e, box);
            }
            PaintObject(e, creator.Keeper);
        }

        private void PaintObject(PaintEventArgs e, IGameObject obj)
        {
            if (obj == null)
                return;
            var image = GetImage(obj);

            e.Graphics.DrawImage(image, new Rectangle(
                obj.Location.X*ElementSize,
                obj.Location.Y*ElementSize,
                ElementSize, ElementSize));
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) != 0)
                creator.NextStaticObject(e.X/ElementSize, e.Y/ElementSize);
            else if ((e.Button & MouseButtons.Right) != 0)
                creator.SetBox(e.X/ElementSize, e.Y/ElementSize);
            else if ((e.Button & MouseButtons.Middle) != 0)
                creator.SetKeeper(e.X/ElementSize, e.Y/ElementSize);
            Invalidate();
        }
    }
    public class WarehouseCreatorForm : Form
    {
        private TableLayoutPanel table;
        public WarehouseCreatorForm(WarehouseCreator creator)
        {
            table = new TableLayoutPanel();
            table.AutoSize = true;
            var images = new WarehouseCreatorView(creator);
            table.Controls.Add(images, 0, 0);

            Controls.Add(table);

            AutoSize = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
        }
    }


}
