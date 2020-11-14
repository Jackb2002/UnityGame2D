using CodeMonkey.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class SpriteTile
{
    private int x;
    private int y;
    private Sprite sprite;
    public GameObject SpriteObject { get; private set; }
    public SpriteTile(int x, int y, Sprite sprite)
    {
        this.x = x;
        this.y = y;
        this.sprite = sprite;
    }

    public void UpdateGameobject()
    {
        if(SpriteObject != null)
        {
            SpriteObject = null;
        }
        UtilsClass.CreateWorldSprite(sprite.name, sprite, MapGrid.SpriteGrid.GetWorldPosition(x, y), Vector3.one, 0, Color.white);
    }
}