using NUnit.Framework;
using ProjectSpace.Domain.Common;
using ProjectSpace.Domain.Modules;
using ProjectSpace.Domain.Ships;
using System.Linq;

namespace ProjectSpace.Domain.Tests
{
    public class Moduletests
    {
        //После создания здоровье равно максимальному.
        [Test]
        public void Constructor_ShouldSetCurrentHealthToMaxHealth()
        {
            ModuleInstance module =
                new(ModuleCatalog.Core, new GridPosition(0, 0));

            Assert.That(module.CurrentHealth,
                Is.EqualTo(ModuleCatalog.Core.MaxHealth));
        }
        
        //Модуль хранит своё определение.
        [Test]
        public void Constructor_ShouldSetDefinition()
        {
            // Arrange
            ModuleDefinition definition = ModuleCatalog.Engine;

            // Act
            ModuleInstance module = new ModuleInstance(
                definition,
                new GridPosition(5, 7));

            // Assert
            Assert.That(
                module.Definition,
                Is.SameAs(definition));
        }
        
        //Модуль хранит свою позицию.
        [Test]
        public void Constructor_ShouldSetPosition()
        {
            // Arrange
            GridPosition position = new GridPosition(5, 7);

            // Act
            ModuleInstance module = new ModuleInstance(
                ModuleCatalog.Engine,
                position);

            // Assert
            Assert.That(
                module.Position,
                Is.EqualTo(position));
        }

        //Здоровье никогда не становится отрицательным.
        [Test]
        public void TakeDamage_ShouldNotGoBelowZero()
        {
            // Arrange
            ModuleInstance module = new ModuleInstance(
                ModuleCatalog.Engine,
                new GridPosition(0, 0));

            // Act
            module.TakeDamage(1000);

            // Assert
            Assert.That(
                module.CurrentHealth,
                Is.EqualTo(0));
        }

        //Получение урона уменьшает здоровье.
        [Test]
        public void TakeDamage_ShouldReduceHealth()
        {
            ModuleInstance module =
                new (ModuleCatalog.Core, new GridPosition(0, 0));

            module.TakeDamage(10);

            Assert.That(module.CurrentHealth,
                Is.EqualTo(ModuleCatalog.Core.MaxHealth - 10));
        }

        //Отрицательный урон игнорируется.
        [Test]
        public void TakeDamage_WithNegativeValue_ShouldDoNothing()
        {
            ModuleInstance module = new ModuleInstance(
                ModuleCatalog.Engine,
                new GridPosition(0, 0));

            module.TakeDamage(-50);

            Assert.That(
                module.CurrentHealth,
                Is.EqualTo(ModuleCatalog.Engine.MaxHealth));
        }

        //Ремонт не превышает максимум.
        [Test]
        public void Repair_ShouldNotExceedMaxHealth()
        {
            ModuleInstance module =
                new(ModuleCatalog.Core, new GridPosition(0, 0));

            module.TakeDamage(20);
            module.Repair(1000);

            Assert.That(module.CurrentHealth,
                Is.EqualTo(ModuleCatalog.Core.MaxHealth));
        }

        //Ремонт не превышает максимум.
        [Test]
        public void Repair_ShouldIncreaseHealth()
        {
            ModuleInstance module = new ModuleInstance(
                ModuleCatalog.Engine,
                new GridPosition(0, 0));

            module.TakeDamage(50);

            module.Repair(20);

            Assert.That(
                module.CurrentHealth,
                Is.EqualTo(120));
        }

        //Отрицательный ремонт игнорируется.
        [Test]
        public void Repair_WithNegativeValue_ShouldDoNothing()
        {
            ModuleInstance module = new ModuleInstance(
                ModuleCatalog.Engine,
                new GridPosition(0, 0));

            module.TakeDamage(50);

            module.Repair(-100);

            Assert.That(
                module.CurrentHealth,
                Is.EqualTo(100));
        }

        //При 0 HP модуль считается уничтоженным.
        [Test]
        public void IsDestroyed_ShouldBecomeTrue_WhenHealthIsZero()
        {
            ModuleInstance module = new ModuleInstance(
                ModuleCatalog.Engine,
                new GridPosition(0, 0));

            module.TakeDamage(1000);

            Assert.That(
                module.IsDestroyed,
                Is.True);
        }


    }

}