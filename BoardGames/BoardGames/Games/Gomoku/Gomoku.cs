using BoardGames.Games.Pieces;
using BoardGames.Pieces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoardGames.Games.Gomoku
{
    public class Gomoku : Board<Stone>
    {
        public Gomoku()
            : base(19, 19) => Turn = Stone.Black;
        public Gomoku(Gomoku Source)
            : base(Source) => Turn = Source.Turn;
        public Gomoku(IEnumerable<Stone> Source, Stone Turn)
            : base(3, 3, Source) => this.Turn = Turn;


        private Gomoku(Gomoku Source, IEnumerable<(int Index, Stone Value)> Changes, Stone Turn)
            : base(Source, Changes) => this.Turn = Turn;
        private Gomoku(Gomoku Source, bool FlipHorizontal, bool FlipVertical)
            : base(Source, FlipHorizontal, FlipVertical) => Turn = Source.Turn;
        private Gomoku(Gomoku Source, int ClockwiseRotations)
            : base(Source, ClockwiseRotations) => Turn = Source.Turn;


        public Gomoku Rotate(int Clockwise)
            => new Gomoku(this, Clockwise);
        public Gomoku Flip(bool Horizontal, bool Vertical)
            => new Gomoku(this, Horizontal, Vertical);


        public Gomoku Play(int Index)
        {
            if (All[Index] != Stone.Empty)
                throw new InvalidOperationException("Cannot place stone in non-empty cell.");

            return new Gomoku(this, new[] { (Index, Turn) }, (Turn == Stone.Black) ? Stone.White : Stone.Black);
        }
        public Gomoku Play(int Column, int Row)
            => Play(Row * 19 + Column);


        public readonly Stone Turn;
    }
}
