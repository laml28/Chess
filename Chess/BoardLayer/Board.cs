using System;
using System.Collections.Generic;
using System.Text;
using Chess.BoardLayer.Exceptions;

namespace Chess.BoardLayer
{
    class Board
    {
        public int Lines { get; set; }
        public int Columns { get; set; }
        private Piece[,] Pieces;

        public Board(int lines, int columns)
        {
            Lines = lines;
            Columns = columns;
            Pieces = new Piece[lines, columns];
        }

        public Piece GetPiece(int line, int column)
        {
            return Pieces[line, column];
        }

        public Piece GetPiece(Position position)
        {
            return Pieces[position.Line, position.Column];
        }

        public bool IsTherePiece(Position pos)
        {
            ValidatePosition(pos);
            return GetPiece(pos) != null;
        }

        public void PlacePiece(Piece piece, Position position)
        {
            if (IsTherePiece(position))
            {
                throw new BoardException("There is already a piece in this position!");
            }
            Pieces[position.Line, position.Column] = piece;
            piece.Position = position;   // !
        }

        public Piece RemovePiece(Position pos)
        {
            if (GetPiece(pos) == null)
            {
                return null;
            }
            Piece removedPiece = GetPiece(pos);
            removedPiece.Position = null;
            Pieces[pos.Line, pos.Column] = null;
            return removedPiece;
        }

        public bool ValidPosition(Position position)
        {
            if (position.Line >= 0 && position.Line < Lines && position.Column >= 0 && position.Column < Columns)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ValidatePosition(Position pos)
        {
            if (!ValidPosition(pos))
            {
                throw new BoardException("Invalid position!");
            }
        }
    }
}
