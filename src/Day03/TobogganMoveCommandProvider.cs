namespace Day03
{
    public class TobogganMoveCommandProvider
        : ITobogganCommandProvider
    {
        private readonly TobogganMoveCommand _moveCommandToProvide;

        public TobogganMoveCommandProvider(TobogganMoveCommand moveCommandToProvide)
        {
            _moveCommandToProvide = moveCommandToProvide;
        }

        public ITobogganCommand? GetNextCommand(State state)
        {
            if (state.CurrentY + _moveCommandToProvide.DistanceY >= state.Topography.Height)
            {
                return default;
            }

            return _moveCommandToProvide;
        }
    }
}
