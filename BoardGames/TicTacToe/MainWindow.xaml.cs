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
                case "B1": game = (BoardGames.TicTacToe.TicTacToe)game.Put(0, turnX ? XO.X : XO.O); break;
                case "B2": game = (BoardGames.TicTacToe.TicTacToe)game.Put(1, turnX ? XO.X : XO.O); break;
                case "B3": game = (BoardGames.TicTacToe.TicTacToe)game.Put(2, turnX ? XO.X : XO.O); break;
                case "B4": game = (BoardGames.TicTacToe.TicTacToe)game.Put(3, turnX ? XO.X : XO.O); break;
                case "B5": game = (BoardGames.TicTacToe.TicTacToe)game.Put(4, turnX ? XO.X : XO.O); break;
                case "B6": game = (BoardGames.TicTacToe.TicTacToe)game.Put(5, turnX ? XO.X : XO.O); break;
                case "B7": game = (BoardGames.TicTacToe.TicTacToe)game.Put(6, turnX ? XO.X : XO.O); break;
                case "B8": game = (BoardGames.TicTacToe.TicTacToe)game.Put(7, turnX ? XO.X : XO.O); break;
                case "B9": game = (BoardGames.TicTacToe.TicTacToe)game.Put(8, turnX ? XO.X : XO.O); break;
            }

            turnX = !turnX;
        }
    }
}
