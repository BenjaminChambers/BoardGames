using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BoardGames
{
    public class Board<T>
    {
        public Board(int Width, int Height)
        {
            this.Width = Width;
            this.Height = Height;
            var a = new T[Width * Height];
            Cells = new ReadOnlyCollection<T>(a);
        }
        public Board(int Width, int Height, IList<T> Source)
        {
            this.Width = Width;
            this.Height = Height;
            Cells = new ReadOnlyCollection<T>(Source);
        }

        public readonly int Width;
        public readonly int Height;
        public ReadOnlyCollection<T> Cells;
    }
}
