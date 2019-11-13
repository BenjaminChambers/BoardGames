using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PlayConnect4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BoardGames.Games.Connect4.Connect4 Game;

        public MainWindow()
        {
            InitializeComponent();

            Game = new BoardGames.Games.Connect4.Connect4();
            foreach (var b in (Board.Content as StackPanel).Children)
                (b as Button).Click += OnClick;
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
            var c = (int)Char.GetNumericValue((sender as Button).Name[6]);
            Game = Game.Play(c);
            Board.Redraw(Game);
        }
    }
}
