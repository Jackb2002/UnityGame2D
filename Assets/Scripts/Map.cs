﻿using Assets.Scripts;
using Assets.Scripts.Level;
using CodeMonkey.Utils;
using System;
using System.Windows.Forms;
using UnityEngine;
using UnityEngine.EventSystems;

public partial class Map : MonoBehaviour
{
    public const float MAP_TILE_SIZE = 10f;
    public static Grid<SpriteTile> SpriteGrid;
    public static Grid<DataTile> DataGrid;
    public int MapWidth;
    public int MapHeight;
    private GameObject HoverGO;

    public static bool LoadingFromSave = false;

    // Start is called before the first frame update
    private void Awake()
    {
        if (!LoadingFromSave)
        {
            if (MapHeight > 0 && MapWidth > 0)
            {
                ItemManager.SelectBlock("Empty");
                Sprite EmptySprite = Resources.Load<Sprite>(ItemManager.CurrentSpritePath);
                Vector3 Origin = new Vector3(-MapWidth * MAP_TILE_SIZE / 2, -MapHeight * MAP_TILE_SIZE / 2);
                SpriteGrid = new Grid<SpriteTile>(MapWidth, MapHeight, MAP_TILE_SIZE, Origin, false);
                for (int x = 0; x < SpriteGrid.GetWidth(); x++)
                {
                    for (int y = 0; y < SpriteGrid.GetHeight(); y++)
                    {
                        //fill empty but dont update datagrid as its not been created yet, do manually below
                        SpriteGrid.SetGridObject(x, y, new SpriteTile(x, y, EmptySprite, SpriteGrid.GetWorldPosition(x, y), false));
                    }
                }

                DataGrid = new Grid<DataTile>(MapWidth, MapHeight, MAP_TILE_SIZE, Origin, false);
                for (int x = 0; x < SpriteGrid.GetWidth(); x++)
                {
                    for (int y = 0; y < SpriteGrid.GetHeight(); y++)
                    {
                        //Creating data grid
                        DataGrid.SetGridObject(x, y, new DataTile(ItemManager.CurrentSpriteID, ItemManager.CurrentSpriteName,
                            SpriteGrid.GetWorldPosition(x, y), x, y, ItemManager.CurrentSpritePath)); // fill with emptiness

                        SpriteGrid.GetGridObject(x, y).Render();
                    }
                }


                ItemManager.SelectBlock("Solid"); // So user cant place empty
            }
        }
        else
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Level Files (*.level)|*.level";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                var ld = LevelIO.LoadLevel(ofd.FileName);
                for (int x = 0; x < SpriteGrid.GetWidth(); x++)
                {
                    for (int y = 0; y < SpriteGrid.GetHeight(); y++)
                    {
                        Destroy(SpriteGrid.GetGridObject(x, y).SpriteObject);
                    }
                }
                SpriteGrid = ld.Sprites;
                DataGrid = ld.Data;
                for (int x = 0; x < SpriteGrid.GetWidth(); x++)
                {
                    for (int y = 0; y < SpriteGrid.GetHeight(); y++)
                    {
                        SpriteGrid.GetGridObject(x, y).Render();
                    }
                }
            }
            else
            {
                LoadingFromSave = false;
                Awake();
            }
        }
        UnityEngine.Cursor.visible = false;
    }

    private void OnDestroy()
    {
        UnityEngine.Cursor.visible = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!CheckIfHoveringOverUI())
        {
            UnityEngine.Cursor.visible = false;
            Vector3 pos = UtilsClass.GetMouseWorldPosition();
            if (Input.GetMouseButton(0))
            {
                TileUpdate(pos, TileUpdateMode.Update);
            }
            TileUpdate(pos, TileUpdateMode.Hover);
        }
        else
        {
            UnityEngine.Cursor.visible = true;
        }
    }

    private void TileUpdate(Vector3 pos, TileUpdateMode Mode)
    {
        if (pos == null)
        {
            //cant update tile if there is a null position so return, dont know how it was happening but it worked as a fix, created issue on git 
            return;
        }
        SpriteGrid.GetIndexFromPosition(pos, out int x, out int y);
        switch (Mode)
        {
            case TileUpdateMode.Hover:
                Destroy(HoverGO);
                HoverGO = UtilsClass.CreateWorldSprite("Hover Box", ItemManager.HoverSprite, SpriteGrid.GetWorldPosition(x, y), Vector3.one, 5, Color.white);
                HoverGO.transform.parent = GameObject.Find("LevelBuilder")?.transform;
                break;
            case TileUpdateMode.Update:
                if (GameObject.Find("Spawn") != null && ItemManager.CurrentSpriteName == "Spawn")
                {
                    Debug.LogWarning("Tried to place multiple spawn blocks");
                    UtilsClass.CreateWorldTextPopup(null, "You cant have more than one spawn",
                        Vector3.one, 50, Color.red, Vector3.zero, 2);
                    return;
                }
                Destroy(SpriteGrid.GetGridObject(x, y).SpriteObject);
                SpriteGrid.SetGridObject(x, y, new SpriteTile(x, y, ItemManager.CurrentSprite, SpriteGrid.GetWorldPosition(x, y)));
                SpriteGrid.GetGridObject(x, y).Render();
                break;
        }
    }

    private bool CheckIfHoveringOverUI()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return true;
        }
        return false;
    }
}