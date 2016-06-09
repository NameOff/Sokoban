using System.Windows.Forms;

namespace Sokoban.WarehouseCreator
{
    public class WarehouseCreatorControl : WarehouseControl
    {
        private readonly WarehouseCreator creator;

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
    }
}