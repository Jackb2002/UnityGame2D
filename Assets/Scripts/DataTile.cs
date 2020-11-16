using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    [Serializable]
    public class DataTile
    {
        public int ID;
        public string Name;
        public string SpritePath;
        [SerializeField]
        private readonly int x;
        [SerializeField]
        private readonly int y;
        [NonSerialized]
        public Vector3 WorldPosition;
        [SerializeField]
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
        public DataTile(int ID, string name, SerializeableVector3 Position, string Path) : this(ID, name, Position.GetVector(),Path)
        {
            Debug.Log("Created Deserialized DataTile Object");
        }

        public DataTile(int iD, string name, Vector3 position, int x, int y, string Path) : this(iD, name, position, Path)
        {
            this.x = x;
            this.y = y;
        }
    }
}
