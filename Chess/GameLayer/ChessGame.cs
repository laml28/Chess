using System;
using System.Collections.Generic;
using System.Text;
using Chess.BoardLayer;
using Chess.BoardLayer.Enums;
using Chess.BoardLayer.Exceptions;

namespace Chess.GameLayer
{
    class ChessGame
    {
        public Board Board { get; private set; }
        public int Turn { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool Finished { get; set; }
        public HashSet<Piece> Pieces { get; set; }
        public HashSet<Piece> CapturedPieces { get; set; }

        public ChessGame()
        {
            Board = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            Finished = false;
            Pieces = new HashSet<Piece>();
            CapturedPieces = new HashSet<Piece>();
            PlacePieces();
        }

        public HashSet<Piece> CapturedPiecesColor(Color color)
        {
            HashSet<Piece> set = new HashSet<Piece>();
            foreach (Piece piece in CapturedPieces)
            {
                if (piece.Color==color)
                {
                    set.Add(piece);
                }
            }
            return set;
        }

        public HashSet<Piece> PiecesInPlayColor(Color color)
        {
            HashSet<Piece> set = new HashSet<Piece>();
            foreach (Piece piece in Pieces)
            {
                if (piece.Color == color)
                {
                    set.Add(piece);
                }
            }
            set.ExceptWith(CapturedPiecesColor(color));
            return set;
        }

        public Piece Move(Position posInit, Position posFinal)
        {
            Piece piece = Board.RemovePiece(posInit);
            piece.IncrementMovementAmount();
            Piece capturedPiece = Board.RemovePiece(posFinal);
            Board.PlacePiece(piece, posFinal);
            if (capturedPiece != null)
            {
                CapturedPieces.Add(capturedPiece);
            }
            return capturedPiece;
        }

        public void UndoMove(Position posInit, Position posFinal, Piece capturedPiece)
        {
            Piece piece = Board.RemovePiece(posFinal);
            piece.DecrementMovementAmount();
            Board.PlacePiece(piece, posInit);
            if (capturedPiece!=null)
            {
                Board.PlacePiece(capturedPiece, posFinal);
                CapturedPiecesColor(capturedPiece.Color).Remove(capturedPiece);
            }
        }

        public void PerformPlay(Position posInit, Position posFinal)
        {
            Piece capturedPiece = Move(posInit, posFinal);
            if (!IsInCheck(CurrentPlayer))
            {
                Turn++;
                ChangePlayer();
            }
            else
            {
                UndoMove(posInit, posFinal, capturedPiece);
                throw new BoardException("You cannot put yourself in check!");
            }
            if (IsCheckMate(CurrentPlayer))
            {
                Finished = true;
                //throw new BoardException("CHECKMATE! " + Opponent(CurrentPlayer) + " player wins!");
            }
        }

        public void CheckInitialPosition(Position pos)
        {
            if (Board.GetPiece(pos) == null)
            {
                throw new BoardException("There is no piece in this position!");
            }
            if (Board.GetPiece(pos).Color != CurrentPlayer)
            {
                throw new BoardException("It is not this player's turn!");
            }
            if (!Board.GetPiece(pos).CanItMove())
            {
                throw new BoardException("This piece cannot move!");
            }
        }

        public void CheckFinalPosition(Position initialPos, Position finalPos)
        {
            if (!Board.ValidPosition(finalPos) || !Board.GetPiece(initialPos).CanItMoveHere(finalPos))
            {
                throw new BoardException("Final position not allowed");
            }

        }

        public void ChangePlayer()
        {
            if (CurrentPlayer == Color.Black)
            {
                CurrentPlayer = Color.White;
            }
            else
            {
                CurrentPlayer = Color.Black;
            }
        }

        public Color Opponent(Color color)
        {
            if (color == Color.White)
            {
                return Color.Black;
            }
            else
            {
                return Color.White;
            }
        }
        
        public Piece GetKing(Color color)
        {
            foreach (Piece piece in PiecesInPlayColor(color))
            {
                if (piece is King)
                {
                    return piece;
                }
            }
            return null;
        }

        public bool IsInCheck(Color color)
        {

            foreach (Piece piece in PiecesInPlayColor(Opponent(color)))
            {
                bool[,] possibleMoves = piece.PossibleMoves();
                if (possibleMoves[GetKing(color).Position.Line, GetKing(color).Position.Column])
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsCheckMate(Color color)
        {
            if (!IsInCheck(color))
            {
                return false;
            }
            else
            {
                foreach (Piece piece in PiecesInPlayColor(color))
                {
                    Position initPos = piece.Position;
                    bool[,] possibleMoves = piece.PossibleMoves();
                    for (int i = 0; i < Board.Lines; i++)
                    {
                        for (int j = 0; j < Board.Columns; j++)
                        {
                            if (possibleMoves[i, j])
                            {
                                Position finalPos = new Position(i, j);
                                Piece capturedPiece = Move(initPos, finalPos);
                                bool check = IsInCheck(color);
                                UndoMove(initPos, finalPos, capturedPiece);
                                if (!check)
                                {
                                    
                                    return false;
                                }
                            }
                        }
                    }
                }
                return true;
            }
        }


        public void AddPiece(char col, int lin, Piece piece)
        {
            Board.PlacePiece(piece, new ChessPosition(col, lin).ToPosition());
            Pieces.Add(piece);
        }

        public void PlacePieces()
        {
            
            AddPiece('a', 1, new Rook(Board, Color.White));
            AddPiece('b', 1, new Knight(Board, Color.White));
            AddPiece('c', 1, new Bishop(Board, Color.White));
            AddPiece('d', 1, new King(Board, Color.White));
            AddPiece('e', 1, new Queen(Board, Color.White));
            AddPiece('f', 1, new Bishop(Board, Color.White));
            AddPiece('g', 1, new Knight(Board, Color.White));
            AddPiece('h', 1, new Rook(Board, Color.White));
            for (int i = 'a'; i < 'a'+Board.Columns; i++)
            {
                AddPiece((char)i, 2, new Pawn(Board, Color.White));
                AddPiece((char)i, 7, new Pawn(Board, Color.Black));
            }

            AddPiece('a', 8, new Rook(Board, Color.Black));
            AddPiece('b', 8, new Knight(Board, Color.Black));
            AddPiece('c', 8, new Bishop(Board, Color.Black));
            AddPiece('d', 8, new King(Board, Color.Black));
            AddPiece('e', 8, new Queen(Board, Color.Black));
            AddPiece('f', 8, new Bishop(Board, Color.Black));
            AddPiece('g', 8, new Knight(Board, Color.Black));
            AddPiece('h', 8, new Rook(Board, Color.Black));
            

            /*
            AddPiece('b', 2, new Rook(Board, Color.White));
            AddPiece('c', 1, new Rook(Board, Color.White));
            AddPiece('a', 8, new King(Board, Color.Black));
            AddPiece('d', 1, new King(Board, Color.White));
            */

        }

    }
}
