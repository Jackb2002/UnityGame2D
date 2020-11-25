using System;
using UnityEngine;

namespace Assets.Scripts
{
    [Serializable]
    public class SerializeableVector3
    {
        public readonly float x;
        public readonly float y;
        public readonly float z;

        public SerializeableVector3(Vector3 pos)
        {
            x = pos.x;
            y = pos.y;
            z = pos.z;
        }

        public UnityEngine.Vector3 GetVector()
        {
            return new UnityEngine.Vector3(x, y, z);
        }
    }
}
