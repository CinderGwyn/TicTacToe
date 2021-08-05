using System;
using System.Linq;
using System.IO;

namespace TicTacToe
{


    public class Game
    {
        // Fields
        readonly char[] board = new char[] { ' ', '1', '2', '3', '4', '5', '6', '7', '8', '9' }; //represents the 9 squares on the board, skipping the 0 element for clarity
        string boardDisplay;
        private char currentPlayer = 'X';
        public char GetPlayer  //property
        {
            get
            {
                return this.currentPlayer;
            }
        }

        // Constructors


        public Game(char[] inboard, char inCurrentPlayer)
        {
            Array.Copy(inboard, this.board, 10);
            this.currentPlayer = inCurrentPlayer;

        }

        public Game()
        { }

        // Interfaces
        public interface IConsole  //abstraction for Console.Read/WriteLine()
        {
            void Write(string message);
            void WriteLine(string message);
            string ReadLine();

            void Clear();
        }

        // Overrides

        public override bool Equals(object obj)
        {
            Game b = obj as Game;

            if ((b.board == this.board) && (b.GetPlayer == this.GetPlayer))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(board, GetPlayer);
        }

        // Methods
        public void UpdateDisplay()  //updates the formatted string used for displaying the gameboard to the board array's current state
        {
            this.boardDisplay = String.Format("\n{0}{1}|{2}|{3}\n-----\n{4}|{5}|{6}\n-----\n{7}|{8}|{9}\n", null, this.board[1], this.board[2], this.board[3], this.board[4], this.board[5], this.board[6], this.board[7], this.board[8], this.board[9]);
            
        }


        public bool IsSingleDigit(string str)
        {
            if (str.Length > 1)
            {
                return false;
            }

            return true;
        }

        public int GetMove(IConsole console) //Prompts the user for a valid move and re-prompts for every invailid input.  Checks that the input is an integer, single digit, and indicates an unclaimed space on the board.
        {
            console.Write($"Player {currentPlayer}, enter the number of the square to give your mark: ");
            var moveAsString = console.ReadLine();
            int moveAsInt;

            while (!int.TryParse(moveAsString, out moveAsInt) || !this.IsSingleDigit(moveAsString) || !this.board.Contains(char.Parse(moveAsString)))
            {
                console.WriteLine("This is not a valid input! Try an unclaimed square 1-9");
                moveAsString = console.ReadLine();
            }


            return moveAsInt;
        }

        public void UpdateBoard(int moveInput)  //updates the array containing the states of the board's squares
        {
            
            this.board[moveInput] = this.currentPlayer;
            this.UpdateDisplay();
        }

        public void PrintBoard()
        {

            Console.Clear();
            Console.WriteLine(this.boardDisplay);
        }

        public bool IsWin()
        {
            if (
                (this.board[1] == this.board[2] & this.board[2] == this.board[3]) ||
                (this.board[4] == this.board[5] & this.board[5] == this.board[6]) ||
                (this.board[7] == this.board[8] & this.board[8] == this.board[9]) ||
                (this.board[1] == this.board[4] & this.board[4] == this.board[7]) ||
                (this.board[2] == this.board[5] & this.board[5] == this.board[8]) ||
                (this.board[3] == this.board[6] & this.board[6] == this.board[9]) ||
                (this.board[1] == this.board[5] & this.board[5] == this.board[9]) ||
                (this.board[3] == this.board[5] & this.board[5] == this.board[7])
               )
            {
                return true;
            }
            return false;

        }

        public void ChangePlayer()
        {
            if (this.currentPlayer == 'X')
            {
                this.currentPlayer = 'O';
            }
            else
            {
                this.currentPlayer = 'X';
            }
        }

        public bool IsTie()
        {            
            for (int i = 1; i < this.board.Length; i++)

                if (this.board[i] != 'X' && this.board[i] != 'O')
                {
                    return false;
                }

            return true;
        }

        public bool Save ( string filename )
        {
            try
            {
                System.IO.TextWriter textOut = new System.IO.StreamWriter(filename);
                textOut.WriteLine(this.GetPlayer);
                textOut.WriteLine(this.board);
                textOut.Close();
            }
            catch
            {
                return false;
            }
            return true;
        }

        // Classes

        public class ConsoleWrapper : IConsole
        {
            public void Write(string message)
            {
                Console.Write(message);
            }

            public void WriteLine(string message)
            {
                Console.WriteLine(message);
            }

            public string ReadLine()
            {
                return Console.ReadLine();
            }

            public void Clear()
            {
                Console.Clear();
            }
        }

        static void PlayGame()
        {
            ConsoleWrapper consoleInput = new Game.ConsoleWrapper();
            Game newGame = new Game();

            do
            {
                
                newGame.UpdateDisplay();
                newGame.PrintBoard();
                newGame.UpdateBoard(newGame.GetMove(consoleInput));
                if (newGame.IsWin() == true)
                {
                    newGame.PrintBoard();
                    Console.WriteLine($"Player {newGame.currentPlayer} wins!");
                    break;
                }

                newGame.ChangePlayer();

            }
            while (newGame.IsTie() == false);

            if (newGame.IsTie() == true && newGame.IsWin() == false)
            {
                newGame.PrintBoard();
                Console.WriteLine("Game ends in a tie!");
            }

        }
        static void Main()
        {
            PlayGame();
        }

        
    }
}