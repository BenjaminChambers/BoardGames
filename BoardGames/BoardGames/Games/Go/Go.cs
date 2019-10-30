using BoardGames.Games.Pieces;
using BoardGames.Pieces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoardGames.Games.Go
{
    class Go : Board<Stone>
    {
        public Go()
            : base(19, 19) { }
    }
}
