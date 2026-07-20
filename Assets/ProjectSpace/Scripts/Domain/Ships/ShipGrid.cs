namespace ProjectSpace.Domain.Ships
{
    using ProjectSpace.Domain.Common;
    using ProjectSpace.Domain.Modules;
    using System;
    using System.Collections.Generic;

    public class ShipGrid
    {
        public GridSize Size { get; }

        private readonly ModuleInstance?[,] _cells;

        private void TryAddNeighbor(GridPosition position, ModuleInstance module, HashSet<ModuleInstance> neighbors)
        {

            if (!IsInside(position))
            {
                return;
            }

            ModuleInstance? neighbor = GetModuleAt(position);

            if (neighbor == null)
            {
                return;
            }

            if (neighbor == module)
            {
                return;
            }

            neighbors.Add(neighbor);
        }

        private bool IsInside(GridPosition position)
        {
            return position.X >= 0 &&
                   position.Y >= 0 &&
                   position.X < Size.Width &&
                   position.Y < Size.Height;
        }
        public ModuleInstance? GetModuleAt(GridPosition position)
        {
            if (!IsInside(position))
            {
                throw new ArgumentOutOfRangeException(
                    nameof(position),
                    $"Position {position} is outside the grid."
                );
            }

            return _cells[position.X, position.Y];
        }
        public bool IsCellEmpty(GridPosition position)
        {
            return GetModuleAt(position) == null;
        }

        public bool CanPlaceModule(ModuleDefinition definition, GridPosition position)
        {
            GridSize size = definition.Size;

            for (int y = 0; y < size.Height; y++)
            {
                for (int x = 0; x < size.Width; x++)
                {
                    GridPosition currentPosition = new GridPosition(
                        position.X + x,
                        position.Y + y);

                    if (!IsInside(currentPosition))
                    {
                        return false;
                    }

                    if (!IsCellEmpty(currentPosition))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public void PlaceModule(ModuleInstance module)
        {
            if (!CanPlaceModule(module.Definition, module.Position))
            {
                throw new InvalidOperationException(
                    "The module cannot be placed at the specified position.");
            }

            GridSize size = module.Definition.Size;

            for (int y = 0; y < size.Height; y++)
            {
                for (int x = 0; x < size.Width; x++)
                {
                    GridPosition currentPosition = new GridPosition(
                        module.Position.X + x,
                        module.Position.Y + y);

                    _cells[currentPosition.X, currentPosition.Y] = module;
                }
            }
        }

        public void RemoveModule(ModuleInstance module)
        {
            GridSize size = module.Definition.Size;

            for (int y = 0; y < size.Height; y++)
            {
                for (int x = 0; x < size.Width; x++)
                {
                    GridPosition currentPosition = new GridPosition(
                        module.Position.X + x,
                        module.Position.Y + y);

                    if (_cells[currentPosition.X, currentPosition.Y] != module)
                    {
                        throw new InvalidOperationException(
                            "The module is not placed correctly on the grid.");
                    }

                    _cells[currentPosition.X, currentPosition.Y] = null;
                }
            }
        }

        internal bool TryMoveModule(ModuleInstance module, GridPosition newPosition)
        {
            if (module.Position.Equals(newPosition))
            {
                return true;
            }

            GridPosition oldPosition = module.Position;

            RemoveModule(module);

            if (!CanPlaceModule(module.Definition, newPosition))
            {
                PlaceModule(module);

                return false;
            }

            module.MoveTo(newPosition);

            PlaceModule(module);

            return true;
        }

        internal IReadOnlyCollection<ModuleInstance> GetNeighbors(ModuleInstance module)
        {
            HashSet<ModuleInstance> neighbors = new();

            GridPosition position = module.Position;
            GridSize size = module.Definition.Size;

            for (int y = 0; y < size.Height; y++)
            {
                for (int x = 0; x < size.Width; x++)
                {
                    GridPosition cell = new GridPosition(
                        position.X + x,
                        position.Y + y);

                    TryAddNeighbor(cell.Left(), module, neighbors);
                    TryAddNeighbor(cell.Right(), module, neighbors);
                    TryAddNeighbor(cell.Down(), module, neighbors);
                    TryAddNeighbor(cell.Up(), module, neighbors);
                }
            }

            return neighbors;
        }

        public ShipGrid(GridSize size)
        {
            Size = size;
            _cells = new ModuleInstance[size.Width, size.Height];
        }
    }
}