using ProjectSpace.Domain.Common;
using ProjectSpace.Domain.Modules;

namespace ProjectSpace.Domain.Ships
{
    public static class ShipFactory
    {
        public static Ship CreatePlayerShip()
        {
            Ship ship = new Ship(new GridSize(30, 30));

            ship.TryAddModule(
                ModuleCatalog.Core,
                new GridPosition(14, 14));

            ship.TryAddModule(
                ModuleCatalog.Cockpit,
                new GridPosition(14, 12));

            ship.TryAddModule(
                ModuleCatalog.Engine,
                new GridPosition(12, 14));

            ship.TryAddModule(
                ModuleCatalog.Engine,
                new GridPosition(17, 14));

            ship.TryAddModule(
                ModuleCatalog.Turret,
                new GridPosition(14, 17));

            return ship;
        }
    }
}