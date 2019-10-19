using Microsoft.VisualStudio.TestTools.UnitTesting;
using BoardGames.TicTacToe;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class TicTacToe
    {
        readonly XO[] Empty =
            {
            XO.Empty, XO.Empty, XO.Empty,
            XO.Empty, XO.Empty, XO.Empty,
            XO.Empty, XO.Empty, XO.Empty
        };
        readonly XO[] Tie =
        {
            XO.O, XO.X, XO.O,
            XO.X, XO.O, XO.X,
            XO.X, XO.O, XO.X
        };
        readonly XO[] OWinRow1 =
        {
            XO.O, XO.O, XO.O,
            XO.Empty, XO.X, XO.Empty,
            XO.X, XO.X, XO.Empty
        };
        readonly XO[] OWinRow2 =
                {
            XO.X, XO.Empty, XO.Empty,
            XO.O, XO.O, XO.O,
            XO.X, XO.Empty, XO.X
        };
        readonly XO[] OWinRow3 =
        {
            XO.X, XO.Empty, XO.Empty,
            XO.X, XO.X, XO.Empty,
            XO.O, XO.O, XO.O
        };
        readonly XO[] XWinColumn1 =
        {
            XO.X, XO.O, XO.Empty,
            XO.X, XO.O, XO.Empty,
            XO.X, XO.Empty, XO.Empty
        };
        readonly XO[] XWinColumn2 =
        {
            XO.Empty, XO.X, XO.O,
            XO.Empty, XO.X, XO.Empty,
            XO.O, XO.X, XO.Empty
        };
        readonly XO[] XWinColumn3 =
        {
            XO.Empty, XO.Empty, XO.X,
            XO.O, XO.O, XO.X,
            XO.Empty, XO.Empty, XO.X
        };
        readonly XO[] OWinDiagonalDown =
        {
            XO.O, XO.X, XO.X,
            XO.Empty, XO.O, XO.Empty,
            XO.X, XO.Empty, XO.O
        };
        readonly XO[] XWinDiagonalUp =
        {
            XO.O, XO.O, XO.X,
            XO.Empty, XO.X, XO.Empty,
            XO.X, XO.Empty, XO.Empty
        };

        [TestMethod]
        public void TestConstructors()
        {
            static void TestConstruction(XO[] Source)
                => Util.CompareEnum(Source, new BoardGames.TicTacToe.TicTacToe(Source).Cells);

            Util.CompareEnum(Empty, new BoardGames.TicTacToe.TicTacToe().Cells);
            TestConstruction(Empty);
            TestConstruction(Tie);
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
            static void TestRow(XO[] Source)
            {
                var b = new BoardGames.TicTacToe.TicTacToe(Source);
                Util.CompareEnum(b.Row(0), Source.Take(3));
                Util.CompareEnum(b.Row(1), Source.Skip(3).Take(3));
                Util.CompareEnum(b.Row(2), Source.Skip(6));
            }

            TestRow(Empty);
            TestRow(Tie);
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
            static void TestColumn(XO[] Source)
            {
                var b = new BoardGames.TicTacToe.TicTacToe(Source);
                Util.CompareEnum(b.Column(0), new[] { Source[0], Source[3], Source[6] });
                Util.CompareEnum(b.Column(1), new[] { Source[1], Source[4], Source[7] });
                Util.CompareEnum(b.Column(2), new[] { Source[2], Source[5], Source[8] });
            }

            TestColumn(Empty);
            TestColumn(Tie);
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
