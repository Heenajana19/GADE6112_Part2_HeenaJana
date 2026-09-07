namespace GADE_POE_HEENA_JANA
{
    public abstract class CharacterTile : Tile
    {
        public int CurrentHealth { get; set; }
        public int MaxHealth { get; protected set; }
        public int AttackPower { get; protected set; }

        protected CharacterTile(Position position, int health, int attackPower) : base(position)
        {
            MaxHealth = health;
            CurrentHealth = health;
            AttackPower = attackPower;
        }

        public bool IsDead => CurrentHealth <= 0;

        public virtual void TakeDamage(int damage)
        {
            CurrentHealth -= damage;
            if (CurrentHealth < 0)
            {
                CurrentHealth = 0;
            }
        }

        public virtual void Attack(CharacterTile target)
        {
            if (target != null && !target.IsDead)
            {
                target.TakeDamage(this.AttackPower);
            }
        }

        public virtual void Heal(int amount)
        {
            CurrentHealth += amount;
            if (CurrentHealth > MaxHealth)
            {
                CurrentHealth = MaxHealth;
            }
        }
    }
}
