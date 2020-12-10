using Assets.Scripts;
using CodeMonkey.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class SpriteTile
{
    public readonly int x;
    public readonly int y;
    [NonSerialized]
    public readonly Sprite sprite;
    [NonSerialized]
    public Vector3 WorldPosition;
    [SerializeField]
    private SerializeableVector3 sVec;
    public GameObject SpriteObject { get; private set; }

    public SpriteTile(int x, int y, Sprite sprite, SerializeableVector3 position, bool UpdateDatagrid = true) : this(x, y, sprite, position.GetVector(), UpdateDatagrid)
    {
        Debug.Log("Created Deserialized Grid Object");
    } // to allow the use of the serializable vec3

    public SpriteTile(int x, int y, Sprite sprite, Vector3 position, bool UpdateDatagrid = true)
    {
        this.x = x;
        this.y = y;
        this.sprite = sprite;
        WorldPosition = position;
        sVec = new SerializeableVector3(WorldPosition);
        if (UpdateDatagrid)
        {
            Map.DataGrid.SetGridObject(x, y, new DataTile(
                ItemManager.CurrentSpriteID,
                ItemManager.CurrentSpriteName,
                WorldPosition,
                x,
                y,
                ItemManager.CurrentSpritePath));
            
            DataTile.AssignTileInfo(Map.DataGrid.GetGridObject(x,y));
        } // update data grid as well
    }

    internal void Render()
    {
        SpriteObject = UtilsClass.CreateWorldSprite(
            Map.DataGrid.GetGridObject(x, y).Name,
            sprite,
            Map.DataGrid.GetWorldPosition(x, y),
            Vector3.one,
            0,
            Color.white);
        SpriteObject.transform.parent = GameObject.Find("LevelBuilder")?.transform;
    }

    ~SpriteTile()
    {
        UnityEngine.Object.Destroy(SpriteObject);
    }

    internal static Grid<SpriteTile> InitializeGrid(Grid<SpriteTile> g, List<DataTile> d)
    {
        for (int x = 0; x < g.GetWidth(); x++)
        {
            for (int y = 0; y < g.GetHeight(); y++)
            {
                var data = d.Single(item => item.x == x && item.y == y);
                var spriteTile = new SpriteTile(x, y, Resources.Load<Sprite>(data.SpritePath), data.WorldPosition, false);
                g.SetGridObject(x, y, spriteTile);
            }
        }
        return g;
    }
}