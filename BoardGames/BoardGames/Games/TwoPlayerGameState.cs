namespace BoardGames.Games
{
    /// <summary>
    /// State enum for games played between two players.
    /// </summary>
    public enum TwoPlayerGameState
    {
        /// <summary>
        /// Used when the game state doesn't make sense (ie wrong number of pieces or both players winning).
        /// </summary>
        Invalid,

        /// <summary>
        /// The game has not yet ended.
        /// </summary>
        InProgress,

        /// <summary>
        /// The game has ended without a winner.
        /// </summary>
        Tie,

        /// <summary>
        /// The game has ended with the first player winning.
        /// </summary>
        PlayerOneWins,

        /// <summary>
        /// The game has ended with the second player winning.
        /// </summary>
        PlayerTwoWins,
    }
}
