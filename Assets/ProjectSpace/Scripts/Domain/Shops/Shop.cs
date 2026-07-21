using System;
using System.Collections.Generic;
using System.Linq;
using ProjectSpace.Domain.Modules;

namespace ProjectSpace.Domain.Shops
{
    public sealed class Shop
    {
        private readonly List<ShopOffer> _offers;

        public IReadOnlyList<ShopOffer> Offers => _offers;

        public Shop(IEnumerable<ShopOffer> offers)
        {
            if (offers == null)
            {
                throw new ArgumentNullException(nameof(offers));
            }

            _offers = new List<ShopOffer>(offers);

            if (_offers.Any(offer => offer == null))
            {
                throw new ArgumentException(
                    "Shop cannot contain null offers.",
                    nameof(offers));
            }
        }

        public bool Contains(ModuleDefinition module)
        {
            if (module == null)
            {
                throw new ArgumentNullException(nameof(module));
            }

            return _offers.Any(
                offer => offer.Module == module);
        }

        public bool TryGetOffer(
            ModuleDefinition module,
            out ShopOffer offer)
        {
            if (module == null)
            {
                throw new ArgumentNullException(nameof(module));
            }

            offer = _offers.FirstOrDefault(
                current => current.Module == module);

            return offer != null;
        }

        public bool RemoveOffer(ShopOffer offer)
        {
            if (offer == null)
            {
                throw new ArgumentNullException(nameof(offer));
            }

            return _offers.Remove(offer);
        }
    }
}