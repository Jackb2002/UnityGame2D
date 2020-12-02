using System;

namespace Assets.Scripts.Level
{
    public class LevelData
    {
        public Grid<DataTile> Data;
        public Grid<SpriteTile> Sprites;
        public string Author;
        public int maxPlayers;

        public LevelData(Grid<DataTile> data, Grid<SpriteTile> sprites, string author, int maxPlayers)
        {
            Data = data ?? throw new ArgumentNullException(nameof(data));
            Sprites = sprites ?? throw new ArgumentNullException(nameof(sprites));
            Author = author ?? throw new ArgumentNullException(nameof(author));
            this.maxPlayers = maxPlayers;
        }
    }
}
