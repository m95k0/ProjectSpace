namespace ProjectSpace.Domain.Modules
{
    using ProjectSpace.Domain.Common;

    public class ModuleInstance
    {
        public ModuleDefinition Definition { get; }

        public GridPosition Position { get; private set; }

        public int CurrentHealth { get; private set; }

        public bool IsDestroyed => CurrentHealth <= 0;

        public void TakeDamage(int amount)
        {
            if (amount <= 0)
            {
                return;
            }

            CurrentHealth -= amount;

            if (CurrentHealth < 0)
            {
                CurrentHealth = 0;
            }
        }
        public void Repair(int amount)
        {
            if (amount <= 0)
            {
                return;
            }

            CurrentHealth += amount;

            if (CurrentHealth > Definition.MaxHealth)
            {
                CurrentHealth = Definition.MaxHealth;
            }
        }
        internal void MoveTo(GridPosition position)
        {
            Position = position;
        }

        public ModuleInstance(ModuleDefinition definition, GridPosition position)
        {
            Definition = definition;
            Position = position;
            CurrentHealth = definition.MaxHealth;
        }
    }
}