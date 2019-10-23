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
            : this((ClockwiseRotations % 2 == 0) ? Source.Width : Source.Height, (ClockwiseRotations % 2 == 0) ? Source.Height : Source.Width)
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
                        0 => Source[x, y],
                        1 => Source[y, Width - 1 - x],
                        2 => Source[Width - 1 - x, Height - 1 - y],
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
            => All[Index];
        public IReadOnlyList<T> All
            => _cells;
        public T this[int Column, int Row]
            => All[Row * Width + Column];
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

        public IEnumerable<Run<T>> Runs(int minLength, T Value)
        {
            foreach (var r in HorizontalRuns(minLength, Value)) yield return r;
            foreach (var r in VerticalRuns(minLength, Value)) yield return r;
            foreach (var r in DiagonalRuns(minLength, Value)) yield return r;
        }

        public IEnumerable<Run<T>> HorizontalRuns(int minLength, T Value)
        {
            for (int r = 0; r < Height; r++)
            {
                int runLength = 0;
                for (int x = 0; x < Width; x++)
                {
                    if (this[x, r].Equals(Value))
                        runLength++;
                    else
                    {
                        if (runLength >= minLength)
                        {
                            yield return new Run<T>() { StartRow = r, EndRow = r, StartColumn = x - runLength - 1, EndColumn = x - 1 };
                        }
                        runLength = 0;
                    }
                }
                if (runLength >= minLength)
                    yield return new Run<T>() { StartRow = r, EndRow = r, StartColumn = Width - runLength - 1, EndColumn = Width - 1 };
            }
        }
        public IEnumerable<Run<T>> VerticalRuns(int minLength, T Value)
        {
            for (int c = 0; c < Width; c++)
            {
                int runLength = 0;
                for (int x = 0; x < Height; x++)
                {
                    if (this[c, x].Equals(Value))
                        runLength++;
                    else
                    {
                        if (runLength >= minLength)
                            yield return new Run<T>() { StartColumn = c, EndColumn = c, StartRow = x - runLength - 1, EndRow = x - 1 };

                        runLength = 0;
                    }
                }
                if (runLength >= minLength)
                    yield return new Run<T>() { StartColumn = c, EndColumn = c, StartRow = Height - runLength - 1, EndRow = Height - 1 };
            }
        }
        public IEnumerable<Run<T>> DiagonalRuns(int minLength, T Value)
        {
            for (int i = 0; i < Height; i++)
            {
                foreach (var run in RunUp(Value, minLength, 0, i))
                    yield return run;
                foreach (var run in RunDown(Value, minLength, 0, i))
                    yield return run;
            }
            for (int i = 1; i < Width; i++)
            {
                foreach (var run in RunUp(Value, minLength, i, 0))
                    yield return run;
                foreach (var run in RunDown(Value, minLength, i, Height - 1))
                    yield return run;
            }
        }

        private IEnumerable<Run<T>> RunUp(T Value, int minLength, int StartingColumn, int StartingRow)
        {
            int runLength = 0;
            int x = StartingColumn;
            int y = StartingRow;

            while (x < Width && y < Height)
            {
                if (this[x, y].Equals(Value))
                    runLength++;
                else
                {
                    if (runLength >= minLength)
                        yield return new Run<T> { StartColumn = StartingColumn, StartRow = StartingRow, EndColumn = x - 1, EndRow = y - 1, Value = Value };
                    runLength = 0;
                }
                x++;
                y++;
            }
            if (runLength >= minLength)
                yield return new Run<T> { StartColumn = StartingColumn, StartRow = StartingRow, EndColumn = x - 1, EndRow = y - 1, Value = Value };
        }

        private IEnumerable<Run<T>> RunDown(T Value, int minLength, int StartingColumn, int StartingRow)
        {
            int runLength = 0;
            int x = StartingColumn;
            int y = StartingRow;

            while (x < Width && y >= 0)
            {
                if (this[x, y].Equals(Value))
                    runLength++;
                else
                {
                    if (runLength >= minLength)
                        yield return new Run<T> { StartColumn = StartingColumn, StartRow = StartingRow, EndColumn = x - 1, EndRow = y - 1, Value = Value };
                    runLength = 0;
                }
                x++;
                y--;
            }
            if (runLength >= minLength)
                yield return new Run<T> { StartColumn = StartingColumn, StartRow = StartingRow, EndColumn = x - 1, EndRow = y - 1, Value = Value };
        }





        public readonly int Width;
        public readonly int Height;

        readonly private T[] _cells;
    }
}
