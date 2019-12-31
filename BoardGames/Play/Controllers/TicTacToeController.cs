namespace Play.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Windows;
    using BoardGames.Games;

    /// <summary>
    /// An instance of the binding target of a Tic Tac Toe board.
    /// </summary>
    public class TicTacToeController
    {
        private readonly TicTacToe game;
        private readonly BindingValue<Visibility>[] xVisibility;
        private readonly BindingValue<Visibility>[] oVisibility;
        private readonly BindingValue<Visibility>[] eVisibility;

        /// <summary>
        /// Initializes a new instance of the <see cref="TicTacToeController"/> class.
        /// </summary>
        public TicTacToeController()
        {
            this.game = new BoardGames.Games.TicTacToe();
            this.StatusText = new BindingValue<string>();
            this.xVisibility = new BindingValue<Visibility>[10];
            this.oVisibility = new BindingValue<Visibility>[10];
            this.eVisibility = new BindingValue<Visibility>[10];
            for (int i = 0; i < 10; i++)
            {
                this.eVisibility[i] = new BindingValue<Visibility>();
                this.xVisibility[i] = new BindingValue<Visibility>();
                this.oVisibility[i] = new BindingValue<Visibility>();
            }

            this.UpdateValues();
        }

        /// <summary>
        /// Gets a textual representation of the state of the game.
        /// </summary>
        public BindingValue<string> StatusText { get; }

        /// <summary>
        /// Gets a list of visibility values for the Xs on the board.
        /// </summary>
        public IReadOnlyList<BindingValue<Visibility>> XVisibility
            => this.xVisibility;

        /// <summary>
        /// Gets a list of visibility values for the Os on the board.
        /// </summary>
        public IReadOnlyList<BindingValue<Visibility>> OVisibility
            => this.oVisibility;

        /// <summary>
        /// Gets a list of visibility values for the empty cells on the board.
        /// </summary>
        public IReadOnlyList<BindingValue<Visibility>> EVisibility
            => this.eVisibility;

        public void Play(int Cell)
        {
            if (this.game.State == TwoPlayerGameState.InProgress)
            {
                this.game.Play(Cell - 1);
                this.UpdateValues();
            }
            else
            {
                throw new InvalidOperationException("Cannot play a move in current game state.");
            }
        }

        private void UpdateValues()
        {
            this.StatusText.Value = this.game.State switch
            {
                TwoPlayerGameState.InProgress => this.game.Turn ? "X's turn" : "O's turn",
                TwoPlayerGameState.Invalid => "This game state shouldn't be possible",
                TwoPlayerGameState.PlayerOneWins => "X wins!",
                TwoPlayerGameState.PlayerTwoWins => "O wins!",
                TwoPlayerGameState.Tie => "It's a tie!",
                _ => "Unexpected value encountered."
            };

            for (int i = 1; i < 10; i++)
            {
                (this.EVisibility[i].Value, this.XVisibility[i].Value, this.OVisibility[i].Value) = this.game.Current[i - 1] switch
                {
                    0 => (Visibility.Visible, Visibility.Collapsed, Visibility.Collapsed),
                    1 => (Visibility.Collapsed, Visibility.Visible, Visibility.Collapsed),
                    2 => (Visibility.Collapsed, Visibility.Collapsed, Visibility.Visible),
                    _ => throw new InvalidOperationException($"Cell should not ever contain a value of {this.game.Current[0]}")
                };
            }
        }
    }
}
