using CodeMonkey.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelBuilderManager : MonoBehaviour
{
    public GameObject BuilderCanvas;
    public GameObject PauseCanvas;

    private void Start()
    {
        BuilderCanvas.SetActive(true);
        PauseCanvas.SetActive(false);
    }
    public void SwitchMenus()
    {
        BuilderCanvas.SetActive(!BuilderCanvas.activeSelf); // swap the menus over
        PauseCanvas.SetActive(!BuilderCanvas.activeSelf);
    }

    public void PlayLevel(bool Online = false)
    {
        if (GameObject.Find("Spawn") == null || GameObject.Find("Goal") == null)
        {
            var location = new Vector3(-50, 0);
            UtilsClass.CreateWorldTextPopup(null, "You must have at least one goal " +
                "and one spawn to play a level!",
    location, 50, Color.red, location, 2);
            return;
        }
        if (Online)
        {
            throw new System.NotImplementedException();
        }
        else
        {
            SaveBuilderdataTmp();
            Grid<Assets.Scripts.DataTile> data = MapGrid.DataGrid;

            Vector3 MapMidpoint = new Vector3(
                -data.GetWidth() * MapGrid.MAP_TILE_SIZE / 2,
                -data.GetHeight() * MapGrid.MAP_TILE_SIZE / 2);

            GameObject MAP = new GameObject("Map");
            DontDestroyOnLoad(MAP);

            for (int x = 0; x < data.GetWidth(); x++)
            {
                for (int y = 0; y < data.GetHeight(); y++)
                {
                    var obj = data.GetGridObject(x, y);
                    // All below 0 are non-level blocks like the 'Empty' block
                    if (obj.ID >= 0)
                    {
                        GameObject tmp = new GameObject(obj.Name);
                        foreach (var tileData in obj.TileInfo)
                        {
                            var sr = tmp.AddComponent<SpriteRenderer>();
                            Debug.Log("Creating sprite data for object ID: " + data.GetGridObject(x, y).ID);
                            sr.sprite = MapGrid.SpriteGrid.GetGridObject(x, y).sprite;
                            sr.color = Color.white;
                            sr.sortingOrder = 0;
                            switch (tileData.Key)
                            {
                                case "solid":
                                    tmp.AddComponent<BoxCollider2D>();
                                    tmp.GetComponent<BoxCollider2D>().isTrigger = !(bool)tileData.Value;
                                    break;
                                case "damage":
                                    tmp.AddComponent<DamageBlock>();
                                    tmp.GetComponent<DamageBlock>().DPS = (float)tileData.Value;
                                    break;
                                case "timer":
                                    tmp.AddComponent<TimerBlock>();
                                    tmp.GetComponent<TimerBlock>().TickTime = (float)tileData.Value;
                                    break;
                                case "spawn":
                                    tmp.AddComponent<SpawnBlock>();
                                    break;
                                case "goal":
                                    tmp.AddComponent<GoalBlock>();
                                    break;
                            }
                        }
                        tmp.transform.parent = MAP.transform; // make it a child of the map
                    }
                }
            }

            SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
        }
    }

    private void SaveBuilderdataTmp()
    {
        const string dir = @"Tmp\";
        if (Directory.Exists(dir))
        {
            Directory.Delete(dir, true);
        }
        Directory.CreateDirectory(dir);
        Debug.Log("Serializing Builder Data To " + Directory.GetCurrentDirectory());
        TmpSaveBuilderData(MapGrid.DataGrid, dir+"tiledat.tmpdat");
    }

    private void TmpSaveBuilderData<T>(Grid<T> grid,string fName)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = File.Create(fName);
        bf.Serialize(fs, grid);
        fs.Close();
        fs.Dispose();
    }
}
