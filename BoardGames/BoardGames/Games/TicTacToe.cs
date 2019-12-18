namespace BoardGames.Games
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A game of Tic-Tac-Toe, consisting of players placing Xs and Os on a 3x3 grid until one has achieved 3 in a row.
    /// </summary>
    public class TicTacToe : IAdversarialSinglePiece
    {
        private readonly List<(int Index, Grid<int> Result)> history;
        private bool turn;
        private TwoPlayerGameState state;


        /// <summary>
        /// Initializes a new instance of the <see cref="TicTacToe"/> class.
        /// </summary>
        public TicTacToe()
        {
            this.turn = true;
            this.state = TwoPlayerGameState.InProgress;
            this.history = new List<(int, Grid<int>)>
            {
                (0, new Grid<int>(3, 3)),
            };
        }

        public bool Turn => this.turn;

        public TwoPlayerGameState State => this.state;

        public IReadOnlyList<(int Index, Grid<int> Result)> History => this.history;

        public Grid<int> Current => this.History.Last().Result;

        /// <summary>
        /// Places an X or O in the given location.
        /// Adds the move and the new board to the History.
        /// Computes a new game state.
        /// </summary>
        /// <param name="index">The location to play in.</param>
        public void Play(int index)
        {
            if (this.Current[index] != 0)
            {
                throw new ArgumentException("Cannot play on a non-empty space.");
            }

            if (this.Turn)
            {
                this.history.Add((index, new Grid<int>(this.Current, new[] { (index, 1) })));
            }
            else
            {
                this.history.Add((index, new Grid<int>(this.Current, new[] { (index, 2) })));
            }

            this.state = TwoPlayerGameState.InProgress;
            this.turn = !this.turn;
            var runs = this.Current.Runs(3);
            if (runs.Any())
            {
                var x = (from run in runs
                         where run.Value == 1
                         select 1).Any();

                var o = (from run in runs
                         where run.Value == 2
                         select 1).Any();

                this.state = (x, o) switch
                {
                    (false, false) => TwoPlayerGameState.InProgress,
                    (false, true) => TwoPlayerGameState.PlayerTwoWins,
                    (true, false) => TwoPlayerGameState.PlayerOneWins,
                    (true, true) => TwoPlayerGameState.Invalid
                };
            }
            else
            {
                if (!this.Current.All().Contains(0))
                {
                    this.state = TwoPlayerGameState.Tie;
                }
            }
        }

        void IAdversarialSinglePiece.Play(int index)
        {
            throw new NotImplementedException();
        }
    }
}
