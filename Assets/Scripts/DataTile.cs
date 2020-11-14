using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public class DataTile
    {
        public int ID;
        public string Name;
        private int x;
        private int y;

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
        public bool SetPosition(Grid<DataTile> grid, int x, int y) => grid.SetGridObject(x, y, this);
    }
}
