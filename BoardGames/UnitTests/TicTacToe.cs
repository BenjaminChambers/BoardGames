using Microsoft.VisualStudio.TestTools.UnitTesting;
using BoardGames.TicTacToe;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class TicTacToe
    {
        TicTacMove[] Empty =
            {
            TicTacMove.Empty, TicTacMove.Empty, TicTacMove.Empty,
            TicTacMove.Empty, TicTacMove.Empty, TicTacMove.Empty,
            TicTacMove.Empty, TicTacMove.Empty, TicTacMove.Empty
        };
        TicTacMove[] Tie =
        {
            TicTacMove.O, TicTacMove.X, TicTacMove.O,
            TicTacMove.X, TicTacMove.O, TicTacMove.X,
            TicTacMove.X, TicTacMove.O, TicTacMove.X
        };
        TicTacMove[] OWinRow1 =
        {
            TicTacMove.O, TicTacMove.O, TicTacMove.O,
            TicTacMove.Empty, TicTacMove.X, TicTacMove.Empty,
            TicTacMove.X, TicTacMove.X, TicTacMove.Empty
        };
        TicTacMove[] OWinRow2 =
                {
            TicTacMove.X, TicTacMove.Empty, TicTacMove.Empty,
            TicTacMove.O, TicTacMove.O, TicTacMove.O,
            TicTacMove.X, TicTacMove.Empty, TicTacMove.X
        };
        TicTacMove[] OWinRow3 =
        {
            TicTacMove.X, TicTacMove.Empty, TicTacMove.Empty,
            TicTacMove.X, TicTacMove.X, TicTacMove.Empty,
            TicTacMove.O, TicTacMove.O, TicTacMove.O
        };
        TicTacMove[] XWinColumn1 =
        {
            TicTacMove.X, TicTacMove.O, TicTacMove.Empty,
            TicTacMove.X, TicTacMove.O, TicTacMove.Empty,
            TicTacMove.X, TicTacMove.Empty, TicTacMove.Empty
        };
        TicTacMove[] XWinColumn2 =
        {
            TicTacMove.Empty, TicTacMove.X, TicTacMove.O,
            TicTacMove.Empty, TicTacMove.X, TicTacMove.Empty,
            TicTacMove.O, TicTacMove.X, TicTacMove.Empty
        };
        TicTacMove[] XWinColumn3 =
        {
            TicTacMove.Empty, TicTacMove.Empty, TicTacMove.X,
            TicTacMove.O, TicTacMove.O, TicTacMove.X,
            TicTacMove.Empty, TicTacMove.Empty, TicTacMove.X
        };
        TicTacMove[] OWinDiagonalDown =
        {
            TicTacMove.O, TicTacMove.X, TicTacMove.X,
            TicTacMove.Empty, TicTacMove.O, TicTacMove.Empty,
            TicTacMove.X, TicTacMove.Empty, TicTacMove.O
        };
        TicTacMove[] XWinDiagonalUp =
        {
            TicTacMove.O, TicTacMove.O, TicTacMove.X,
            TicTacMove.Empty, TicTacMove.X, TicTacMove.Empty,
            TicTacMove.X, TicTacMove.Empty, TicTacMove.Empty
        };

        [TestMethod]
        public void TestConstructors()
        {
            void TestConstruction(TicTacMove[] Source)
                => Util.CompareEnum(Source, new BoardGames.TicTacToe.TicTacToe(Source).Cells);

            Util.CompareEnum(Empty, new BoardGames.TicTacToe.TicTacToe().Cells);
            TestConstruction(Empty);
            TestConstruction(OWinRow1);
            TestConstruction(OWinRow2);
            TestConstruction(OWinRow3);
            TestConstruction(XWinColumn1);
            TestConstruction(XWinColumn2);
            TestConstruction(XWinColumn3);
            TestConstruction(OWinDiagonalDown);
            TestConstruction(XWinDiagonalUp);
        }

        [TestMethod]
        public void TestRows()
        {
            void TestRow(TicTacMove[] Source)
            {
                var b = new BoardGames.TicTacToe.TicTacToe(Source);
                Util.CompareEnum(b.Row(0), Source.Take(3));
                Util.CompareEnum(b.Row(1), Source.Skip(3).Take(3));
                Util.CompareEnum(b.Row(2), Source.Skip(6));
            }

            TestRow(Empty);
            TestRow(OWinRow1);
            TestRow(OWinRow2);
            TestRow(OWinRow3);
            TestRow(XWinColumn1);
            TestRow(XWinColumn2);
            TestRow(XWinColumn3);
            TestRow(OWinDiagonalDown);
            TestRow(XWinDiagonalUp);
        }

        [TestMethod]
        public void TestColumns()
        {
            void TestColumn(TicTacMove[] Source)
            {
                var b = new BoardGames.TicTacToe.TicTacToe(Source);
                Util.CompareEnum(b.Column(0), new[] { Source[0], Source[3], Source[6] });
                Util.CompareEnum(b.Column(1), new[] { Source[1], Source[4], Source[7] });
                Util.CompareEnum(b.Column(2), new[] { Source[2], Source[5], Source[8] });
            }

            TestColumn(Empty);
            TestColumn(OWinRow1);
            TestColumn(OWinRow2);
            TestColumn(OWinRow3);
            TestColumn(XWinColumn1);
            TestColumn(XWinColumn2);
            TestColumn(XWinColumn3);
            TestColumn(OWinDiagonalDown);
            TestColumn(XWinDiagonalUp);
        }
    }
}
