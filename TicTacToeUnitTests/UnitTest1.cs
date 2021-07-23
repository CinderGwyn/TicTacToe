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
        public void FourthTime()
        {
            Game testBoard = new Game();
            testBoard.UpdateBoard(9);
            testInput.LinesToRead.Add("9");
            testInput.LinesToRead.Add("8");
            testInput.LinesToRead.Add("7");

            int getMoveTest = testBoard.GetMove(testInput);

            Assert.AreEqual<int>(8, getMoveTest);
        }
    }
}
