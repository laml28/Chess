using Chess.BoardLayer;
using Chess.BoardLayer.Enums;

namespace Chess.GameLayer
{
    class Pawn : Piece
    {
        public Pawn(Board board, Color color)
            : base(board, color)
        { }

        public override string ToString()
        {
            return "P";
        }

        public bool IsPosValidAndEmpty(Position pos)
        {
            if (Board.ValidPosition(pos))
            {
                return !Board.IsTherePiece(pos);
            }
            else
            {
                return false;
            }
        }

        public bool IsPosValidAndEnemy(Position pos)
        {
            if (Board.ValidPosition(pos))
            {
                return Board.IsTherePiece(pos) && Board.GetPiece(pos).Color != Color;
            }
            else
            {
                return false;
            }
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] boardMoves = new bool[Board.Lines, Board.Columns];

            int lineUpOrDown = 1;

            if (Color==Color.White)
            {
                lineUpOrDown = -1;
            }

            for (int hor = -1; hor <= 1; hor++)
            {
                Position pos = new Position(Position.Line + lineUpOrDown, Position.Column + hor);
                if (hor % 2 != 0)
                {
                    if (IsPosValidAndEnemy(pos))
                    {
                        boardMoves[pos.Line, pos.Column] = true;
                    }
                }
                else
                {
                    if (IsPosValidAndEmpty(pos))
                    {
                        boardMoves[pos.Line, pos.Column] = true;
                    }
                }
            }

            if (MovementAmount==0)
            {
                boardMoves[Position.Line + 2*lineUpOrDown, Position.Column] = true;
            }

            return boardMoves;
        }
    }
}
