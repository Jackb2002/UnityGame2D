using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Level
{
    public static class Serialization
    {
        private static StringBuilder FileContents = new StringBuilder();

        public static string SaveLevel(string Path, LevelData levelData)
        {
            if (File.Exists(Path))
            {
                Debug.Log("File Already Exists, moving to .old file");
                File.Move(Path, Path + ".old");
                File.Delete(Path); 
            }

            FileStream fs = File.Create(Path);
            AddToFile(levelData.Author + ";");
        }

        private static void AddToFile(string v)
        {
            FileContents.Append(v);
        }
    }
}
