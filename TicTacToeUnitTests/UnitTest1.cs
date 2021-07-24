using Microsoft.VisualStudio.TestTools.UnitTesting;
using TicTacToe;
using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToeUnitTests

{
    [TestClass]
    public class UnitTest1
    {
        public class ConsoleWrapper : Game.IConsole
        {
            public List<String> LinesToRead = new List<String>();

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


        }

        ConsoleWrapper testInput = new ConsoleWrapper();

        [TestMethod]
        public void FirstMove9IsTrue()
        { //test commit

            //Arrange
            Game testBoard = new Game();

            testInput.LinesToRead.Add("9");
            //Act
            int getMoveTest = testBoard.GetMove(testInput);
            //Assert
            Assert.AreEqual<int>(9, getMoveTest);
        }
        [TestMethod]
        public void ThirdTimesTheCharm()  // first passess in two invalid entries followed by a third valid one.  GetMove should discard all invalid entries and return the valid.
        {
            //Arrange
            Game testBoard = new Game();
            testInput.LinesToRead.Add("P");
            testInput.LinesToRead.Add("0");
            testInput.LinesToRead.Add("9");
            //Act
            int getMoveTest = testBoard.GetMove(testInput);
            //Assert
            Assert.AreEqual<int>(9, getMoveTest);
        }
        [TestMethod]
        public void FourthTime()  // loads the board with a move (X on square 9) and sees if GetMove() will discard an attempt to use 9 again.
        {
            Game testBoard = new Game();
            testBoard.UpdateBoard(9);
            testInput.LinesToRead.Add("9");
            testInput.LinesToRead.Add("8");
            testInput.LinesToRead.Add("7");

            int getMoveTest = testBoard.GetMove(testInput);

            Assert.AreEqual<int>(8, getMoveTest);
        }

        // IsWin unit tests
        [TestMethod]
        public void XWinsTopRow()
        {
            Game testboard = new Game();
            testboard.UpdateBoard(1);
            testboard.UpdateBoard(2);
            testboard.UpdateBoard(3);
            Assert.IsTrue(testboard.IsWin());
        }

        [TestMethod]
        public void OWinsTopRow()
        {
            Game testboard = new Game('O');

            testboard.UpdateBoard(1);
            testboard.UpdateBoard(2);
            testboard.UpdateBoard(3);
            Assert.IsTrue(testboard.IsWin());
        }
        [TestMethod]
        public void OneSquareShort()
        {
            Game testboard = new Game();
            testboard.UpdateBoard(1);
            testboard.UpdateBoard(2);
            Assert.IsFalse(testboard.IsWin());
        }

        [TestMethod]
        public void XWinsMidRow()
        {
            Game testboard = new Game();

            testboard.UpdateBoard(4);
            testboard.UpdateBoard(5);
            testboard.UpdateBoard(6);

            Assert.IsTrue(testboard.IsWin());
        }

        [TestMethod]
        public void XWinsBottomRow()
        {
            Game testboard = new Game();

            testboard.UpdateBoard(7);
            testboard.UpdateBoard(8);
            testboard.UpdateBoard(9);

            Assert.IsTrue(testboard.IsWin());

        }

        [TestMethod]
        public void XWinsLeftColumn()
        {
            Game testboard = new Game();

            testboard.UpdateBoard(1);
            testboard.UpdateBoard(4);
            testboard.UpdateBoard(7);

            Assert.IsTrue(testboard.IsWin());

        }

        [TestMethod]
        public void XWinsMidColumn()
        {
            Game testboard = new Game();

            testboard.UpdateBoard(2);
            testboard.UpdateBoard(5);
            testboard.UpdateBoard(8);

            Assert.IsTrue(testboard.IsWin());

        }

        [TestMethod]
        public void XWinsRightColumn()
        {
            Game testboard = new Game();

            testboard.UpdateBoard(3);
            testboard.UpdateBoard(6);
            testboard.UpdateBoard(9);

            Assert.IsTrue(testboard.IsWin());

        }

        [TestMethod]
        public void XWinsTLDiag()
        {
            Game testboard = new Game();

            testboard.UpdateBoard(1);
            testboard.UpdateBoard(5);
            testboard.UpdateBoard(9);

            Assert.IsTrue(testboard.IsWin());

        }

        [TestMethod]
        public void XWinsBLDiag()
        {
            Game testboard = new Game();

            testboard.UpdateBoard(3);
            testboard.UpdateBoard(5);
            testboard.UpdateBoard(7);

            Assert.IsTrue(testboard.IsWin());

        }

        //ChangePlayer() tests
        [TestMethod]
        public void ItsOTurn()
        {
            Game testboard = new Game();

            testboard.ChangePlayer();

            Assert.AreEqual<char>(testboard.currentPlayer, 'O');
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
    }
}
