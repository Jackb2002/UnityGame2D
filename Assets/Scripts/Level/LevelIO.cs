using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Level
{
    public static class LevelIO
    {
        private static readonly StringBuilder SaveFileContents = new StringBuilder(); // In order to build a string for the contents of the save file

        public static string SaveLevel(LevelData ld, string Path)
        {
            SaveFileContents.Clear(); // ensure its cleared beforehand
            AddToFile(ld.Author + Environment.NewLine);
            AddToFile(ld.Name + Environment.NewLine);
            AddToFile(ld.maxPlayers + Environment.NewLine);
            AddToFile("DATA" + Environment.NewLine);
            for (int x = 0; x < ld.Data.GetWidth(); x++)
            {
                for (int y = 0; y < ld.Data.GetHeight(); y++)
                {
                    DataTile obj = ld.Data.GetGridObject(x, y); // get the coresponding data tile for the loop vars
                    string s = string.Join(":", obj.ID, obj.Name, obj.WorldPosition, obj.SpritePath, obj.x, obj.y); // create a colon seperated version of classs fields
                    AddToFile(s + Environment.NewLine); // add it to the file
                }
            }
            AddToFile("END" + Environment.NewLine);
            File.Create(Path).Close(); // create the file and close the stream
            File.WriteAllText(Path, SaveFileContents.ToString()); // write the string builder contents
            Debug.Log("Written to level file " + Path + " Content of length " + SaveFileContents.Length);
            return Path;
        }

        public static LevelData LoadLevel(string Path)
        {
            string[] Lines;
            if (File.Exists(Path))
            {
                Lines = File.ReadAllLines(Path);
            }
            else
            {
                Debug.LogError("Could not find file at path");
                return default;
            }
            string Author = Lines[0].Trim();
            string Name = Lines[1].Trim();
            int maxPlayers = int.Parse(Lines[2].Trim());
            List<DataTile> DataItems = new List<DataTile>();
            for (int lineIndex = 4; lineIndex < Lines.Length; lineIndex++)
            {
                if (Lines[lineIndex].Trim() == "END")
                {
                    break;
                }
                string[] dataObject = Lines[lineIndex].Trim().Split(':');
                DataTile tile = GetTile(dataObject);
                DataItems.Add(tile);
            }
            int[] dims;
            dims = GetDimensions(DataItems);
            Vector3 Origin = new Vector3(-dims[0] * Map.MAP_TILE_SIZE / 2, -dims[1] * Map.MAP_TILE_SIZE / 2);

            Grid<DataTile> DataGrid = new Grid<DataTile>(dims[0], dims[1], Map.MAP_TILE_SIZE, Origin, false);
            Grid<SpriteTile> SpriteGrid = new Grid<SpriteTile>(dims[0], dims[1], Map.MAP_TILE_SIZE, Origin, false);

            DataTile.InitializeGrid(DataGrid, DataItems);
            SpriteTile.InitializeGrid(SpriteGrid, DataItems);

            return new LevelData(Name, DataGrid, SpriteGrid, Author, maxPlayers);
        }

        /// <summary>
        /// Use linq to get the dimensions quickly
        /// </summary>
        /// <param name="dataItems"></param>
        /// <returns></returns>
        private static int[] GetDimensions(List<DataTile> dataItems)
        {
            int[] dims = new int[2];
            dims[0] = dataItems.Max(item => item.x);
            dims[1] = dataItems.Max(item => item.y);
            Debug.Log($"Editor level found with dimensions {dims[0]}:{dims[1]}");
            return dims;
        }


        /// <summary>
        /// converts the string from the file back to the data object with some string manipulation. Assumes file is intact  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static DataTile GetTile(string[] data)
        {
            string[] vals = data[2].Replace("(", string.Empty).Replace(")", string.Empty).Split(',');
            Vector3 worldPos = new Vector3(float.Parse(vals[0]),
                            float.Parse(vals[1]),
                            float.Parse(vals[2]));
            DataTile tile = new DataTile(int.Parse(data[0]), data[1], worldPos, int.Parse(data[4]), int.Parse(data[5]), data[3]);
            if (tile.Name != "Empty")
            {
                DataTile.AssignTileInfo(tile);
            }
            return tile;
        }

        private static void AddToFile(string v)
        {
            SaveFileContents.Append(v);
        }
    }
}

