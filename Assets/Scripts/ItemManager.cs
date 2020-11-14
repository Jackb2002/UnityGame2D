using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class ItemManager : MonoBehaviour
    {
        public static string CurrentSpritePath { get; private set; }
        public static int CurrentSpriteID { get; private set; }
        public static string CurrentSpriteName { get; private set; }
        public static Sprite CurrentSprite { get; private set; }

        public const string EmptySprite = @"Map\EmptyTile";
        public const string HoverSprite = @"Map\HoverTile";
        public static void SetSprite(string SpritePath, int TileID, string Name)
        {
            if (Resources.Load<Sprite>(SpritePath) != null)
            {
                CurrentSpriteID = TileID;
                CurrentSpritePath = SpritePath;
                CurrentSpriteName = Name;
                CurrentSprite = Resources.Load<Sprite>(CurrentSpritePath);
                Debug.Log("Building material selected " + CurrentSpriteName);
            }
            else
            {
                Debug.LogError("Tried to assign empty sprite to CURRENT " + $"Path:{SpritePath} ID:{TileID}");
            }
        }

        /// <summary>
        /// Makes it quicker for me to select current sprite being used. just a wrapper essentially
        /// </summary>
        /// <param name="Name"></param>
        public static void SelectBlock(string Name)
        {
            switch (Name)
            {
                case "Empty":
                    SetSprite(@"Map\EmptyTile", -1, Name);
                    break;
                case "Solid":
                    SetSprite(@"Map\SolidTile", 0, Name);
                    break;
                case "Damage":
                    SetSprite(@"Map\DamageTile", 1, Name);
                    break;
                case "Timer":
                    SetSprite(@"Map\TimerTile", 2, Name);
                    break;
                default:
                    Debug.LogWarning("Unrecognised Block Selection " + Name);
                    break;
            }
        }

        /// <summary>
        /// Button press methods need to be non-static so just use this to call it 
        /// </summary>
        /// <param name="Name"></param>
        public void PressMaterialBtn(string Name) => SelectBlock(Name);
    }
}
