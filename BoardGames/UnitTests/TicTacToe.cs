using Microsoft.VisualStudio.TestTools.UnitTesting;
using BoardGames.TicTacToe;

namespace UnitTests
{
    [TestClass]
    public class TicTacToe
    {
        TicTacMove[] Empty = { TicTacMove.Empty, TicTacMove.Empty, TicTacMove.Empty, TicTacMove.Empty, TicTacMove.Empty, TicTacMove.Empty, TicTacMove.Empty, TicTacMove.Empty, TicTacMove.Empty };


        [TestMethod]
        public void TestConstructors()
        {
            Util.CompareEnum(Empty, new BoardGames.TicTacToe.TicTacToe().Cells);
            Util.CompareEnum(Empty, new BoardGames.TicTacToe.TicTacToe(Empty).Cells);
        }
    }
}
