namespace ProjectSpace.Domain.Modules
{
    using ProjectSpace.Domain.Common;

    public class ModuleInstance
    {
        public ModuleDefinition Definition { get; }

        public GridPosition Position { get; private set; }

        public int CurrentHealth { get; private set; }

        public ModuleInstance(ModuleDefinition definition, GridPosition position)
        {
            Definition = definition;
            Position = position;
            CurrentHealth = definition.MaxHealth;
        }
    }
}