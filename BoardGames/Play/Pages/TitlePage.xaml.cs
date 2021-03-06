﻿namespace Play.Pages
{
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

    /// <summary>
    /// Interaction logic for TitlePage.xaml
    /// </summary>
    public partial class TitlePage : Page
    {
        public TitlePage()
        {
            this.InitializeComponent();
        }

        private void ClickTicTacToe(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new TicTacToePage());
        }
    }
}
