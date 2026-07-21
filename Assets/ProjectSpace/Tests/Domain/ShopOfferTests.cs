using NUnit.Framework;
using ProjectSpace.Domain.Modules;
using ProjectSpace.Domain.Shops;
using System;

namespace ProjectSpace.Domain.Tests
{
    public class ShopOfferTests
    {
        [Test]
        public void Constructor_ShouldSetModule()
        {
            // Arrange + Act
            ShopOffer offer = new ShopOffer(
                ModuleCatalog.Engine,
                120);

            // Assert
            Assert.That(
                offer.Module,
                Is.EqualTo(ModuleCatalog.Engine));
        }

        [Test]
        public void Constructor_ShouldSetPrice()
        {
            // Arrange + Act
            ShopOffer offer = new ShopOffer(
                ModuleCatalog.Engine,
                120);

            // Assert
            Assert.That(
                offer.Price,
                Is.EqualTo(120));
        }

        [Test]
        public void Constructor_ShouldThrow_WhenModuleIsNull()
        {
            Assert.Throws<ArgumentNullException>(
                () => new ShopOffer(null, 120));
        }

        [Test]
        public void Constructor_ShouldThrow_WhenPriceIsNotPositive()
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () => new ShopOffer(
                    ModuleCatalog.Engine,
                    0));
        }
    }
}