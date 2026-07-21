using System;

namespace ProjectSpace.Domain.Resources
{
    public sealed class ResourcePool
    {
        public int Salvage { get; private set; }

        public ResourcePool(int salvage)
        {
            if (salvage < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(salvage),
                    "The amount of salvage cannot be negative.");
            }

            Salvage = salvage;
        }

        public void Add(int amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(amount),
                    "The amount to add must be greater than zero.");
            }

            Salvage += amount;
        }

        public bool TrySpend(int amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(amount),
                    "The amount to spend must be greater than zero.");
            }

            if (Salvage < amount)
            {
                return false;
            }

            Salvage -= amount;

            return true;
        }
    }
}