using System.Collections.Generic;

namespace BoardGames.TicTacToe
{
    public enum TicTacMove { Empty, X, O };

    public class TicTacToe : Board<TicTacMove>
    {
        public TicTacToe()
            : base(3, 3) { }
        public TicTacToe(TicTacToe Source)
            : base(Source) { }
        public TicTacToe(IEnumerable<TicTacMove> Source)
            : base(3, 3, Source) { }

        private TicTacToe(TicTacToe Source, IEnumerable<(int Index, TicTacMove Value)> Changes)
            : base(Source, Changes) { }

        public TicTacToe Put(int Index, TicTacMove Value)
            => new TicTacToe(this, new[] { (Index, Value) });
    }
}
