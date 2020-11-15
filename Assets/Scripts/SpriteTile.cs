using Assets.Scripts;
using CodeMonkey.Utils;
using UnityEngine;

public class SpriteTile
{
    private readonly int x;
    private readonly int y;
    private readonly Sprite sprite;
    public GameObject SpriteObject { get; private set; }
    public SpriteTile(int x, int y, Sprite sprite, bool UpdateDatagrid = true)
    {
        this.x = x;
        this.y = y;
        this.sprite = sprite;
        if (UpdateDatagrid)
        {
            MapGrid.DataGrid.SetGridObject(x, y, new DataTile(
                ItemManager.CurrentSpriteID,
                ItemManager.CurrentSpriteName));
        } // update data grid as well
    }

    internal void Render()
    {
        SpriteObject = UtilsClass.CreateWorldSprite(
            MapGrid.DataGrid.GetGridObject(x, y).Name,
            sprite,
            MapGrid.DataGrid.GetWorldPosition(x, y),
            Vector3.one,
            0,
            Color.white);
    }

    ~SpriteTile()
    {
        UnityEngine.Object.Destroy(SpriteObject);
    }
}