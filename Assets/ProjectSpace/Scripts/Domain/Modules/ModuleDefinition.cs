namespace ProjectSpace.Domain.Modules
{
    using ProjectSpace.Domain.Common;

    public class ModuleDefinition
    {
        public string Name { get; }

        public GridSize Size { get; }

        public int MaxHealth { get; }

        public ModuleDefinition(string name, GridSize size, int maxHealth)
        {
            Name = name;
            Size = size;
            MaxHealth = maxHealth;
        }
    }
}