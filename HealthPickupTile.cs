namespace GADE_POE_HEENA_JANA
{
    public class HealthPickupTile : Tile
    {
        public int HealAmount { get; private set; }

        public HealthPickupTile(Position position, int healAmount = 10) : base(position)
        {
            HealAmount = healAmount;
        }

        public override TileType GetTileType()
        {
            return TileType.HealthPickup;
        }

        public override string ToString()
        {
            return "+";
        }
    }
}
