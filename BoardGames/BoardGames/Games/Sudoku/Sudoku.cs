using System;
using System.Collections.Generic;
using System.Text;

namespace BoardGames.Games.Sudoku
{
    public class Sudoku : Board<int>
    {
        public Sudoku()
            : base(9, 9) { }
    }
}
