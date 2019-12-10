namespace BoardGames.Games
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Games consisting of two players each placing a single type of piece on the board.
    /// Tic-Tac-Toe, Connect4, and Go, are representative of this type of game.
    /// </summary>
    public interface IAdversarialSinglePiece
    {
        /// <summary>
        /// Gets a value indicating whether it is the first player's turn or not.
        /// </summary>
        bool Turn { get; }

        /// <summary>
        /// Gets a value indicating the current state of play.
        /// </summary>
        TwoPlayerGameState State { get; }

        /// <summary>
        /// Gets the history of the game, consisting of every move and the resulting board state after that move.
        /// </summary>
        IReadOnlyList<(int Index, Grid<int> Result)> History { get; }

        /// <summary>
        /// Gets the current game board.
        /// </summary>
        Grid<int> Current => this.History.Last().Result;

        /// <summary>
        /// Makes a play for the current player at the specified index.
        /// </summary>
        /// <param name="index">The location to place a piece.</param>
        void Play(int index);
    }
}
