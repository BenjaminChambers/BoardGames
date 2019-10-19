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
            => (BoardGames.TicTacToe.XO)value switch {
            BoardGames.TicTacToe.XO.X => "X",
            BoardGames.TicTacToe.XO.O => "O",
            _ => string.Empty
            };

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => null;
    }
}
