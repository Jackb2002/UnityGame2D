using CodeMonkey.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class MapTile
{
    private int x;
    private int y;
    private Grid<MapTile> grid;
    private Sprite sprite;
    private GameObject SpriteObject;
     
    public MapTile(Grid<MapTile> g ,int x, int y)
    {
        this.x = x;
        this.y = y;
        grid = g;
    }

    public void SetSprite(Sprite sprite)
    {
        this.sprite = sprite;
        if(SpriteObject != null)
        {
            SpriteObject.GetComponent<SpriteRenderer>().sprite = sprite;
        }
    }

    public void CreateWorldSprite()
    {
        if(sprite != null)
        {
            var pos = GetCellWorldPosition();
            SpriteObject = UtilsClass.CreateWorldSprite("MapTile", sprite, pos, Vector3.one, 1, Color.white);
            SpriteObject.AddComponent<BoxCollider2D>();
        }
        else if(SpriteObject != null)
        {
            Debug.LogWarning("World Sprite Already Exists For This Tile");
        }
        else
        {
            Debug.LogError("null sprite on tile creation");
        }
    }

    private Vector3 GetCellWorldPosition()
    {
        var pos = grid.GetWorldPosition(x, y);
        pos.Set(pos.x + 5f, pos.y + 5f, pos.z);
        return pos;
    }
}