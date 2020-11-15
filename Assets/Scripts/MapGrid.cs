using Assets.Scripts;
using CodeMonkey.Utils;
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
    private void Start()
    {
        Cursor.visible = false;
        if (MapHeight > 0 && MapWidth > 0)
        {
            ItemManager.SelectBlock("Empty");
            Sprite EmptySprite = Resources.Load<Sprite>(ItemManager.CurrentSpritePath);
            SpriteGrid = new Grid<SpriteTile>(MapWidth, MapHeight, MAP_TILE_SIZE, new Vector3(-MapWidth / 2, -MapHeight / 2), false);
            for (int x = 0; x < SpriteGrid.GetWidth(); x++)
            {
                for (int y = 0; y < SpriteGrid.GetHeight(); y++)
                {
                    //fill empty but dont update datagrid as its not been created yet, do manually below
                    SpriteGrid.SetGridObject(x, y, new SpriteTile(x, y, EmptySprite, false));
                }
            }

            DataGrid = new Grid<DataTile>(MapWidth, MapHeight, MAP_TILE_SIZE, new Vector3(-MapWidth / 2, -MapHeight / 2), false);
            for (int x = 0; x < SpriteGrid.GetWidth(); x++)
            {
                for (int y = 0; y < SpriteGrid.GetHeight(); y++)
                {
                    //Creating data grid
                    DataGrid.SetGridObject(x, y, new DataTile(ItemManager.CurrentSpriteID, ItemManager.CurrentSpriteName)); // fill with emptiness
                    SpriteGrid.GetGridObject(x, y).Render();
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
    private void Update()
    {
        if (!CheckIfHoveringOverUI())
        {
            Cursor.visible = false;
            Vector3 pos = UtilsClass.GetMouseWorldPosition();
            if (Input.GetMouseButton(0))
            {
                TileUpdate(pos, TileUpdateMode.Update);
            }
            TileUpdate(pos, TileUpdateMode.Hover);
        }
        else
        {
            Cursor.visible = true;
        }
    }

    private void TileUpdate(Vector3 pos, TileUpdateMode Mode)
    {

        SpriteGrid.GetIndexFromPosition(pos, out int x, out int y);
        switch (Mode)
        {
            case TileUpdateMode.Hover:
                Destroy(HoverGO);
                HoverGO = UtilsClass.CreateWorldSprite("Hover Box", ItemManager.HoverSprite, SpriteGrid.GetWorldPosition(x, y), Vector3.one, 5, Color.white);
                break;
            case TileUpdateMode.Update:
                Destroy(SpriteGrid.GetGridObject(x, y).SpriteObject);
                SpriteGrid.SetGridObject(x, y, new SpriteTile(x, y, ItemManager.CurrentSprite));
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
