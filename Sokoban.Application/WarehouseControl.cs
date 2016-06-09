using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Sokoban.Model;
using Sokoban.Model.GameObjects;
using System.Linq;
using Sokoban.Model.Interfaces;

namespace Sokoban.Application
{
    public class WarehouseControl : Control
    {
        protected int ElementSize = 40;
        protected Warehouse Warehouse;

        public WarehouseControl(Warehouse warehouse)
        {
            Warehouse = warehouse;
            DoubleBuffered = true;
            Size = GetOptimalSize();
        }

        public void SetWarehouse(Warehouse warehouse)
        {
            Warehouse = warehouse;
            Size = GetOptimalSize();
            Invalidate();
        }

        public Size GetOptimalSize()
        {
            var width = Warehouse.StaticObjects.Select(obj => obj.Location).Max(v => v.X) -
                        Warehouse.StaticObjects.Select(obj => obj.Location).Min(v => v.X) + 1;
            var height = Warehouse.StaticObjects.Select(obj => obj.Location).Max(v => v.Y) -
                         Warehouse.StaticObjects.Select(obj => obj.Location).Min(v => v.Y) + 1;

            return new Size(width * ElementSize, height * ElementSize);
        }

        private Bitmap GetImage(IGameObject obj)
        {
            const string folder = "Images";
            if (obj is Wall)
                return new Bitmap(Path.Combine(folder, "Wall.png"));
            if (obj is Floor)
                return new Bitmap(Path.Combine(folder, "Floor.png"));
            if (obj is Storage)
                return new Bitmap(Path.Combine(folder, "Storage.png"));
            if (obj is WarehouseKeeper)
                return new Bitmap(Path.Combine(folder, "WarehouseKeeper.png"));
            if (obj is Box && Warehouse.GetStaticObject(obj.Location) is Storage)
                return new Bitmap(Path.Combine(folder, "BoxOnStorage.png"));
            return new Bitmap(Path.Combine(folder, "Box.png"));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            foreach (var staticObject in Warehouse.StaticObjects)
            {
                PaintObject(e, staticObject);
            }

            foreach (var box in Warehouse.Boxes)
            {
                PaintObject(e, box);
            }
            PaintObject(e, Warehouse.Keeper);
        }

        private void PaintObject(PaintEventArgs e, IGameObject obj)
        {
            if (obj == null)
                return;

            var image = GetImage(obj);

            e.Graphics.DrawImage(image, new Rectangle(
                obj.Location.X * ElementSize,
                obj.Location.Y * ElementSize,
                ElementSize, ElementSize));
        }
    }
}