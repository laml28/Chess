using Chess.BoardLayer;
using Chess.BoardLayer.Enums;

namespace Chess.GameLayer
{
    class Rook : Piece
    {
        public Rook(Board board, Color color)
            : base(board, color)
        { }

        public override string ToString()
        {
            return "R";
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] boardMoves = new bool[Board.Lines, Board.Columns];

            Position pos = new Position(Position.Line, Position.Column);
            pos.Line++;
            while (IsPosValidAndEmptyOrEnemy(pos))
            {
                boardMoves[pos.Line, pos.Column] = true;
                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != Color)
                {
                    break;
                }
                pos.Line++;
            }

            pos = new Position(Position.Line, Position.Column);
            pos.Line--;
            while (IsPosValidAndEmptyOrEnemy(pos))
            {
                boardMoves[pos.Line, pos.Column] = true;
                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != Color)
                {
                    break;
                }
                pos.Line--;
            }

            pos = new Position(Position.Line, Position.Column);
            pos.Column++;
            while (IsPosValidAndEmptyOrEnemy(pos))
            {
                boardMoves[pos.Line, pos.Column] = true;
                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != Color)
                {
                    break;
                }
                pos.Column++;
            }

            pos = new Position(Position.Line, Position.Column);
            pos.Column--;
            while (IsPosValidAndEmptyOrEnemy(pos))
            {
                boardMoves[pos.Line, pos.Column] = true;
                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != Color)
                {
                    break;
                }
                pos.Column--;
            }

            boardMoves[Position.Line, Position.Column] = false;
            return boardMoves;
        }

    }
}
