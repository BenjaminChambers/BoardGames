using BoardGames.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoardGames.Games.Connect4
{
    public class Connect4 : Board<Checker>
    {
        public Connect4()
            : base(7, 6)
        {
            Turn = Checker.Red;
        }

        private Connect4(Connect4 Source, IEnumerable<(int Index, Checker Value)> Changes, Checker Turn)
            : base(Source, Changes) { this.Turn = Turn; }

        public Connect4 Play(int Column)
            => new Connect4(this,
                            new[] { ((from r in Enumerable.Range(0, 6) where this[Column, r] == Checker.Empty select r).First()*7+Column, Turn)},
                            (Turn == Checker.Red) ? Checker.Black : Checker.Red);

        public readonly Checker Turn;
    }
}
