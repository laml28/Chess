using Chess.BoardLayer;

namespace Chess.GameLayer
{
    class ChessPosition
    {
        public char Column { get; set; }
        public int Line { get; set; }

        public ChessPosition(char column, int line)
        {
            Column = column;
            Line = line;
        }

        public override string ToString()
        {
            return "" + Column + Line;
        }

        public Position ToPosition()
        {
            return new Position(8 - Line, Column - 'a');
        }

    }
}
