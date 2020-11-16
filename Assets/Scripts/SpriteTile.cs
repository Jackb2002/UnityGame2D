using Assets.Scripts;
using CodeMonkey.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SpriteTile
{
    private readonly int x;
    private readonly int y;
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

    public SpriteTile(int x, int y, Sprite sprite,Vector3 position, bool UpdateDatagrid = true)
    {
        this.x = x;
        this.y = y;
        this.sprite = sprite;
        WorldPosition = position;
        sVec = new SerializeableVector3(WorldPosition);
        if (UpdateDatagrid)
        {
            MapGrid.DataGrid.SetGridObject(x, y, new DataTile(
                ItemManager.CurrentSpriteID,
                ItemManager.CurrentSpriteName,
                WorldPosition,
                ItemManager.CurrentSpritePath));
            Dictionary<string, object> data = new Dictionary<string, object>();
            switch (MapGrid.DataGrid.GetGridObject(x, y).ID)
            {
                case 0:
                    //Solid block input data
                    data.Add("solid", true);
                    break;
                case 1:
                    //Damage block input data
                    data.Add("solid", true);
                    data.Add("damage", 10f);
                    break;
                case 2:
                    //Timer block input data
                    data.Add("solid", true);
                    data.Add("timer", 1500f);
                    break;
                case 3:
                    //Goal block input data
                    data.Add("solid", true);
                    data.Add("goal", true);
                    break;
                case 4:
                    //Spawn block input data
                    data.Add("solid", true);
                    data.Add("spawn", true);
                    break;
            }
            MapGrid.DataGrid.GetGridObject(x, y).TileInfo = data;
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