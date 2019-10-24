using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using BoardGames.TicTacToe;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BoardGames.TicTacToe.TicTacToe game = new BoardGames.TicTacToe.TicTacToe();

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
    }
}
