using Chess.BoardLayer;
using Chess.BoardLayer.Enums;

namespace Chess.GameLayer
{
    class Knight : Piece
    {
        public Knight(Board board, Color color)
            : base(board, color)
        { }

        public override string ToString()
        {
            return "N";
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] boardMoves = new bool[Board.Lines, Board.Columns];
            for (int ver = -2; ver <= 2; ver+=4)
            {
                for (int hor = -1; hor <= 1; hor+=2)
                {
                    Position pos = new Position(Position.Line + ver, Position.Column + hor);
                    if (IsPosValidAndEmptyOrEnemy(pos))
                    {
                        boardMoves[pos.Line, pos.Column] = true;
                    }
                }
            }
            for (int ver = -1; ver <= 1; ver += 2)
            {
                for (int hor = -2; hor <= 2; hor += 4)
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