using Assets.Scripts;
using CodeMonkey.Utils;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public partial class MapGrid : MonoBehaviour
{
    private const float MAP_TILE_SIZE = 10f;
    public static Grid<SpriteTile> SpriteGrid;
    public static Grid<DataTile> DataGrid;
    public int MapWidth;
    public int MapHeight;
    private GameObject HoverGO;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        if(MapHeight > 0 && MapWidth > 0)
        {
            ItemManager.SelectBlock("Empty");
            Sprite EmptySprite = Resources.Load<Sprite>(ItemManager.CurrentSpritePath);
            SpriteGrid = new Grid<SpriteTile>(MapWidth,MapHeight,MAP_TILE_SIZE,new Vector3(-MapWidth/2,-MapHeight/2),false);
            for (int spriteX = 0; spriteX < SpriteGrid.GetWidth(); spriteX++)
            {
                for (int spriteY = 0; spriteY < SpriteGrid.GetHeight(); spriteY++)
                {
                    SpriteGrid.SetGridObject(spriteX, spriteY, new SpriteTile(spriteX,spriteY,EmptySprite)); // fill with emptiness
                    SpriteGrid.GetGridObject(spriteX, spriteY).UpdateGameobject();
                }
            } 

            DataGrid = new Grid<DataTile>(MapWidth, MapHeight, MAP_TILE_SIZE, new Vector3(-MapWidth / 2, -MapHeight / 2),false);
            for (int spriteX = 0; spriteX < SpriteGrid.GetWidth(); spriteX++)
            {
                for (int spriteY = 0; spriteY < SpriteGrid.GetHeight(); spriteY++)
                {
                    DataGrid.SetGridObject(spriteX, spriteY, new DataTile(ItemManager.CurrentSpriteID,ItemManager.CurrentSpriteName)); // fill with emptiness
                }
            }

            ItemManager.SelectBlock("Solid"); // So user cant place empty
        }
        else
        {
            Debug.LogError("Invalid Map Creation Parameters");
        }
    }

    private void OnDestroy()
    {
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!CheckIfHoveringOverUI())
        {
            var pos = UtilsClass.GetMouseWorldPosition();
            if (Input.GetMouseButtonDown(0))
            {
                UpdateCurrentTile(pos,TileUpdateMode.Place);
            }
            else if (Input.GetMouseButtonDown(1))
            {
                UpdateCurrentTile(pos, TileUpdateMode.Delete);
            }
            else
            {
                UpdateCurrentTile(pos, TileUpdateMode.Hover);
            }
        }
    }

    private void UpdateCurrentTile(Vector3 pos, TileUpdateMode Mode)
    {
        int x;
        int y;

        SpriteGrid.GetIndexFromPosition(pos, out x, out y);
        switch (Mode)
        {
            case TileUpdateMode.Hover:
                Destroy(HoverGO);
                HoverGO = UtilsClass.CreateWorldSprite("Hover Box", Resources.Load<Sprite>(ItemManager.HoverSprite), SpriteGrid.GetWorldPosition(x,y), Vector3.one, 5, Color.white);
                break;
            case TileUpdateMode.Place:
                Debug.Log("Tile: " + $"{x}:{y} Being set to {ItemManager.CurrentSpriteName}");
                //assume data tile and sprite tile are in sync
                SpriteGrid.SetGridObject(x, y, new SpriteTile(x, y, ItemManager.CurrentSprite));
                DataGrid.SetGridObject(x, y, new DataTile(ItemManager.CurrentSpriteID, ItemManager.CurrentSpriteName));
                SpriteGrid.GetGridObject(x, y).UpdateGameobject();
                break;
            case TileUpdateMode.Delete:
                Debug.Log("Tile: " + $"{x}:{y} Being set to Empty");
                //assume data tile and sprite tile are in sync
                Sprite EmptySprite = Resources.Load<Sprite>(ItemManager.EmptySprite);
                SpriteGrid.SetGridObject(x, y, new SpriteTile(x, y, EmptySprite));
                DataGrid.SetGridObject(x, y, new DataTile(-1, "Empty"));
                SpriteGrid.GetGridObject(x, y).UpdateGameobject();
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
