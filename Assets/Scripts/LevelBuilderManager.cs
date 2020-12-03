using Assets.Scripts;
using CodeMonkey.Utils;
using System.Collections.Generic;
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
            Vector3 location = new Vector3(-50, 0);
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
            Grid<DataTile> data = MapGrid.DataGrid;

            Vector3 MapMidpoint = new Vector3(
                -data.GetWidth() * MapGrid.MAP_TILE_SIZE / 2,
                -data.GetHeight() * MapGrid.MAP_TILE_SIZE / 2);

            GameObject MAP = new GameObject("Map");
            GameObject LevelRoot = new GameObject("LEVEL");
            MAP.transform.parent = LevelRoot.transform;
            for (int x = 0; x < data.GetWidth(); x++)
            {
                for (int y = 0; y < data.GetHeight(); y++)
                {
                    DataTile obj = data.GetGridObject(x, y);
                    // All below 0 are non-level blocks like the 'Empty' block
                    if (obj.ID >= 0)
                    {
                        GameObject tmp = new GameObject(obj.Name + obj.GetHashCode().ToString());
                        if (tmp.name.Contains("Spawn"))
                        {
                            tmp.name = "Spawn";
                        }
                        tmp.tag = "Map";
                        foreach (KeyValuePair<string, object> tileData in obj.TileInfo)
                        {
                            if (tmp.GetComponent<SpriteRenderer>() == null)
                            {
                                Debug.LogWarning("No renderer attached creating one");
                                tmp.AddComponent<SpriteRenderer>();
                            }
                            SpriteRenderer sr = tmp.GetComponent<SpriteRenderer>();
                            Debug.Log("Sprite renderer loading in sprite at: " + obj.SpritePath);
                            sr.sprite =
                                Resources.Load<Sprite>(obj.SpritePath);
                            sr.color
                                = Color.white;
                            sr.sortingOrder
                                = 0;
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
                        Debug.Log($"Moving {tmp.name} to {obj.WorldPosition}");
                        tmp.transform.Translate(obj.WorldPosition);
                    }
                }
            }

            SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
            LevelTestManager.LevelBuilder = GameObject.Find("LevelBuilder");
            LevelTestManager.LevelBuilder.SetActive(false);
            MAP.AddComponent<LevelTestManager>();
        }
    }
}
