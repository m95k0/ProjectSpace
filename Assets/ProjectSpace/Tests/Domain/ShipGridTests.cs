using NUnit.Framework;
using ProjectSpace.Domain.Common;
using ProjectSpace.Domain.Modules;
using ProjectSpace.Domain.Ships;
using System.Linq;

namespace ProjectSpace.Domain.Tests
{
    public class ShipGridTests
    {
        [Test]
        public void GetModuleAt_ShouldReturnNull_WhenCellIsEmpty()
        {
            // Arrange
            ShipGrid grid = new ShipGrid(new GridSize(10, 10));

            // Act
            ModuleInstance? module =
                grid.GetModuleAt(new GridPosition(5, 5));

            // Assert
            Assert.That(module, Is.Null);
        }

        [Test]
        public void PlaceModule_ShouldOccupyCells()
        {
            // Arrange
            ShipGrid grid = new ShipGrid(new GridSize(10, 10));

            ModuleInstance engine = new ModuleInstance(
                ModuleCatalog.Engine,
                new GridPosition(3, 4));

            // Act
            grid.PlaceModule(engine);

            // Assert
            Assert.That(
                grid.GetModuleAt(new GridPosition(3, 4)),
                Is.SameAs(engine));

            Assert.That(
                grid.GetModuleAt(new GridPosition(4, 4)),
                Is.SameAs(engine));

            Assert.That(
                grid.GetModuleAt(new GridPosition(3, 5)),
                Is.SameAs(engine));

            Assert.That(
                grid.GetModuleAt(new GridPosition(4, 5)),
                Is.SameAs(engine));

            Assert.That(
                grid.GetModuleAt(new GridPosition(3, 6)),
                Is.SameAs(engine));

            Assert.That(
                grid.GetModuleAt(new GridPosition(4, 6)),
                Is.SameAs(engine));
        }

        [Test]
        public void RemoveModule_ShouldFreeCells()
        {
            // Arrange
            ShipGrid grid = new ShipGrid(new GridSize(10, 10));

            ModuleInstance engine = new ModuleInstance(
                ModuleCatalog.Engine,
                new GridPosition(3, 4));

            grid.PlaceModule(engine);

            // Act
            grid.RemoveModule(engine);

            // Assert
            Assert.That(
                grid.GetModuleAt(new GridPosition(3, 4)),
                Is.Null);

            Assert.That(
                grid.GetModuleAt(new GridPosition(4, 6)),
                Is.Null);
        }

        [Test]
        public void CanPlaceModule_ShouldReturnTrue_WhenAreaIsEmpty()
        {
            // Arrange
            ShipGrid grid = new ShipGrid(new GridSize(10, 10));

            // Act
            bool result = grid.CanPlaceModule(
                ModuleCatalog.Engine,
                new GridPosition(3, 4));

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CanPlaceModule_ShouldReturnFalse_WhenAreaIsOccupied()
        {
            // Arrange
            ShipGrid grid = new ShipGrid(new GridSize(10, 10));

            ModuleInstance engine = new ModuleInstance(
                ModuleCatalog.Engine,
                new GridPosition(3, 4));

            grid.PlaceModule(engine);

            // Act
            bool result = grid.CanPlaceModule(
                ModuleCatalog.Engine,
                new GridPosition(3, 4));

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CanPlaceModule_ShouldReturnFalse_WhenOutsideGrid()
        {
            // Arrange
            ShipGrid grid = new ShipGrid(new GridSize(10, 10));

            // Act
            bool result = grid.CanPlaceModule(
                ModuleCatalog.Engine,
                new GridPosition(9, 9));

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void IsCellEmpty_ShouldReturnTrue_WhenCellIsEmpty()
        {
            // Arrange
            ShipGrid grid = new ShipGrid(new GridSize(10, 10));

            // Act
            bool result = grid.IsCellEmpty(
                new GridPosition(5, 5));

            // Assert
            Assert.That(
                result,
                Is.True);
        }

        [Test]
        public void IsCellEmpty_ShouldReturnFalse_WhenCellIsOccupied()
        {
            // Arrange
            ShipGrid grid = new ShipGrid(new GridSize(10, 10));

            ModuleInstance engine = new ModuleInstance(
                ModuleCatalog.Engine,
                new GridPosition(3, 4));

            grid.PlaceModule(engine);

            // Act
            bool result = grid.IsCellEmpty(
                new GridPosition(3, 4));

            // Assert
            Assert.That(
                result,
                Is.False);
        }

    }
}