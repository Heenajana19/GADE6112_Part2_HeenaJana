using System;

namespace GADE_POE_HEENA_JANA
{
    public class GameEngine
    {
        public Level CurrentLevel { get; private set; }

        public GameEngine(int width, int height)
        {
            CurrentLevel = new Level(width, height);
        }

        public void MoveHero(Direction direction)
        {
            if (CurrentLevel.Hero.IsDead) return;

            Position targetPos = GetTargetPosition(CurrentLevel.Hero.Position, direction);
            Tile targetTile = CurrentLevel.Grid[targetPos.X, targetPos.Y];

            // Handle step onto empty space
            if (targetTile is EmptyTile)
            {
                CurrentLevel.MoveTile(CurrentLevel.Hero, targetPos);
            }
            // Handle Pickup interaction
            else if (targetTile is HealthPickupTile pickup)
            {
                CurrentLevel.Hero.Heal(pickup.HealAmount);
                CurrentLevel.HealthPickups.Remove(pickup);
                CurrentLevel.MoveTile(CurrentLevel.Hero, targetPos);
            }
            // Handle attack on Grunt
            else if (targetTile is GruntTile grunt)
            {
                CurrentLevel.Hero.Attack(grunt);
                if (grunt.IsDead)
                {
                    CurrentLevel.Grunts.Remove(grunt);
                    CurrentLevel.Grid[targetPos.X, targetPos.Y] = new EmptyTile(targetPos);
                }
            }

            // Process enemy turn following Hero's move
            ProcessEnemyTurns();
        }

        private void ProcessEnemyTurns()
        {
            foreach (var grunt in CurrentLevel.Grunts.ToArray())
            {
                if (grunt.IsDead) continue;

                // Check if adjacent to Hero
                if (IsAdjacent(grunt.Position, CurrentLevel.Hero.Position))
                {
                    grunt.Attack(CurrentLevel.Hero);
                }
                else
                {
                    Position nextPos = grunt.GetNextMovePosition(CurrentLevel.Hero.Position, CurrentLevel);
                    if (CurrentLevel.IsTileEmpty(nextPos))
                    {
                        CurrentLevel.MoveTile(grunt, nextPos);
                    }
                }
            }
        }

        private bool IsAdjacent(Position p1, Position p2)
        {
            int dx = Math.Abs(p1.X - p2.X);
            int dy = Math.Abs(p1.Y - p2.Y);
            return (dx + dy) == 1;
        }

        private Position GetTargetPosition(Position current, Direction dir)
        {
            switch (dir)
            {
                case Direction.Up: return new Position(current.X, current.Y - 1);
                case Direction.Down: return new Position(current.X, current.Y + 1);
                case Direction.Left: return new Position(current.X - 1, current.Y);
                case Direction.Right: return new Position(current.X + 1, current.Y);
                default: return current;
            }
        }
    }
}
