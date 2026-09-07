using System;

namespace GADE_POE_HEENA_JANA
{
    public class GruntTile : CharacterTile
    {
        public GruntTile(Position position) : base(position, 10, 1)
        {
        }

        public override TileType GetTileType()
        {
            return TileType.Grunt;
        }

        // Logic to calculate step towards Hero
        public Position GetNextMovePosition(Position heroPosition, Level level)
        {
            int dx = Math.Sign(heroPosition.X - Position.X);
            int dy = Math.Sign(heroPosition.Y - Position.Y);

            // Try primary horizontal move
            Position targetPos = new Position(Position.X + dx, Position.Y);
            if (dx != 0 && level.IsTileEmpty(targetPos))
            {
                return targetPos;
            }

            // Try primary vertical move
            targetPos = new Position(Position.X, Position.Y + dy);
            if (dy != 0 && level.IsTileEmpty(targetPos))
            {
                return targetPos;
            }

            return Position; // Stay put if blocked
        }

        public override string ToString()
        {
            return "G";
        }
    }
}
