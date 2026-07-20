namespace ProjectSpace.Domain.Common
{
    public readonly struct GridPosition
    {
        public int X { get; }

        public int Y { get; }

        public GridPosition(int x, int y)
        {
            X = x;
            Y = y;
        }
        public GridPosition Left()
        {
            return new GridPosition(X - 1, Y);
        }

        public GridPosition Right()
        {
            return new GridPosition(X + 1, Y);
        }

        public GridPosition Up()
        {
            return new GridPosition(X, Y - 1);
        }

        public GridPosition Down()
        {
            return new GridPosition(X, Y + 1);
        }

    }
}