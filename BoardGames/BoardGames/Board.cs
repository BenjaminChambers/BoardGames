using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace BoardGames
{
    public abstract class Board<T>
    {
        protected Board(int Width, int Height)
        {
            this.Width = Width;
            this.Height = Height;
            _cells = new T[Width * Height];
        }
        protected Board(Board<T> Source)
            : this(Source.Width, Source.Height)
            => Array.Copy(Source._cells, _cells, _cells.Length);
        protected Board(Board<T> Source, IEnumerable<(int Index, T Value)> Changes)
            : this(Source)
        {
            foreach (var (Index, Value) in Changes)
                _cells[Index] = Value;
        }

        public readonly int Width;
        public readonly int Height;
        public IReadOnlyList<T> Cells;

        private T[] _cells;
    }
}
