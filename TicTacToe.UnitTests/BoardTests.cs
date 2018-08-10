using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TTT.ViewModel;

namespace TicTacToe.UnitTests
{
    [TestClass]
    public class BoardTests
    {
        [TestMethod]
        public void CheckWinner_TopRowWin_ReturnsTrue()
        {
            // Arrange
            var board = new Board();
            board.Test_SetTileValues("X", "X", "X", "", "", "", "", "", "");

            // Act
            var result = board.CheckWinner();

            // Assert
            Assert.IsTrue(result);
        }
    }
}
