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

            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            void UpdateCell(Button b, int num)
                => (b.Content as TextBlock).Text = (game[num] == XO.Empty) ? string.Empty : game[num].ToString();

            UpdateCell(B1, 0);
            UpdateCell(B2, 1);
            UpdateCell(B3, 2);
            UpdateCell(B4, 3);
            UpdateCell(B5, 4);
            UpdateCell(B6, 5);
            UpdateCell(B7, 6);
            UpdateCell(B8, 7);
            UpdateCell(B9, 8);

            if (game.InProgress)
            {
                Status.Text = game.Turn.ToString() + "'s turn";
            }
            else
            {
                Status.Text = game.Winner switch
                {
                    XO.X => "X wins!",
                    XO.O => "O wins!",
                    _ => "It's a tie!"
                };
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var cell = int.Parse((sender as Button).Name[1].ToString()) - 1;
            if (game.InProgress)
                game = game.Play(cell);

            UpdateDisplay();
        }

        private void OnNewGame(object sender, RoutedEventArgs e)
        {
            game = new BoardGames.Games.TicTacToe.TicTacToe();

            UpdateDisplay();
        }

        private void OnFlipH(object sender, RoutedEventArgs e)
        {
            game = game.Flip(true, false);
            UpdateDisplay();
        }

        private void OnFlipV(object sender, RoutedEventArgs e)
        {
            game = game.Flip(false, true);
            UpdateDisplay();
        }

        private void OnRotateCW(object sender, RoutedEventArgs e)
        {
            game = game.Rotate(1);
            UpdateDisplay();
        }

        private void OnRotateCCW(object sender, RoutedEventArgs e)
        {
            game = game.Rotate(-1);
            UpdateDisplay();
        }
    }
}
