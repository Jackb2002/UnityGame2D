using System;

namespace Assets.Scripts
{
    public class DataTile
    {
        public int ID;
        public string Name;
        private readonly int x;
        private readonly int y;

        public DataTile(int ID, string name)
        {
            this.ID = ID;
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public DataTile(int iD, string name, int x, int y) : this(iD, name)
        {
            this.x = x;
            this.y = y;
        }



        /// <summary>
        /// Set position of the data tile
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>Bool - Success?</returns>
        public bool SetPosition(Grid<DataTile> grid, int x, int y)
        {
            return grid.SetGridObject(x, y, this);
        }
    }
}
