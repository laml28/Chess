using Chess.BoardLayer;
using Chess.BoardLayer.Enums;

namespace Chess.GameLayer
{
    class King : Piece
    {
        public King(Board board, Color color)
            : base(board, color)
        { }

        public override string ToString()
        {
            return "K";
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] boardMoves = new bool[Board.Lines, Board.Columns];
            for (int ver = -1; ver <= 1; ver++)
            {
                for (int hor = -1; hor <= 1; hor++)
                {
                    Position pos = new Position(Position.Line + ver, Position.Column + hor);
                    if (IsPosValidAndEmptyOrEnemy(pos))
                    {
                        boardMoves[pos.Line, pos.Column] = true;
                    }
                }
            }
            return boardMoves;
        }
    }
}
