namespace ProjectSpace.Domain.Ships
{
    using System.Collections.Generic;
    using ProjectSpace.Domain.Common;
    using ProjectSpace.Domain.Modules;

    public sealed class Ship
    {
        private readonly ShipGrid _grid;
        private readonly List<ModuleInstance> _modules;
        private ModuleInstance _core;
        private bool _isDestroyed;

        public ModuleInstance? Core => _core;

        public GridSize Size => _grid.Size;

        public IReadOnlyList<ModuleInstance> Modules => _modules;

        public bool IsDestroyed => _isDestroyed;

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

            if (definition.Type == ModuleType.Core && _core != null)
            {
                return false;
            }

            ModuleInstance module = new ModuleInstance(
                definition,
                position);

            _grid.PlaceModule(module);

            _modules.Add(module);

            if (definition.Type == ModuleType.Core)
            {
                _core = module;
            }

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

        public bool ApplyDamage(
        GridPosition position,
        int amount)
        {
            ModuleInstance? module = _grid.GetModuleAt(position);

            if (module == null)
            {
                return false;
            }

            module.TakeDamage(amount);

            if (module == _core && module.IsDestroyed)
            {
                _isDestroyed = true;
            }

            return true;
        }
    }
}