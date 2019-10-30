using BoardGames.Pieces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoardGames.Games.Chess
{
    class Chessboard : Board<(BlackWhite, ChessPiece)>
    {
        public Chessboard()
            : base(8, 8) { }
    }
}
