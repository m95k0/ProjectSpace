using NUnit.Framework;
using ProjectSpace.Domain.Modules;
using ProjectSpace.Domain.Shops;
using System;
using System.Collections.Generic;

namespace ProjectSpace.Domain.Tests
{
    public class ShopTests
    {
        [Test]
        public void Constructor_ShouldStoreOffers()
        {
            // Arrange
            List<ShopOffer> offers = new()
            {
                new ShopOffer(ModuleCatalog.Engine, 120),
                new ShopOffer(ModuleCatalog.Turret, 80)
            };

            // Act
            Shop shop = new Shop(offers);

            // Assert
            Assert.That(
                shop.Offers.Count,
                Is.EqualTo(2));
        }

        [Test]
        public void Constructor_ShouldThrow_WhenOffersIsNull()
        {
            Assert.Throws<ArgumentNullException>(
                () => new Shop(null));
        }

        [Test]
        public void Constructor_ShouldThrow_WhenOffersContainNull()
        {
            List<ShopOffer> offers = new()
            {
                new ShopOffer(ModuleCatalog.Engine, 120),
                null
            };

            Assert.Throws<ArgumentException>(
                () => new Shop(offers));
        }

        [Test]
        public void Contains_ShouldReturnTrue_WhenModuleExists()
        {
            // Arrange
            ShopOffer offer = new ShopOffer(
                ModuleCatalog.Engine,
                120);

            Shop shop = new Shop(
                new List<ShopOffer>
                {
            offer
                });

            // Act
            bool result = shop.Contains(
                ModuleCatalog.Engine);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void Contains_ShouldReturnFalse_WhenModuleDoesNotExist()
        {
            // Arrange
            Shop shop = new Shop(
                new List<ShopOffer>
                {
            new ShopOffer(
                ModuleCatalog.Engine,
                120)
                });

            // Act
            bool result = shop.Contains(
                ModuleCatalog.Turret);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void TryGetOffer_ShouldReturnTrue_WhenOfferExists()
        {
            // Arrange
            ShopOffer engineOffer = new ShopOffer(
                ModuleCatalog.Engine,
                120);

            Shop shop = new Shop(
                new List<ShopOffer>
                {
            engineOffer
                });

            // Act
            bool result = shop.TryGetOffer(
                ModuleCatalog.Engine,
                out ShopOffer offer);

            // Assert
            Assert.IsTrue(result);

            Assert.That(
                offer,
                Is.EqualTo(engineOffer));
        }

        [Test]
        public void TryGetOffer_ShouldReturnFalse_WhenOfferDoesNotExist()
        {
            // Arrange
            Shop shop = new Shop(
                new List<ShopOffer>
                {
            new ShopOffer(
                ModuleCatalog.Engine,
                120)
                });

            // Act
            bool result = shop.TryGetOffer(
                ModuleCatalog.Turret,
                out ShopOffer offer);

            // Assert
            Assert.IsFalse(result);

            Assert.IsNull(offer);
        }

        [Test]
        public void RemoveOffer_ShouldRemoveOffer()
        {
            // Arrange
            ShopOffer engineOffer = new ShopOffer(
                ModuleCatalog.Engine,
                120);

            Shop shop = new Shop(
                new List<ShopOffer>
                {
            engineOffer
                });

            // Act
            bool removed = shop.RemoveOffer(engineOffer);

            // Assert
            Assert.IsTrue(removed);

            Assert.That(
                shop.Offers.Count,
                Is.EqualTo(0));
        }
    }
}