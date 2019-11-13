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

        readonly StackPanel[] CellStacks = new StackPanel[7];

        public C4Board()
        {
            InitializeComponent();

            CellStacks[0] = Cells0;
            CellStacks[1] = Cells1;
            CellStacks[2] = Cells2;
            CellStacks[3] = Cells3;
            CellStacks[4] = Cells4;
            CellStacks[5] = Cells5;
            CellStacks[6] = Cells6;

            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    CellStacks[i].Children.Add(new Image() { Source = Empty, Width = 64, Height = 64 });
                }
            }
        }

        public void Redraw(BoardGames.Games.Connect4.Connect4 Source)
        {
            for (int i = 0; i < 7; i++)
            {
                CellStacks[i].Children.Clear();

                for (int j = 5; j >= 0; j--)
                {
                    var img = new Image()
                    {
                        Source = Source[i, j] switch
                        {
                            BoardGames.Pieces.Checker.Black => Black,
                            BoardGames.Pieces.Checker.Red => Red,
                            _ => Empty
                        },
                        Width = 64,
                        Height = 64
                    };

                    CellStacks[i].Children.Add(img);
                }
            }
        }
    }
}
