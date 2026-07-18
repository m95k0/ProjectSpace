namespace ProjectSpace.Domain.Ships
{
    using System.Collections.Generic;
    using ProjectSpace.Domain.Common;
    using ProjectSpace.Domain.Modules;

    public sealed class Ship
    {
        private readonly ShipGrid _grid;
        private readonly List<ModuleInstance> _modules;

        public GridSize Size => _grid.Size;

        public IReadOnlyList<ModuleInstance> Modules => _modules;

        public Ship(GridSize size)
        {
            _grid = new ShipGrid(size);
            _modules = new List<ModuleInstance>();
        }

        public bool TryAddModule(
            ModuleDefinition definition,
            GridPosition position)
        {
            if (!_grid.CanPlaceModule(definition, position))
            {
                return false;
            }

            ModuleInstance module = new ModuleInstance(
                definition,
                position);

            _grid.PlaceModule(module);

            _modules.Add(module);

            return true;
        }

        public bool RemoveModule(ModuleInstance module)
        {
            if (!_modules.Contains(module))
            {
                return false;
            }

            _grid.RemoveModule(module);

            _modules.Remove(module);

            return true;
        }

        public ModuleInstance? GetModuleAt(GridPosition position)
        {
            return _grid.GetModuleAt(position);
        }
    }
}