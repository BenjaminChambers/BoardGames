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
        protected Board(Board<T> Source, int ClockwiseRotations)
            : this((ClockwiseRotations % 2 == 0) ? Source.Width : Source.Height, (ClockwiseRotations % 2 == 0) ? Source.Height:Source.Width)
        {
            var rot = ClockwiseRotations % 4;
            if (rot < 0)
                rot += 4;

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    _cells[y * Width + x] = rot switch
                    {
                        0 => Source[x,y],
                        1 => Source[y, Width - 1 - x],
                        2 => Source[Width-1-x, Height-1-y],
                        3 => Source[Height - 1 - y, x],
                        _ => throw new InvalidOperationException("How the hell did that happen?")
                    };
                }
            }
        }
        protected Board(Board<T> Source, bool FlipHorizontal, bool FlipVertical)
            : this(Source.Width, Source.Height)
        {
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    _cells[j * Width + i] = (FlipHorizontal, FlipVertical) switch
                    {
                        (false, false) => Source[i, j],
                        (false, true) => Source[i, Height - 1 - j],
                        (true, false) => Source[Width - 1 - i, j],
                        (true, true) => Source[Width - 1 - i, Height - 1 - j]
                    };
                }
            }
        }

        public T this[int Index]
            => Cells[Index];
        public T this[int Column, int Row]
            => Cells[Row * Width + Column];
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
