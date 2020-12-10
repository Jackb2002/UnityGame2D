using System;

namespace Assets.Scripts.Level
{
    public class LevelData
    {
        public string Name;
        public Grid<DataTile> Data;
        public Grid<SpriteTile> Sprites;
        public string Author;
        public int maxPlayers;

        public LevelData(string Name, Grid<DataTile> data, Grid<SpriteTile> sprites, string author, int maxPlayers)
        {
            this.Name = Name ?? throw new ArgumentNullException(nameof(Name));
            Data = data ?? throw new ArgumentNullException(nameof(data));
            Sprites = sprites ?? throw new ArgumentNullException(nameof(sprites));
            Author = author ?? throw new ArgumentNullException(nameof(author));
            this.maxPlayers = maxPlayers;
        }
    }
}
