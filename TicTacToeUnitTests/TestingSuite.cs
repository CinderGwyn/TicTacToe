using Microsoft.VisualStudio.TestTools.UnitTesting;
using TicTacToe;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TicTacToeUnitTests

{
    [TestClass]
    public class TestingSuite
    {
        public class ConsoleWrapper : Game.IConsole
        {
            public List<String> LinesToRead = new List<String>();

            public void StringToLines(string commandsIn)  //Allows writing list of commands as a string "ABC" becomes {"A","B","C"}
            {
                foreach(char c in commandsIn)
                {
                    LinesToRead.Add(c.ToString());
                }
            }

            public void Write(string message)
            {
            }

            public void WriteLine(string message)
            {
            }

            public string ReadLine()
            {
                string result = LinesToRead[0];
                LinesToRead.RemoveAt(0);
                return result;
            }

            public void Clear()
            {
                Console.Clear();
            }


        }

        static ConsoleWrapper testInput;
        static Game testBoard;

        public void SetupCommands(string commandsIn)
        {
            testInput = new ConsoleWrapper();
            testBoard = new Game();

            if (commandsIn != " ")
            {
                testInput.StringToLines(commandsIn);
            }

        }
        public void SetupBoard(string boardIn)
        {
            testBoard = new Game();
            foreach (char c in boardIn)
            {
                int intIn = (int)char.GetNumericValue(c);
                testBoard.UpdateBoard(intIn);
            }
        }

        public void SetupBoardAndCommands (string commandsIn, string boardIn)
        {
            testInput = new ConsoleWrapper();
            testBoard = new Game();

            if (commandsIn != " ")
            {
                testInput.StringToLines(commandsIn);
            }

            SetupBoard(boardIn);
            
        }
            /*public TestSetup(string commandsIn, string boardIn, char playerIn)
            {
               

                if (playerIn != ' ')
                {
                    testBoard = new Game(playerIn);
                }
                else
                {
                    testBoard = new Game();
                }

                if (commandsIn != " ")
                {
                    testInput.StringToLines(commandsIn);
                }

                
            } */
         

        
        

        [TestMethod]
        public void ThirdTimesTheCharm()  // first passess in two invalid entries followed by a third valid one.  GetMove should discard all invalid entries and return the valid.
        {
            //Arrange
            SetupCommands("P09");            
            //Act
            int getMoveTest = testBoard.GetMove(testInput);
            //Assert
            Assert.AreEqual<int>(9, getMoveTest);
        }

        [TestMethod]
        public void FirstMove9IsTrue()
        { //test commit

            //Arrange
            SetupCommands("9");
            //Act
            int getMoveTest = testBoard.GetMove(testInput);
            //Assert
            Assert.AreEqual<int>(9, getMoveTest);
        }

        [TestMethod]
        public void FourthTime()  // loads the board with a move (X on square 9) and sees if GetMove() will discard an attempt to use 9 again.
        {
            SetupBoardAndCommands("98", "9");
            

            int getMoveTest = testBoard.GetMove(testInput);

            Assert.AreEqual<int>(8, getMoveTest);
        }

        // IsWin unit tests
        [TestMethod]
        public void XWinsTopRow()
        {
            SetupBoard("123");
            Assert.IsTrue(testBoard.IsWin());
        }

        
        
        [TestMethod]
        public void OneSquareShort()
        {
            SetupBoard("12");
            Assert.IsFalse(testBoard.IsWin());
        }

        [TestMethod]
        public void XWinsMidRow()
        {
            SetupBoard("456");
            Assert.IsTrue(testBoard.IsWin());
        }

        [TestMethod]
        public void XWinsBottomRow()
        {
            SetupBoard("789");
            Assert.IsTrue(testBoard.IsWin());

        }

        [TestMethod]
        public void XWinsLeftColumn()
        {
            SetupBoard("147");
            Assert.IsTrue(testBoard.IsWin());

        }

        [TestMethod]
        public void XWinsMidColumn()
        {
            SetupBoard("258");

            Assert.IsTrue(testBoard.IsWin());

        }

        [TestMethod]
        public void XWinsRightColumn()
        {
            SetupBoard("369");
            Assert.IsTrue(testBoard.IsWin());

        }

        [TestMethod]
        public void XWinsTLDiag()
        {
            SetupBoard("159");
            Assert.IsTrue(testBoard.IsWin());

        }

        [TestMethod]
        public void XWinsBLDiag()
        {
            SetupBoard("357");
            
            Assert.IsTrue(testBoard.IsWin());

        }

        //ChangePlayer() tests
        [TestMethod]
        public void ItsOTurn()
        {
            Game testboard = new Game();

            testboard.ChangePlayer();

            Assert.AreEqual<char>(testboard.GetPlayer, 'O');
        }

        //IsTie() tests
        [TestMethod]
        public void AllSquaresUsed()
        {
            Game testboard = new Game();
            for (int i = 1; i<=9; i++)
            {
                testboard.UpdateBoard(i);
                testboard.ChangePlayer();
            }

            Assert.IsTrue(testboard.IsTie());
        }
        [TestMethod]
        public void NotATie()
        {
            Game testBoard = new Game();

            Assert.IsFalse(testBoard.IsTie());
        }

        [TestMethod]
        public void StillNotATie()
        {
            Game testBoard = new Game ();

            for (int i = 1; i <= 7; i++)
            {
                testBoard.UpdateBoard(i);
                testBoard.ChangePlayer();
            }

            Assert.IsFalse(testBoard.IsTie());
        }

        //Save() Tests

        [TestMethod]
        public void SaveFirstTurn()
        {
            SetupBoard("1");
            testBoard.ChangePlayer();
            Assert.IsTrue(testBoard.Save("SaveFirstTurn.txt"));
        }

        //Load() Tests

        [TestMethod]
        public void LoadFirstTurn()
        {
            SetupBoard("1");
            testBoard.ChangePlayer();
            Game loadedBoard = Game.Load("SaveFirstTurn.txt");
            Assert.IsTrue(loadedBoard.Equals(testBoard));
        }

        [TestMethod]
        public void LoadCompareBoard()
        {
            SetupBoard("1");
            testBoard.ChangePlayer();
            Game loadedBoard = Game.Load("SaveFirstTurn.txt");
            Assert.IsTrue(loadedBoard.GetBoard.SequenceEqual(testBoard.GetBoard));
        }

        //Equals() Tests

        [TestMethod]

        public void EqualsTest()
        {
            SetupBoard("123");
            Game GameX = testBoard, GameY = testBoard;
            Assert.IsTrue(GameX.Equals(GameY));
        }

        [TestMethod]
        public void NotEqualsTest()
        {
            SetupBoard("123");
            Game GameX = testBoard;
            SetupBoard("456");
            Game GameY = testBoard;

            Assert.IsFalse(GameX.Equals(GameY));
        }

        
    }
}
