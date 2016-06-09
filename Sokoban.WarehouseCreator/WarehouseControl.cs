using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Sokoban.Model;
using Sokoban.Model.GameObjects;
using Sokoban.Model.Interfaces;

namespace Sokoban.WarehouseCreator
{
    public class WarehouseControl : Control
    {
        protected Warehouse Warehouse;
        protected int ElementSize = 40;

        public WarehouseControl(Warehouse warehouse)
        {
            this.Warehouse = warehouse;
            DoubleBuffered = true;
            Size = GetOptimalSize();
        }

        private Size GetOptimalSize()
        {
            var width = Warehouse.StaticObjects.Keys.Max(v => v.X) -
                        Warehouse.StaticObjects.Keys.Min(v => v.X);
            var height = Warehouse.StaticObjects.Keys.Max(v => v.Y) -
                         Warehouse.StaticObjects.Keys.Min(v => v.Y);
            return new Size(width*ElementSize, height*ElementSize);
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
            if (obj is Box && Warehouse.StaticObjects[obj.Location] is Storage)
                return new Bitmap("BoxOnStorage.png");
            return new Bitmap("Box.png");
        }

        protected override void OnPaint(PaintEventArgs e)
        {

            foreach (var staticsObject in Warehouse.StaticObjects.Values)
            {
                PaintObject(e, staticsObject);
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
                obj.Location.X*ElementSize,
                obj.Location.Y*ElementSize,
                ElementSize, ElementSize));
        }
    }
}