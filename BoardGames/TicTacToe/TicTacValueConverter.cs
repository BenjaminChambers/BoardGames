using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace TicTacToe
{
    class TicTacValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => (BoardGames.TicTacToe.TicTacMove)value switch {
            BoardGames.TicTacToe.TicTacMove.X => "X",
            BoardGames.TicTacToe.TicTacMove.O => "O",
            _ => string.Empty
            };

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => null;
    }
}
