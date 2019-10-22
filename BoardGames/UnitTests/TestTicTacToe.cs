using Microsoft.VisualStudio.TestTools.UnitTesting;
using BoardGames.TicTacToe;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class TestTicTacToe
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
                => Util.CompareEnum(Source, new TicTacToe(Source, XO.X).All);

            Util.CompareEnum(Empty, new BoardGames.TicTacToe.TicTacToe().All);
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
                var b = new BoardGames.TicTacToe.TicTacToe(Source, XO.X);
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
                var b = new TicTacToe(Source, XO.X);
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

        [TestMethod]
        public void TestFlip()
        {
            static void Test(XO[] Source)
            {
                static void TestBoard(TicTacToe Original, TicTacToe Transformed, int[] Locations)
                    => Util.CompareEnum(Transformed.All, from x in Locations select Original[x]);

                var src = new TicTacToe(Source, XO.X);
                TestBoard(src, src.Flip(false, false), new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 });
                TestBoard(src, src.Flip(false, true), new[] { 6, 7, 8, 3, 4, 5, 0, 1, 2 });
                TestBoard(src, src.Flip(true, false), new[] { 2, 1, 0, 5, 4, 3, 8, 7, 6 });
                TestBoard(src, src.Flip(true, true), new[] { 8, 7, 6, 5, 4, 3, 2, 1, 0 });
            }

            Test(Empty);
            Test(Tie);
            Test(OWinRow1);
            Test(OWinRow2);
            Test(OWinRow3);
            Test(XWinColumn1);
            Test(XWinColumn2);
            Test(XWinColumn3);
            Test(OWinDiagonalDown);
            Test(XWinDiagonalUp);
        }

        [TestMethod]
        public void TestRotate()
        {
            static void Test(XO[] Source)
            {
                static void TestBoard(TicTacToe Original, TicTacToe Transformed, int[] Locations)
                    => Util.CompareEnum(Transformed.All, from x in Locations select Original[x]);

                var src = new TicTacToe(Source, XO.X);

                var answers = new[]
                {
                    new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 },
                    new[] { 6, 3, 0, 7, 4, 1, 8, 5, 2 },
                    new[] { 8, 7, 6, 5, 4, 3, 2, 1, 0 },
                    new[] { 2, 5, 8, 1, 4, 7, 0, 3, 6 }
                };

                for (int i = -1; i < 10; i++)
                {
                    for (int offset = 0; offset < 4; offset++)
                        TestBoard(src, src.Rotate(i * 4 + offset), answers[offset]);
                }
            }

            Test(Empty);
            Test(Tie);
            Test(OWinRow1);
            Test(OWinRow2);
            Test(OWinRow3);
            Test(XWinColumn1);
            Test(XWinColumn2);
            Test(XWinColumn3);
            Test(OWinDiagonalDown);
            Test(XWinDiagonalUp);
        }
    }
}
