using BoardGames.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BoardGames.Games.TicTacToe
{
    public class TicTacToe : Board<XO>
    {
        #region Constructors
        public TicTacToe()
            : base(3, 3)
        {
            Turn = XO.X;
            ProcessBoard();
        }

        public TicTacToe(TicTacToe Source)
            : base(Source)
        {
            Turn = Source.Turn;
            ProcessBoard();
        }

        public TicTacToe(IEnumerable<XO> Source, XO Turn)
            : base(3, 3, Source)
        {
            this.Turn = Turn;
            ProcessBoard();
        }

        private TicTacToe(TicTacToe Source, IEnumerable<(int Index, XO Value)> Changes, XO Turn)
            : base(Source, Changes)
        {
            this.Turn = Turn;
            ProcessBoard();
        }
        private TicTacToe(TicTacToe Source, bool FlipHorizontal, bool FlipVertical)
            : base(Source, FlipHorizontal, FlipVertical)
        {
            Turn = Source.Turn;
            ProcessBoard();
        }
        private TicTacToe(TicTacToe Source, int ClockwiseRotations)
            : base(Source, ClockwiseRotations)
        {
            Turn = Source.Turn;
            ProcessBoard();
        }

        private void ProcessBoard()
        {
            bool xWins = Runs(3, XO.X).Any();
            bool oWins = Runs(3, XO.O).Any();
            bool empties = All.Contains(XO.Empty);

            switch (xWins, oWins, empties)
            {
                case (true, false, false):
                case (true, false, true):
                    InProgress = false;
                    Winner = XO.X;
                    break;
                case (false, true, false):
                case (false, true, true):
                    InProgress = false;
                    Winner = XO.O;
                    break;
                case (true, true, false):
                case (true, true, true):
                    throw new InvalidOperationException("Somehow both sides achieved victory.");
                case (false, false, false):
                    InProgress = false;
                    Winner = XO.Empty;
                    break;
                case (false, false, true):
                    InProgress = true;
                    Winner = XO.Empty;
                    break;
            }
        }
        #endregion


        #region Transformation
        public TicTacToe Rotate(int Clockwise)
            => new TicTacToe(this, Clockwise);
        public TicTacToe Flip(bool Horizontal, bool Vertical)
            => new TicTacToe(this, Horizontal, Vertical);
        #endregion

        #region Info
        public readonly XO Turn;

        public bool InProgress { get; private set; }
        public XO Winner { get; private set; }

        #endregion



        private TicTacToe Put(int Index, XO Value)
            => new TicTacToe(this, new[] { (Index, Value) }, (Turn == XO.X) ? XO.O : XO.X);

        public TicTacToe Play(int Index)
        {
            if (this[Index] != XO.Empty)
                throw new System.InvalidOperationException("Cannot play on non-empty cell");

            return Put(Index, Turn);
        }
        public TicTacToe Play(int Column, int Row)
            => Play(Row * 3 + Column);

    }
}
