namespace BoardGames
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// A smart grid, including functions for transforming as necessary.
    /// </summary>
    /// <typeparam name="T">The type of value in each cell.</typeparam>
    public class Grid<T>
    {
        private readonly int width;

        private readonly int height;

        private readonly T[] cells;

        /// <summary>
        /// Initializes a new instance of the <see cref="Grid{T}"/> class.
        /// Every cell is filled with the default value of T.
        /// </summary>
        /// <param name="width">The width of the <see cref="Grid{T}"/> instance.</param>
        /// <param name="height">The height of the <see cref="Grid{T}"/> instance.</param>
        public Grid(int width, int height)
        {
            this.width = width;
            this.height = height;
            this.cells = new T[width * height];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Grid{T}"/> class.
        /// The value of every cell is supplied.
        /// </summary>
        /// <param name="width">The width of the <see cref="Grid{T}"/> instance.</param>
        /// <param name="height">The height of the <see cref="Grid{T}"/> instance.</param>
        /// <param name="source">An array of T, which must contain width*height values.</param>
        public Grid(int width, int height, T[] source)
            : this(width, height)
        {
            if (source.Length != width * height)
            {
                throw new ArgumentException($"Expected array of {width * height} values, received array of {source.Length}");
            }

            Array.Copy(source, this.cells, source.Length);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Grid{T}"/> class.
        /// </summary>
        /// <param name="source">The <see cref="Grid{T}"/> to copy.</param>
        public Grid(Grid<T> source)
            : this(source.Width, source.Height)
            => Array.Copy(source.cells, this.cells, this.cells.Length);

        /// <summary>
        /// Initializes a new instance of the <see cref="Grid{T}"/> class, performing a deep copy of the original data, and applying the list of changes specified.
        /// </summary>
        /// <param name="source">The original <see cref="Grid{T}"/> to copy.</param>
        /// <param name="changes">The list of chagnes to apply.</param>
        public Grid(Grid<T> source, IEnumerable<(int Index, T Value)> changes)
            : this(source)
        {
            foreach (var (index, value) in changes)
                this.cells[index] = value;
        }

        /// <summary>
        /// Gets the Width of the <see cref="Grid{T}"/>.
        /// </summary>
        public int Width
            => this.width;

        /// <summary>
        /// Gets the Height of the <see cref="Grid{T}"/>.
        /// </summary>
        public int Height
            => this.height;

        /// <summary>
        /// Access the cell at a specified index.
        /// </summary>
        /// <param name="index">The index to access.</param>
        /// <returns>T.</returns>
        public T this[int index]
        {
            get => this.cells[index];
        }

        /// <summary>
        /// Access the cell at the specified grid coordinates.
        /// </summary>
        /// <param name="column">The column to access.</param>
        /// <param name="row">The row to access.</param>
        /// <returns>T.</returns>
        public T this[int column, int row]
        {
            get => this.cells[(row * this.Width) + column];
        }

        /// <summary>
        /// Enumerates over every cell in the <see cref="Grid{T}"/>.
        /// </summary>
        /// <returns>Every cell in order.</returns>
        public IEnumerable<T> All()
        {
            foreach (var cell in this.cells)
            {
                yield return cell;
            }
        }

        /// <summary>
        /// Enumerates over every cell in a particular row.
        /// </summary>
        /// <param name="row">The row to enumerate.</param>
        /// <returns>The cell values.</returns>
        public IEnumerable<T> Row(int row)
        {
            for (int i = row * this.Width; i < (row + 1) * this.Width; i++)
            {
                yield return this.cells[i];
            }
        }

        /// <summary>
        /// Enumerates over ever cell in a particular column.
        /// </summary>
        /// <param name="column">The column to enumerate.</param>
        /// <returns>The cell values.</returns>
        public IEnumerable<T> Column(int column)
        {
            for (int i = column; i < this.cells.Length; i += this.Width)
            {
                yield return this.cells[i];
            }
        }

        /// <summary>
        /// Returns a new <see cref="Grid{T}"/> rotated by the specified number of turns.
        /// </summary>
        /// <param name="clockwiseRotations">The number of clockwise rotations to perform.
        /// To rotate counter-clockwise, specify a negative number.
        /// Values greater than 3 will be modulated by 4.</param>
        /// <returns>A new <see cref="Grid{T}"/> with the rotated data.</returns>
        public Grid<T> Rotate(int clockwiseRotations)
        {
            var result = new T[this.cells.Length];
            (int newWidth, int newHeight) = (clockwiseRotations % 2 == 0) ? (this.Width, this.Height) : (this.Height, this.Width);

            var rot = clockwiseRotations % 4;
            if (rot < 0)
            {
                rot += 4;
            }

            for (int x1 = 0; x1 < this.Width; x1++)
            {
                for (int y1 = 0; y1 < this.Height; y1++)
                {
                    (int x2, int y2) = rot switch
                    {
                        0 => (x1, y1),
                        1 => (newWidth - y1 - 1, x1),
                        2 => (newWidth - x1 - 1, newHeight - y1 - 1),
                        3 => (y1, newHeight - x1 - 1),
                        _ => throw new InvalidOperationException($"Trying to rotate by {rot} turns after using %4... How did that happen?")
                    };

                    result[(y2 * newWidth) + x2] = this[x1, y1];
                }
            }

            return new Grid<T>(newWidth, newHeight, result);
        }

        /// <summary>
        /// Returns a new instance of the <see cref="Grid{T}"/> class, performing a deep copy of the original, but flipped along the horizontal and/or vertical axes.
        /// </summary>
        /// <param name="horizontal">Whether or not to flip along the horizontal axis.</param>
        /// <param name="vertical">Whether or not to flip along the vertical axis.</param>
        /// <returns>A new instance of the <see cref="Grid{T}"/> class with the modified data.</returns>
        public Grid<T> Flip(bool horizontal, bool vertical)
        {
            var result = new T[this.cells.Length];
            for (int i = 0; i < this.Width; i++)
            {
                for (int j = 0; j < this.Height; j++)
                {
                    (int x, int y) = (horizontal, vertical) switch
                    {
                        (false, false) => (i, j),
                        (false, true) => (i, this.Height - j - 1),
                        (true, false) => (this.Width - i - 1, j),
                        (true, true) => (this.Width - i - 1, this.Height - j - 1)
                    };

                    result[(j * this.Width) + i] = this[x, y];
                }
            }

            return new Grid<T>(this.Width, this.Height, result);
        }

        /// <summary>
        /// Swaps the horizontal and vertical axes in a <see cref="Grid{T}"/>.
        /// </summary>
        /// <returns>A new instance with the modified data.</returns>
        public Grid<T> Transpose()
        {
            var result = new T[this.cells.Length];
            for (int i = 0; i < this.Width; i++)
            {
                for (int j = 0; j < this.Height; j++)
                {
                    result[(i * this.Height) + j] = this[i, j];
                }
            }

            return new Grid<T>(this.Height, this.Width, result);
        }

        /// <summary>
        /// Searches for all runs in a grid of a minimum length.
        /// </summary>
        /// <param name="minimumLength">The minimum length run to return. Setting to 1 will include a large number of duplicate singles.</param>
        /// <returns>An <see cref="IEnumerable{Run}"/> query of the results.</returns>
        public IEnumerable<Run<T>> Runs(int minimumLength)
        {
            var runs = new List<IEnumerable<Run<T>>>();

            for (int i = 0; i < this.Width; i++)
            {
                runs.Add(this.Run8(i, minimumLength));
            }

            for (int i = 0; i < this.Height; i++)
            {
                runs.Add(this.Run6(i, minimumLength));
            }

            for (int i = 0; i < this.Width - 1; i++)
            {
                runs.Add(this.Run9(i, 0, minimumLength));
                runs.Add(this.Run3(i, this.Height - 1, minimumLength));
            }

            for (int i = 1; i < this.Height - 1; i++)
            {
                runs.Add(this.Run9(0, i, minimumLength));
                runs.Add(this.Run3(0, i, minimumLength));
            }

            foreach (var run in runs.SelectMany(item => item))
            {
                if (run.Length >= minimumLength)
                {
                    yield return run;
                }
            }
        }

        /// <summary>
        /// Evaluates a single row and returns every run of a minimum length.
        /// </summary>
        /// <param name="row">The row to evaluate.</param>
        /// <param name="minimumLength">The minimum length of run to return.</param>
        /// <returns>Every instance of <see cref="Run{T}"/> that meet the length criteria.</returns>
        public IEnumerable<Run<T>> Run6(int row, int minimumLength)
        {
            var val = this[0, row];
            var start = 0;

            for (int c = 1; c < this.Width; c++)
            {
                if (!this[c, row].Equals(val))
                {
                    if (c - start >= minimumLength)
                    {
                        yield return new Run<T>(start, row, c - 1, row, val);
                    }

                    start = c;
                    val = this[c, row];
                }
            }

            if (this.Width - start >= minimumLength)
            {
                yield return new Run<T>(start, row, this.Width - 1, row, val);
            }
        }

        /// <summary>
        /// Evaluates a single column and returns every run of a minimum length.
        /// </summary>
        /// <param name="column">The column to evaluate.</param>
        /// <param name="minimumLength">The minimum length of run to return.</param>
        /// <returns>Every instance of <see cref="Run{T}"/> that meet the length criteria.</returns>
        public IEnumerable<Run<T>> Run8(int column, int minimumLength)
        {
            var val = this[column, 0];
            var start = 0;

            for (int r = 1; r < this.Height; r++)
            {
                if (!this[column, r].Equals(val))
                {
                    if (r - start >= minimumLength)
                    {
                        yield return new Run<T>(column, start, column, r - 1, val);
                    }

                    start = r;
                    val = this[column, r];
                }
            }

            if (this.Height - start >= minimumLength)
            {
                yield return new Run<T>(column, start, column, this.Height - 1, val);
            }
        }

        /// <summary>
        /// Evaluates a diagonal (increasing both X and Y coordinates) and returns every <see cref="Run{T}"/> of a minimum length.
        /// </summary>
        /// <param name="startColumn">The column to start in.</param>
        /// <param name="startRow">The row to start in.</param>
        /// <param name="minimumLength">The minimum length.</param>
        /// <returns>Every <see cref="Run{T}"/> found that meets the criteria.</returns>
        public IEnumerable<Run<T>> Run9(int startColumn, int startRow, int minimumLength)
        {
            var val = this[startColumn, startRow];
            int sc = startColumn;
            int sr = startRow;

            int c = sc + 1;
            int r = sr + 1;
            while (c < this.Width && r < this.Height)
            {
                if (!this[c, r].Equals(val))
                {
                    if (c - sc >= minimumLength)
                    {
                        yield return new Run<T>(sc, sr, c - 1, r - 1, val);
                    }

                    sc = c;
                    sr = r;
                    val = this[c, r];
                }

                c++;
                r++;
            }

            if (c - sc >= minimumLength)
            {
                yield return new Run<T>(sc, sr, c - 1, r - 1, val);
            }
        }

        /// <summary>
        /// Evaluates a diagonal (increasing X and decreasing Y) and returns every <see cref="Run{T}"/> of a minimum length.
        /// </summary>
        /// <param name="startColumn">The column to start in.</param>
        /// <param name="startRow">The row to start in.</param>
        /// <param name="minimumLength">The minimum length.</param>
        /// <returns>Every <see cref="Run{T}"/> found that meets the criteria.</returns>
        public IEnumerable<Run<T>> Run3(int startColumn, int startRow, int minimumLength)
        {
            var val = this[startColumn, startRow];
            int sc = startColumn;
            int sr = startRow;

            int c = sc + 1;
            int r = sr - 1;
            while (c < this.Width && r >= 0)
            {
                if (!this[c, r].Equals(val))
                {
                    if (c - sc >= minimumLength)
                    {
                        yield return new Run<T>(sc, sr, c - 1, r + 1, val);
                    }

                    sc = c;
                    sr = r;
                    val = this[sc, sr];
                }

                c++;
                r--;
            }

            if (c - sc >= minimumLength)
            {
                yield return new Run<T>(sc, sr, c - 1, 0, val);
            }
        }
    }
}
