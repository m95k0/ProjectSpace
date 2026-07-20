using NUnit.Framework;
using ProjectSpace.Domain.Common;
using ProjectSpace.Domain.Modules;
using ProjectSpace.Domain.Ships;
using System.Linq;

namespace ProjectSpace.Domain.Tests
{
    public class ShipTests
    {
        [Test]
        public void TryAddModule_ShouldAddModule_WhenPositionIsFree()
        {
            // Arrange
            Ship ship = new Ship(new GridSize(30, 30));

            // Act
            bool added = ship.TryAddModule(
                ModuleCatalog.Engine,
                new GridPosition(5, 5));

            // Assert
            Assert.That(added, Is.True);

            Assert.That(
                ship.Modules.Count,
                Is.EqualTo(1));

            Assert.That(
                ship.GetModuleAt(new GridPosition(5, 5)),
                Is.Not.Null);
        }

        [Test]
        public void TryAddModule_ShouldReturnFalse_WhenAreaIsOccupied()
        {
            // Arrange
            Ship ship = new Ship(new GridSize(30, 30));

            ship.TryAddModule(
                ModuleCatalog.Engine,
                new GridPosition(5, 5));

            // Act
            bool added = ship.TryAddModule(
                ModuleCatalog.Engine,
                new GridPosition(5, 5));

            // Assert
            Assert.That(added, Is.False);

            Assert.That(
                ship.Modules.Count,
                Is.EqualTo(1));
        }

        [Test]
        public void TryAddModule_ShouldReturnFalse_WhenAddingSecondCore()
        {
            // Arrange
            Ship ship = new Ship(new GridSize(30, 30));

            ship.TryAddModule(
                ModuleCatalog.Core,
                new GridPosition(5, 5));

            // Act
            bool added = ship.TryAddModule(
                ModuleCatalog.Core,
                new GridPosition(15, 15));

            // Assert
            Assert.That(added, Is.False);

            Assert.That(
                ship.Modules.Count,
                Is.EqualTo(1));
        }

        [Test]
        public void TryRemoveCore_ShouldReturnFalse()
        {
            // Arrange
            Ship ship = ShipFactory.CreatePlayerShip();

            ModuleInstance core = ship.Modules
                .First(m => m.Definition == ModuleCatalog.Core);

            // Act
            bool removed = ship.TryRemoveModule(core);

            // Assert
            Assert.IsFalse(removed);
        }

        [Test]
        public void TryRemoveModule_ShouldRemoveEngine()
        {
            // Arrange
            Ship ship = new Ship(new GridSize(30, 30));

            ship.TryAddModule(
                ModuleCatalog.Engine,
                new GridPosition(5, 5));

            ModuleInstance engine = ship.Modules.First();

            // Act
            bool removed = ship.TryRemoveModule(engine);

            // Assert
            Assert.That(removed, Is.True);

            Assert.That(
                ship.Modules.Count,
                Is.EqualTo(0));

            Assert.That(
                ship.GetModuleAt(new GridPosition(5, 5)),
                Is.Null);
        }

        [Test]
        public void TryRemoveModule_ShouldReturnFalse_WhenModuleDoesNotBelongToShip()
        {
            // Arrange
            Ship ship = new Ship(new GridSize(30, 30));

            ModuleInstance engine = new ModuleInstance(
                ModuleCatalog.Engine,
                new GridPosition(5, 5));

            // Act
            bool removed = ship.TryRemoveModule(engine);

            // Assert
            Assert.That(
                removed,
                Is.False);

            Assert.That(
                ship.Modules.Count,
                Is.EqualTo(0));
        }

        [Test]
        public void TryMoveModule_ShouldMoveEngine_WhenDestinationIsEmpty()
        {
            // Arrange
            Ship ship = new Ship(new GridSize(30, 30));

            ship.TryAddModule(
                ModuleCatalog.Engine,
                new GridPosition(5, 5));

            ModuleInstance engine = ship.Modules.First();

            // Act
            bool moved = ship.TryMoveModule(
                engine,
                new GridPosition(10, 10));

            // Assert
            Assert.That(moved, Is.True);

            Assert.That(
                engine.Position,
                Is.EqualTo(new GridPosition(10, 10)));

            Assert.That(
                ship.GetModuleAt(new GridPosition(10, 10)),
                Is.SameAs(engine));

            Assert.That(
                ship.GetModuleAt(new GridPosition(5, 5)),
                Is.Null);
        }

        [Test]
        public void TryMoveModule_ShouldReturnFalse_WhenOutsideGrid()
        {
            // Arrange
            Ship ship = new Ship(new GridSize(30, 30));

            ship.TryAddModule(
                ModuleCatalog.Engine,
                new GridPosition(5, 5));

            ModuleInstance engine = ship.Modules.First();

            // Act
            bool moved = ship.TryMoveModule(
                engine,
                new GridPosition(29, 29));

            // Assert
            Assert.That(moved, Is.False);

            Assert.That(
                engine.Position,
                Is.EqualTo(new GridPosition(5, 5)));

            Assert.That(
                ship.GetModuleAt(new GridPosition(5, 5)),
                Is.SameAs(engine));
        }

        [Test]
        public void TryMoveModule_ShouldReturnFalse_WhenAreaIsOccupied()
        {
            // Arrange
            Ship ship = new Ship(new GridSize(30, 30));

            ship.TryAddModule(
                ModuleCatalog.Engine,
                new GridPosition(5, 5));

            ship.TryAddModule(
                ModuleCatalog.Engine,
                new GridPosition(10, 10));

            ModuleInstance firstEngine = ship.Modules.First();

            // Act
            bool moved = ship.TryMoveModule(
                firstEngine,
                new GridPosition(10, 10));

            // Assert
            Assert.That(moved, Is.False);

            Assert.That(
                firstEngine.Position,
                Is.EqualTo(new GridPosition(5, 5)));
        }

        [Test]
        public void TryMoveModule_ShouldReturnFalse_WhenMovingCore()
        {
            // Arrange
            Ship ship = new Ship(new GridSize(30, 30));

            ship.TryAddModule(
                ModuleCatalog.Core,
                new GridPosition(10, 10));

            ModuleInstance core = ship.Core!;

            // Act
            bool moved = ship.TryMoveModule(
                core,
                new GridPosition(15, 15));

            // Assert
            Assert.That(moved, Is.False);

            Assert.That(
                core.Position,
                Is.EqualTo(new GridPosition(10, 10)));
        }

        [Test]
        public void CreatePlayerShip_ShouldCreateShipWithStarterModules()
        {
            // Arrange + Act
            Ship ship = ShipFactory.CreatePlayerShip();

            // Assert
            Assert.IsNotNull(ship);

            Assert.IsTrue(ship.Modules.Count > 0);

            Assert.That(
                ship.Modules.Count,
                Is.EqualTo(5)
            );
            TestContext.WriteLine($"Modules count = {ship.Modules.Count}");
        }

        [Test]
        public void ApplyDamage_ShouldReduceModuleHealth()
        {
            // Arrange
            Ship ship = new Ship(new GridSize(30, 30));

            ship.TryAddModule(
                ModuleCatalog.Engine,
                new GridPosition(5, 5));

            ModuleInstance engine = ship.Modules.First();

            // Act
            bool damaged = ship.ApplyDamage(
                new GridPosition(5, 5),
                50);

            // Assert
            Assert.That(damaged, Is.True);

            Assert.That(
                engine.CurrentHealth,
                Is.EqualTo(100));
        }

        [Test]
        public void ApplyDamage_ShouldReturnFalse_WhenCellIsEmpty()
        {
            // Arrange
            Ship ship = new Ship(new GridSize(30, 30));

            // Act
            bool damaged = ship.ApplyDamage(
                new GridPosition(5, 5),
                50);

            // Assert
            Assert.That(damaged, Is.False);
        }

        [Test]
        public void ApplyDamage_ShouldDestroyShip_WhenCoreIsDestroyed()
        {
            // Arrange
            Ship ship = new Ship(new GridSize(30, 30));

            ship.TryAddModule(
                ModuleCatalog.Core,
                new GridPosition(10, 10));

            // Act
            bool damaged = ship.ApplyDamage(
                new GridPosition(10, 10),
                500);

            // Assert
            Assert.That(damaged, Is.True);

            Assert.That(
                ship.IsDestroyed,
                Is.True);
        }

        [Test]
        public void ApplyDamage_ShouldNotDestroyShip_WhenCoreSurvives()
        {
            // Arrange
            Ship ship = new Ship(new GridSize(30, 30));

            ship.TryAddModule(
                ModuleCatalog.Core,
                new GridPosition(10, 10));

            // Act
            bool damaged = ship.ApplyDamage(
                new GridPosition(10, 10),
                100);

            // Assert
            Assert.That(damaged, Is.True);

            Assert.That(
                ship.IsDestroyed,
                Is.False);

            Assert.That(
                ship.Core!.CurrentHealth,
                Is.EqualTo(400));
        }


    }
}