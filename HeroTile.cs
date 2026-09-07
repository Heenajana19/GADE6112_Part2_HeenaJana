namespace GADE_POE_HEENA_JANA
{
    public class HeroTile : CharacterTile
    {
        public HeroTile(Position position) : base(position, 40, 2)
        {
        }

        public override TileType GetTileType()
        {
            return TileType.Hero;
        }

        public override string ToString()
        {
            return "H";
        }
    }
}
