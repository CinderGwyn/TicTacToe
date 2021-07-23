using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToe
{

    public class Game
    {   
        // Fields
        static char[] board = new char[] { ' ', '1', '2', '3', '4', '5', '6', '7', '8', '9' }; //represents the 9 squares on the board, skipping the 0 element for clarity
        static string boardDisplay;
        static char currentPlayer = 'X';

        // Constructors

        // Default
        public Game(char[] inboard, char inCurrentPlayer)
        {
            Array.Copy(inboard, board, 10);
            currentPlayer = inCurrentPlayer;

        }

        public Game()
        { }

        
        // Methods
        static void UpdateDisplay()  //updates the formatted string used for displaying the gameboard to the board array's current state
        {
        boardDisplay = String.Format("{0}{1}|{2}|{3}\n-----\n{4}|{5}|{6}\n-----\n{7}|{8}|{9}\n", null, board[1], board[2], board[3], board[4], board[5], board[6], board[7], board[8], board[9]);
        }

        static void PrintTurn()
        {
            Console.WriteLine($"Player {currentPlayer}'s turn\n");
        }

        static bool IsSingleDigit(string str)
        {
            if (str.Length > 1)
            {
                return false;
            }

            return true;
        }

        public static int GetMove() //Prompts the user for a valid move and re-prompts for every invailid input.  Checks that the input is an integer, single digit, and indicates an unclaimed space on the board.
        {
            Console.WriteLine("Enter the number of the square to give your mark:\n");
            var moveAsString = Console.ReadLine();
            int moveAsInt;

            while (!int.TryParse(moveAsString, out moveAsInt) || !IsSingleDigit(moveAsString) || !board.Contains(char.Parse(moveAsString)))
            {
                Console.WriteLine("This is not a valid input! Try an unclaimed square 1-9");
                moveAsString = Console.ReadLine();
            }

            Console.WriteLine($"Your move is {moveAsInt}");
            return moveAsInt;
        }

        static void UpdateBoard(int moveInput)  //updates the array containing the states of the board's squares
        {
            board[moveInput] = currentPlayer;
            UpdateDisplay();
        }

        static void PrintBoard()
        {
            Console.WriteLine(boardDisplay);
        }


        static void Main()
        {   
            UpdateDisplay();
            PrintBoard();
            PrintTurn();
            UpdateBoard(GetMove());
            PrintBoard();
        }
}

}

