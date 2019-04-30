using System;
using System.Collections.Generic;
using System.Text;
using Chess.BoardLayer;
using Chess.BoardLayer.Enums;
using Chess.GameLayer;

namespace Chess
{
    class Screen
    {
        public static void PrintBoard(Board board, bool[,] possibleMoves)
        {
            for (int i = 0; i < board.Lines; i++)
            {
                Console.Write((8 - i).ToString() + "  ");
                for (int j = 0; j < board.Columns; j++)
                {
                    if (possibleMoves[i, j] == true)
                    {
                        ConsoleColor aux = Console.BackgroundColor;
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                        PrintPiece(board.GetPiece(i, j));
                        Console.BackgroundColor = aux;
                    }
                    else
                    {
                        PrintPiece(board.GetPiece(i, j));
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("   a b c d e f g h");
        }

        public static void PrintPiece(Piece piece)
        {
            if (piece == null)
            {
                Console.Write("-");
            }
            else if (piece.Color == Color.White)
            {
                Console.Write(piece);
            }
            else
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(piece);
                Console.ForegroundColor = aux;
            }
            Console.Write(" ");
        }

        public static void PrintPosition(Position pos)
        {
            string line = (8 - pos.Line).ToString();
            string col = (char)('a' + pos.Column) + "";
            Console.WriteLine(col + line);
        }

        public static ChessPosition ReadPosition()
        {
            string str = Console.ReadLine();
            char col = str[0];
            int lin = int.Parse(str[1] + "");
            return new ChessPosition(col, lin);
        }

        public static void PrintPlay(ChessGame game, bool[,] possibleMoves)
        {
            Console.Clear();
            Screen.PrintBoard(game.Board, possibleMoves);

            Console.WriteLine();

            PrintCapturedPieces(game);

            Console.WriteLine("Turn #" + game.Turn.ToString());
            Console.WriteLine("Current player: " + game.CurrentPlayer);
            if (game.IsInCheck(game.CurrentPlayer))
            {
                Console.WriteLine("CHECK!");
            }
            Console.WriteLine();
        }

        public static void EndGame(ChessGame game)
        {
            if (game.Finished)
            {
                Screen.PrintPlay(game, new bool[game.Board.Lines, game.Board.Columns]);
                Console.WriteLine("CHECKMATE! " + game.Opponent(game.CurrentPlayer) + " player wins!");
                Console.WriteLine();
            }
        }

        public static void PrintCapturedPieces(ChessGame game)
        {
            Console.Write("Pieces captured by white player: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            PrintCapturedPiecesColor(game, Color.Black);
            Console.ForegroundColor = aux;
            Console.Write("Pieces captured by black player: ");
            PrintCapturedPiecesColor(game, Color.White);
            Console.WriteLine();
        }

        public static void PrintCapturedPiecesColor(ChessGame game, Color color)
        {
            HashSet<Piece> set = game.CapturedPiecesColor(color);
            StringBuilder str = new StringBuilder();
            str.Append("[ ");
            foreach (Piece piece in set)
            {
                str.Append(piece.ToString() + " ");
            }
            str.Append("]");
            Console.WriteLine(str.ToString());

        }

    }
}
