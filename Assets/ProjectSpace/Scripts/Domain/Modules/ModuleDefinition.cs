namespace ProjectSpace.Domain.Modules
{
    using ProjectSpace.Domain.Common;
    using System;

    public class ModuleDefinition
    {
        public ModuleType Type { get; }

        public string Name { get; }

        public GridSize Size { get; }

        public int MaxHealth { get; }

        public ModuleDefinition(ModuleType type, string name, GridSize size, int maxHealth)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Module name cannot be empty.", nameof(name));
            }

            if (maxHealth <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(maxHealth),
                    "Maximum health must be greater than zero.");
            }

            Type = type;
            Name = name;
            Size = size;
            MaxHealth = maxHealth;
        }
    }
}