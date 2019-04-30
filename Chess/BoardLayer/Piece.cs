using Chess.BoardLayer.Enums;

namespace Chess.BoardLayer
{
    abstract class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int MovementAmount { get; protected set; }
        public Board Board { get; protected set; }

        public Piece(Board board, Color color)
        {
            Position = null;
            Color = color;
            Board = board;
            MovementAmount = 0;
        }

        public void IncrementMovementAmount()
        {
            MovementAmount += 1;
        }

        public void DecrementMovementAmount()
        {
            MovementAmount -= 1;
        }

        public bool CanItMoveHere(Position pos)
        {
            return PossibleMoves()[pos.Line, pos.Column];
        }

        public bool CanItMove()
        {
            bool[,] moves = PossibleMoves();
            for (int i = 0; i < Board.Lines; i++)
            {
                for (int j = 0; j < Board.Columns; j++)
                {
                    if (moves[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool IsPosValidAndEmptyOrEnemy(Position pos)
        {
            if (Board.ValidPosition(pos))
            {
                return !Board.IsTherePiece(pos) || Board.GetPiece(pos).Color != Color;
            }
            else
            {
                return false;
            }
        }

        public abstract bool[,] PossibleMoves();

    }
}
