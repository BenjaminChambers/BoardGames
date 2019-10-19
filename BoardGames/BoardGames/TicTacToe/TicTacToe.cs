using System.Collections.Generic;

namespace BoardGames.TicTacToe
{
    public class TicTacToe : Board<XO>
    {
        public TicTacToe()
            : base(3, 3) { }
        public TicTacToe(TicTacToe Source)
            : base(Source) { }
        public TicTacToe(IEnumerable<XO> Source)
            : base(3, 3, Source) { }

        private TicTacToe(TicTacToe Source, IEnumerable<(int Index, XO Value)> Changes)
            : base(Source, Changes) { }

        public TicTacToe Play(int Index)
        {
            if (this[Index] != XO.Empty)
                throw new System.InvalidOperationException("Cannot play on non-empty cell");

            var result = Put(Index, Turn);
            result.Turn = Turn switch
            {
                XO.X => XO.O,
                XO.O => XO.X,
                _ => throw new System.InvalidOperationException("Somehow the turn got set to Empty")
            };

            return result;
        }

        private TicTacToe Put(int Index, XO Value)
            => new TicTacToe(this, new[] { (Index, Value) });

        public XO Turn
        {
            get;
            private set;
        } = XO.X;
    }
}
