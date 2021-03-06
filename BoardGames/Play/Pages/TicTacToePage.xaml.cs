﻿namespace Play.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;
    using BoardGames.Games;

    /// <summary>
    /// Interaction logic for TicTacToePage.xaml.
    /// </summary>
    public partial class TicTacToePage : Page
    {
        /// <summary>
        /// The RoutedCommand used when clicking on cells.
        /// </summary>
        public static readonly RoutedUICommand Play = new RoutedUICommand();

        /// <summary>
        /// Initializes a new instance of the <see cref="TicTacToePage"/> class.
        /// </summary>
        public TicTacToePage()
        {
            this.InitializeComponent();
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.gameController.State == TwoPlayerGameState.InProgress;
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.gameController.Play((int)e.Parameter);
        }
    }
}
