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
        protected Board(int Width, int Height, IEnumerable<T> Values)
            : this(Width, Height)
        {
            int i = 0;
            foreach (var val in Values)
                _cells[i++] = val;
        }

        public T this[int Index]
            => Cells[Index];
        public T this[int Column, int Row]
            => Cells[Row*Width + Column];
        public IEnumerable<T> Row(int RowNumber)
        {
            for (int i = RowNumber * Width; i < (RowNumber + 1) * Width; i++)
                yield return this[i];
        }
        public IEnumerable<T> Column(int ColumnNumber)
        {
            for (int i = ColumnNumber; i < Width * Height; i += Width)
                yield return this[i];
        }

        public readonly int Width;
        public readonly int Height;
        public IReadOnlyList<T> Cells
            => _cells;

        private T[] _cells;
    }
}
