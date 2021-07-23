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
            
            testInput.LinesToRead.Add("9");
            //Act
            int getMoveTest = TicTacToe.Game.GetMove(testInput);
            //Assert
            Assert.AreEqual<int>(9, getMoveTest);
        }
        [TestMethod]
        public void ThirdTimesTheCharm()
        {
            //Arrange
            testInput.LinesToRead.Add("P");
            testInput.LinesToRead.Add("G");
            testInput.LinesToRead.Add("9");
            //Act
            int getMoveTest = TicTacToe.Game.GetMove(testInput);
            //Assert
            Assert.AreEqual<int>(9, getMoveTest);
        }
    }
}
