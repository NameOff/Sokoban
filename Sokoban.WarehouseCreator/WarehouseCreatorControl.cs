using System.Windows.Forms;
using Sokoban.Model;
using Sokoban.Application;

namespace Sokoban.WarehouseCreator
{
    public class WarehouseCreatorControl : WarehouseControl
    {
        private WarehouseCreator creator;

        public Warehouse CurrentWarehouse => creator.CurrentWarehouse;

        public WarehouseCreatorControl(WarehouseCreator creator) : base(creator.CurrentWarehouse)
        {
            this.creator = creator;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) != 0)
                creator.NextStaticObject(e.X/ElementSize, e.Y/ElementSize);
            else if ((e.Button & MouseButtons.Right) != 0)
                creator.SetBox(e.X/ElementSize, e.Y/ElementSize);
            else if ((e.Button & MouseButtons.Middle) != 0)
                creator.SetKeeper(e.X/ElementSize, e.Y/ElementSize);
            Warehouse = creator.CurrentWarehouse;
            Invalidate();
        }

        public void SetNewCreator(WarehouseCreator newCreator)
        {
            Warehouse = newCreator.CurrentWarehouse;
            Size = GetOptimalSize();
            creator = newCreator;
        }
    }
}