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

namespace PlayConnect4.Controls
{
    /// <summary>
    /// Interaction logic for C4Board.xaml
    /// </summary>
    public partial class C4Board : UserControl
    {
        readonly static BitmapImage Red = new BitmapImage(new Uri(@"/Assets/checkerred.png", UriKind.RelativeOrAbsolute));
        readonly static BitmapImage Black = new BitmapImage(new Uri(@"/Assets/checkerblack.png", UriKind.RelativeOrAbsolute));
        readonly static BitmapImage Empty = new BitmapImage(new Uri(@"/Assets/checkernone.png", UriKind.RelativeOrAbsolute));

        readonly Image[,] Cells = new Image[7,6];

        public C4Board()
        {
            InitializeComponent();

            for (int i = 0; i < 7; i++)
            {
                var b = new Button() { Name = $"Column{i}" };
                (Content as StackPanel).Children.Add(b);
                var s = new StackPanel();
                b.Content = s;

                for (int j = 0; j < 6; j++)
                {
                    var img = new Image() { Source = Empty, Width = 64, Height = 64 };
                    Cells[i, j] = img;
                    s.Children.Insert(0,img);
                }
            }
        }

        public void Redraw()
        {
            for (int i = 0; i < 7; i++)
                for (int j = 0; j < 6; j++)
                    Cells[i, j].Source = Empty;
        }

        public void Redraw(BoardGames.Games.Connect4.Connect4 Source)
        {
            for (int i = 0; i < 7; i++)
            {
                for (int j = 5; j >= 0; j--)
                {
                    Cells[i, j].Source = Source[i, j] switch
                    {
                        BoardGames.Pieces.Checker.Black => Black,
                        BoardGames.Pieces.Checker.Red => Red,
                        _ => Empty
                    };
                }
            }
        }
    }
}
