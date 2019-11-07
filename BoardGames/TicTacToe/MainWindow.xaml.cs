using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using BoardGames.Pieces;
using BoardGames.Games.TicTacToe;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BoardGames.Games.TicTacToe.TicTacToe game = new BoardGames.Games.TicTacToe.TicTacToe();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var cell = int.Parse((sender as Button).Name[1].ToString()) - 1;
            if (game.InProgress)
            {
                game = game.Play(cell);
                ((sender as Button).Content as TextBlock).Text = (game[cell] == XO.Empty) ? string.Empty : game[cell].ToString();
            }

            if (game.InProgress)
            {
                Status.Text = game.Turn.ToString() + "'s turn";
            } else
            {
                Status.Text = game.Winner switch
                {
                    XO.X => "X wins!",
                    XO.O => "O wins!",
                    _ => "It's a tie!"
                };
            }
        }

        private void OnNewGame(object sender, RoutedEventArgs e)
        {
            game = new BoardGames.Games.TicTacToe.TicTacToe();
            (B1.Content as TextBlock).Text = string.Empty;
            (B2.Content as TextBlock).Text = string.Empty;
            (B3.Content as TextBlock).Text = string.Empty;
            (B4.Content as TextBlock).Text = string.Empty;
            (B5.Content as TextBlock).Text = string.Empty;
            (B6.Content as TextBlock).Text = string.Empty;
            (B7.Content as TextBlock).Text = string.Empty;
            (B8.Content as TextBlock).Text = string.Empty;
            (B9.Content as TextBlock).Text = string.Empty;

            Status.Text = "X's turn";
        }
    }
}
