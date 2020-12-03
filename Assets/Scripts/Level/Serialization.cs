using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Level
{
    public static class Serialization
    {
        private static StringBuilder FileContents = new StringBuilder();

        public static string SaveLevel(string Path, LevelData ld)
        {
            if (File.Exists(Path))
            {
                Debug.Log("File Already Exists, moving to .old file");
                File.Move(Path, Path + ".old");
                File.Delete(Path);
            }

            FileStream fs = File.Create(Path);
            AddToFile(ld.Author + ";" + Environment.NewLine);
            AddToFile(ld.maxPlayers + ";" + Environment.NewLine);
            for (int x = 0; x < ld.Data.GetWidth(); x++)
            {
                for (int y = 0; y < ld.Data.GetHeight(); y++)
                {
                    var obj = ld.Data.GetGridObject(x, y);
                    var s = string.Join(",", obj.ID, obj.Name, obj.WorldPosition, obj.SpritePath);
                    AddToFile(s + Environment.NewLine);
                }
            }
        }

        private static void AddToFile(string v) => FileContents.Append(v);
    }
}
