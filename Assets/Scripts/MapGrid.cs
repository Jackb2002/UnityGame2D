using Assets.Scripts;
using CodeMonkey.Utils;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapGrid : MonoBehaviour
{
    private const float MAP_TILE_SIZE = 10f;
    public static Grid<SpriteTile> SpriteGrid;
    public static Grid<DataTile> DataGrid;
    public int MapWidth;
    public int MapHeight;

    // Start is called before the first frame update
    void Start()
    {
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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!CheckIfHoveringOverUI())
            {
                var pos = UtilsClass.GetMouseWorldPosition();
                UpdateClickedTile(pos);
            }
        }
    }

    private void UpdateClickedTile(Vector3 pos)
    {
        int x;
        int y;

        SpriteGrid.GetIndexFromPosition(pos, out x, out y);
        //assume data tile and sprite tile are in sync
        SpriteGrid.SetGridObject(pos, new SpriteTile(x, y, ItemManager.CurrentSprite));
        DataGrid.SetGridObject(pos, new DataTile(ItemManager.CurrentSpriteID, ItemManager.CurrentSpriteName));
    }

    private bool CheckIfHoveringOverUI()
    {
        EventSystem eventSystem = EventSystem.current;
        if (EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("Clicked on the UI");
            return true;
        }
        return false;
    }
}
