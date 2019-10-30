using BoardGames.Pieces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoardGames.Games.Checkers
{
    class Checkerboard : Board<Checker>
    {
        public Checkerboard()
            : base(8, 8) { }
    }
}
