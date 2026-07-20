using NUnit.Framework;
using ProjectSpace.Domain.Common;
using ProjectSpace.Domain.Modules;
using ProjectSpace.Domain.Ships;
using System.Collections.Generic;
using System.Linq;

namespace ProjectSpace.Domain.Tests
{
    public class ShipConnectivityTests
    {
        [Test]
        public void GetConnectedModules_ShouldReturnOnlyCore_WhenShipHasOnlyCore()
        {
            // Arrange
            Ship ship = new Ship(new GridSize(30, 30));

            ship.TryAddModule(
                ModuleCatalog.Core,
                new GridPosition(10, 10));

            // Act
            IReadOnlyCollection<ModuleInstance> connected =
                ShipConnectivity.GetConnectedModules(ship);

            // Assert
            Assert.That(
                connected.Count,
                Is.EqualTo(1));

            Assert.That(
                connected.Contains(ship.Core!),
                Is.True);
        }

        [Test]
        public void GetConnectedModules_ShouldReturnAllConnectedModules()
        {
            // Arrange
            Ship ship = ShipFactory.CreatePlayerShip();

            // Act
            IReadOnlyCollection<ModuleInstance> connected =
                ShipConnectivity.GetConnectedModules(ship);

            TestContext.WriteLine($"Connected = {connected.Count}");

            foreach (ModuleInstance module in connected)
            {
                TestContext.WriteLine(
                    $"{module.Definition.Name} {module.Position}");
            }

            // Assert
            Assert.That(
                connected.Count,
                Is.EqualTo(5));

            foreach (ModuleInstance module in ship.Modules)
            {
                Assert.That(
                    connected.Contains(module),
                    Is.True);
            }
        }

        [Test]
        public void GetConnectedModules_ShouldIgnoreDisconnectedModule()
        {
            // Arrange
            Ship ship = new Ship(new GridSize(30, 30));

            ship.TryAddModule(
                ModuleCatalog.Core,
                new GridPosition(10, 10));

            ship.TryAddModule(
                ModuleCatalog.Engine,
                new GridPosition(20, 20));

            // Act
            IReadOnlyCollection<ModuleInstance> connected =
                ShipConnectivity.GetConnectedModules(ship);

            // Assert
            Assert.That(
                connected.Count,
                Is.EqualTo(1));

            Assert.That(
                connected.Contains(ship.Core!),
                Is.True);
        }

        [Test]
        public void GetConnectedModules_ShouldReturnModulesConnectedThroughOtherModules()
        {
            // Arrange
            Ship ship = new Ship(new GridSize(30, 30));

            ship.TryAddModule(
                ModuleCatalog.Core,
                new GridPosition(10, 10));

            ship.TryAddModule(
                ModuleCatalog.Engine,
                new GridPosition(10, 13));

            ship.TryAddModule(
                ModuleCatalog.Turret,
                new GridPosition(10, 16));

            // Act
            IReadOnlyCollection<ModuleInstance> connected =
                ShipConnectivity.GetConnectedModules(ship);

            // Assert
            Assert.That(
                connected.Count,
                Is.EqualTo(3));
        }

        [Test]
        public void GetConnectedModules_ShouldNotReturnDuplicateModules()
        {
            // Arrange
            Ship ship = ShipFactory.CreatePlayerShip();

            // Act
            IReadOnlyCollection<ModuleInstance> connected =
                ShipConnectivity.GetConnectedModules(ship);

            // Assert
            Assert.That(
                connected.Count,
                Is.EqualTo(
                    connected.Distinct().Count()));
        }



    }
}