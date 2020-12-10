using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    [Serializable]
    public class DataTile
    {
        public int ID;
        public string Name;
        public string SpritePath;
        public readonly int x;
        public readonly int y;
        public Vector3 WorldPosition;
        private SerializeableVector3 sVec;

        public Dictionary<string, object> TileInfo = new Dictionary<string, object>();

        public DataTile(int ID, string name, Vector3 Position, string Path)
        {
            this.ID = ID;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            WorldPosition = Position;
            sVec = new SerializeableVector3(WorldPosition);
            SpritePath = Path;
        }
        public DataTile(int ID, string name, SerializeableVector3 Position, string Path) : this(ID, name, Position.GetVector(), Path)
        {
            Debug.Log("Created Deserialized DataTile Object");
        }

        public DataTile(int iD, string name, Vector3 position, int x, int y, string Path) : this(iD, name, position, Path)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return $"{x}:{y} - {Name}";
        }

        public static Grid<DataTile> InitializeGrid(Grid<DataTile> g, List<DataTile> d)
        {
            for (int x = 0; x < g.GetWidth(); x++)
            {
                for (int y = 0; y < g.GetHeight(); y++)
                {
                    var tile = d.Where(item => item.x == x && item.y == y);
                    var element = tile.ElementAt(0);
                    if (element != null)
                    {
                        g.SetGridObject(x, y, element);
                    }
                }
            }
            return g;
        }

        internal static void AssignTileInfo(DataTile tile)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            switch (tile.ID)
            {
                case 0:
                    //Solid block input data
                    data.Add("solid", true);
                    break;
                case 1:
                    //Damage block input data
                    data.Add("solid", true);
                    data.Add("damage", 10f);
                    break;
                case 2:
                    //Timer block input data
                    data.Add("solid", true);
                    data.Add("timer", 1500f);
                    break;
                case 3:
                    //Goal block input data
                    data.Add("solid", true);
                    data.Add("goal", true);
                    break;
                case 4:
                    //Spawn block input data
                    data.Add("solid", true);
                    data.Add("spawn", true);
                    break;
            }
            tile.TileInfo = data;
        }
    }
}
