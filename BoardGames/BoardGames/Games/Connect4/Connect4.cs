using BoardGames.Pieces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoardGames.Games.Connect4
{
    class Connect4 : Board<Checker>
    {
        public Connect4()
            : base(7, 6) { }
    }
}
