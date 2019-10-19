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

        public TicTacToe Put(int Index, XO Value)
            => new TicTacToe(this, new[] { (Index, Value) });
    }
}
