namespace Day03
{
    public class TobogganMoveCommand
        : ITobogganCommand
    {
        public TobogganMoveCommand(int distanceX, int distanceY)
        {
            DistanceX = distanceX;
            DistanceY = distanceY;
        }

        public int DistanceX { get; }

        public int DistanceY { get; }
    }
}
