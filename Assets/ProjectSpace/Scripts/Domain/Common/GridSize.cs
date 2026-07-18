using System;

namespace ProjectSpace.Domain.Common
{
    public readonly struct GridSize
    {
        public int Width { get; }

        public int Height { get; }

        public GridSize(int width, int height)
        {
            if (width <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(width),
                    "Width must be greater than zero.");
            }

            if (height <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(height),
                    "Height must be greater than zero.");
            }

            Width = width;
            Height = height;
        }
    }
}