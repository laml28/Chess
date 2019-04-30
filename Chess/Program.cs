using System;
using Chess.BoardLayer;
using Chess.BoardLayer.Enums;
using Chess.GameLayer;
using Chess.BoardLayer.Exceptions;

namespace Chess
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                ChessGame game = new ChessGame();

                while (!game.Finished)
                {

                    try
                    {

                        Screen.PrintPlay(game, new bool[game.Board.Lines, game.Board.Columns]);

                        Console.Write("Initial position: ");
                        ChessPosition initialChessPosition = Screen.ReadPosition();
                        Position initialPosition = initialChessPosition.ToPosition();

                        game.CheckInitialPosition(initialPosition);
                        bool[,] possibleMoves = game.Board.GetPiece(initialPosition).PossibleMoves();

                        Screen.PrintPlay(game, possibleMoves);
                        Console.Write("Initial position: ");
                        Screen.PrintPosition(initialPosition);

                        Console.Write("Final position: ");
                        Position finalPosition = Screen.ReadPosition().ToPosition();
                        game.CheckFinalPosition(initialPosition, finalPosition);

                        game.PerformPlay(initialPosition, finalPosition);

                        Screen.EndGame(game);
                    }
                    catch (BoardException e)
                    {
                        Console.WriteLine();
                        Console.WriteLine(e.Message);
                        Console.Write("Press ENTER to re-do this play. ");
                        Console.ReadLine();
                    }

                }

            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
