using System.Collections.Immutable;

namespace Day03
{
    public class State
    {
        public State
        (
            Topography topography,
            IImmutableList<(int x, int y)> moveHistory,
            int currentX,
            int currentY
        )
        {
            Topography = topography;
            MoveHistory = moveHistory;
            CurrentX = currentX;
            CurrentY = currentY;
        }

        public Topography Topography { get; }

        public int CurrentX { get; }

        public int CurrentY { get; }

        public IImmutableList<(int x, int y)> MoveHistory { get; }
    }
}
