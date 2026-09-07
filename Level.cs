using System;
using System.Collections.Generic;

namespace GADE_POE_HEENA_JANA
{
    public class Level
    {
        public Tile[,] Grid { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public HeroTile Hero { get; private set; }
        public List<GruntTile> Grunts { get; private set; }
        public List<HealthPickupTile> HealthPickups { get; private set; }

        private Random random = new Random();

        public Level(int width, int height)
        {
            Width = width;
            Height = height;
            Grid = new Tile[width, height];
            Grunts = new List<GruntTile>();
            HealthPickups = new List<HealthPickupTile>();

            GenerateLevel();
        }

        private void GenerateLevel()
        {
            // Build border walls and empty inner tiles
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if (x == 0 || x == Width - 1 || y == 0 || y == Height - 1)
                    {
                        Grid[x, y] = new WallTile(new Position(x, y));
                    }
                    else
                    {
                        Grid[x, y] = new EmptyTile(new Position(x, y));
                    }
                }
            }

            // Spawn Hero
            Position heroPos = GetRandomEmptyPosition();
            Hero = new HeroTile(heroPos);
            Grid[heroPos.X, heroPos.Y] = Hero;

            // Spawn Grunts (2-3 enemies)
            int gruntCount = random.Next(2, 4);
            for (int i = 0; i < gruntCount; i++)
            {
                Position gPos = GetRandomEmptyPosition();
                GruntTile grunt = new GruntTile(gPos);
                Grunts.Add(grunt);
                Grid[gPos.X, gPos.Y] = grunt;
            }

            // Spawn Health Pickups (1-2 pickups)
            int pickupCount = random.Next(1, 3);
            for (int i = 0; i < pickupCount; i++)
            {
                Position pPos = GetRandomEmptyPosition();
                HealthPickupTile pickup = new HealthPickupTile(pPos);
                HealthPickups.Add(pickup);
                Grid[pPos.X, pPos.Y] = pickup;
            }
        }

        public Position GetRandomEmptyPosition()
        {
            int x, y;
            do
            {
                x = random.Next(1, Width - 1);
                y = random.Next(1, Height - 1);
            } while (!(Grid[x, y] is EmptyTile));

            return new Position(x, y);
        }

        public bool IsTileEmpty(Position pos)
        {
            return Grid[pos.X, pos.Y] is EmptyTile;
        }

        public void MoveTile(Tile tile, Position newPos)
        {
            Grid[tile.Position.X, tile.Position.Y] = new EmptyTile(tile.Position);
            tile.Position = newPos;
            Grid[newPos.X, newPos.Y] = tile;
        }
    }
}
