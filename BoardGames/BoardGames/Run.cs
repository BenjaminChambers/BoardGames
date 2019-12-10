namespace BoardGames
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Represents a string of identical values through a <see cref="Grid{T}"/>, along a straight horizontal, vertical, or diagonal line.
    /// </summary>
    /// <typeparam name="T">The type held by cells in the <see cref="Grid{T}"/>.</typeparam>
    public struct Run<T>
    {
        /// <summary>The Row coordinate of the first end.</summary>
        public readonly int StartRow;

        /// <summary>The Column coordinate of the first end.</summary>
        public readonly int StartColumn;

        /// <summary>The Row coordinate of the second end.</summary>
        public readonly int EndRow;

        /// <summary>The Column coordinate of the second end.</summary>
        public readonly int EndColumn;

        /// <summary>The value of the cells in the <see cref="Run{T}"/>.</summary>
        public readonly T Value;

        /// <summary>The length of the <see cref="Run{T}"/>.</summary>
        public readonly int Length;

        /// <summary>
        /// Initializes a new instance of the <see cref="Run{T}"/> struct.
        /// </summary>
        /// <param name="startColumn">The column where the <see cref="Run{T}"/> begins.</param>
        /// <param name="startRow">The row where the <see cref="Run{T}"/> begins.</param>
        /// <param name="endColumn">The column where the <see cref="Run{T}"/> ends.</param>
        /// <param name="endRow">The row where the <see cref="Run{T}"/> ends.</param>
        /// <param name="value">The value found in the run.</param>
        public Run(int startColumn, int startRow, int endColumn, int endRow, T value)
        {
            if (startColumn + startRow <= endColumn + endRow)
            {
                this.StartColumn = startColumn;
                this.StartRow = startRow;
                this.EndColumn = endColumn;
                this.EndRow = endRow;
            }
            else
            {
                this.StartColumn = endColumn;
                this.StartRow = endRow;
                this.EndColumn = startColumn;
                this.EndRow = startRow;
            }

            this.Value = value;

            var rLength = (endRow > startRow) ? endRow - startRow : startRow - endRow;
            var cLength = (endColumn > startColumn) ? endColumn - startColumn : startColumn - endColumn;

            this.Length = ((rLength > cLength) ? rLength : cLength) + 1;
        }

        /// <summary>
        /// Tests for equality by comparing the terminals of the <see cref="Run{T}"/>s.
        /// </summary>
        /// <param name="first">The first <see cref="Run{T}"/> to compare.</param>
        /// <param name="second">The second <see cref="Run{T}"/> to compare.</param>
        /// <returns>True if the <see cref="Run{T}"/>s are identical, or false if they are not.</returns>
        public static bool operator ==(Run<T> first, Run<T> second)
            => first.StartColumn == second.StartColumn && first.StartRow == second.StartRow && first.EndColumn == second.EndColumn && first.EndRow == second.EndRow;

        /// <summary>
        /// Tets for inequality by comparing the terminals of the <see cref="Run{T}"/>s.
        /// </summary>
        /// <param name="first">The first <see cref="Run{T}"/> to compare.</param>
        /// <param name="second">The second <see cref="Run{T}"/> to compare.</param>
        /// <returns>False if the <see cref="Run{T}"/>s are identical, or False if they are not.</returns>
        public static bool operator !=(Run<T> first, Run<T> second)
            => first.StartColumn != second.StartColumn || first.StartRow != second.StartRow || first.EndColumn != second.EndColumn || first.EndRow != second.EndRow;

        /// <summary>
        /// Override the Equals function.
        /// </summary>
        /// <param name="obj">The object to compare to.</param>
        /// <returns>True if the second object is also a <see cref="Run{T}"/> and equal to this, or false if they are not.</returns>
        public override bool Equals(object obj)
            => this == (Run<T>)obj;

        /// <summary>
        /// Override the GetHashCode function.
        /// </summary>
        /// <returns>A hash code used for sorting into buckets.</returns>
        public override int GetHashCode()
            => (this.StartRow * 59) ^ (this.StartColumn * 157) ^ (this.EndRow * 1051) ^ (this.EndColumn * 2719);
    }
}
