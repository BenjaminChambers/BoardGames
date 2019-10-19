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
        bool turnX = true;
        BoardGames.TicTacToe.TicTacToe game = new BoardGames.TicTacToe.TicTacToe();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            switch ((sender as Button).Name)
            {
                case "B1": game = (BoardGames.TicTacToe.TicTacToe)game.Play(0); break;
                case "B2": game = (BoardGames.TicTacToe.TicTacToe)game.Play(1); break;
                case "B3": game = (BoardGames.TicTacToe.TicTacToe)game.Play(2); break;
                case "B4": game = (BoardGames.TicTacToe.TicTacToe)game.Play(3); break;
                case "B5": game = (BoardGames.TicTacToe.TicTacToe)game.Play(4); break;
                case "B6": game = (BoardGames.TicTacToe.TicTacToe)game.Play(5); break;
                case "B7": game = (BoardGames.TicTacToe.TicTacToe)game.Play(6); break;
                case "B8": game = (BoardGames.TicTacToe.TicTacToe)game.Play(7); break;
                case "B9": game = (BoardGames.TicTacToe.TicTacToe)game.Play(8); break;
            }

            turnX = !turnX;
        }
    }
}
