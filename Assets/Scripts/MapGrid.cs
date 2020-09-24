using UnityEngine;

public class MapGrid : MonoBehaviour
{
    private const float MAP_TILE_SIZE = 10f;
    public Grid<MapTile> BaseMapGrid;
    public int MapWidth;
    public int MapHeight;

    // Start is called before the first frame update
    void Start()
    {
        if(MapHeight > 0 && MapWidth > 0)
        {
            Sprite EmptyTileSprite = Resources.Load<Sprite>(@"Map\EmptyTile");
            BaseMapGrid = new Grid<MapTile>(MapWidth, MapHeight, MAP_TILE_SIZE, new Vector3(-(MapWidth / 2 * MAP_TILE_SIZE),
                -(MapHeight / 2 * MAP_TILE_SIZE), 0), (Grid<MapTile> g, int x, int y) => new MapTile(g, x, y), false);
            for (int x = 0; x < BaseMapGrid.GetWidth(); x++)
            {
                for (int y = 0; y < BaseMapGrid.GetHeight(); y++)
                {
                    BaseMapGrid.GetGridObject(x, y).SetSprite(EmptyTileSprite);
                    BaseMapGrid.GetGridObject(x, y).CreateWorldSprite();
                }
            }
            Debug.Log("Created Map Tiles");
        }
        else
        {
            Debug.LogError("Invalid Map Creation Parameters");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
