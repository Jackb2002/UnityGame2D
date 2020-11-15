using UnityEngine;

namespace Assets.Scripts
{
    public class ItemManager : MonoBehaviour
    {
        public GameObject TextObject;
        private readonly bool Buildmode = true;

        public static string CurrentSpritePath { get; private set; }
        public static int CurrentSpriteID { get; private set; }
        public static string CurrentSpriteName { get; private set; }
        public static Sprite CurrentSprite { get; private set; }

        public static Sprite EmptySprite { get; private set; }
        public static Sprite HoverSprite { get; private set; }

        private void Start()
        {
            EmptySprite = Resources.Load<Sprite>(@"Map\EmptyTile");
            HoverSprite = Resources.Load<Sprite>(@"Map\HoverTile");
        }

        public static void SetSprite(string SpritePath, int TileID, string Name)
        {
            Debug.Log($"Setting sprite data " + $"{SpritePath} {TileID} {Name}");
            if (Resources.Load<Sprite>(SpritePath) != null)
            {
                CurrentSpriteID = TileID;
                CurrentSpritePath = SpritePath;
                CurrentSpriteName = Name;
                CurrentSprite = Resources.Load<Sprite>(CurrentSpritePath);
                Debug.Log("Building material selected " + CurrentSpriteName + $" with PPU:{CurrentSprite.pixelsPerUnit}");
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
            Debug.Log(Name);
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
        public void PressMaterialBtn(string Name)
        {
            SelectBlock(Name);
        }
    }
}
