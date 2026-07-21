namespace ProjectSpace.Domain.Shops
{
    using System;
    using ProjectSpace.Domain.Modules;

    public sealed class ShopOffer
    {
        public ModuleDefinition Module { get; }

        public int Price { get; }

        public ShopOffer(
            ModuleDefinition module,
            int price)
        {
            if (module == null)
            {
                throw new ArgumentNullException(nameof(module));
            }

            if (price <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(price),
                    "Price must be greater than zero.");
            }

            Module = module;
            Price = price;
        }
    }
}