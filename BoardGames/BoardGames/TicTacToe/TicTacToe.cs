using System.Collections.Generic;
using System.Linq;

namespace BoardGames.TicTacToe
{
    public class TicTacToe : Board<XO>
    {
        #region Constructors
        public TicTacToe()
            : base(3, 3) => Turn = XO.X;
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
        }

        private TicTacToe(TicTacToe Source, IEnumerable<(int Index, XO Value)> Changes, XO Turn)
            : base(Source, Changes) => this.Turn = Turn;
        private TicTacToe(TicTacToe Source, bool FlipHorizontal, bool FlipVertical)
            : base(Source, FlipHorizontal, FlipVertical) => Turn = Source.Turn;
        private TicTacToe(TicTacToe Source, int ClockwiseRotations)
            : base(Source, ClockwiseRotations) => Turn = Source.Turn;

        private void ProcessBoard()
        {
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

        public readonly bool InProgress;
        public readonly XO Winner;

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
