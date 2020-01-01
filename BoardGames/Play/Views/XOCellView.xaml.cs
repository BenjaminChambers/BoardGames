namespace Play.Views
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
    /// Interaction logic for XOCellView.xaml.
    /// </summary>
    public partial class XOCellView : UserControl
    {
        private int index;

        /// <summary>
        /// Initializes a new instance of the <see cref="XOCellView"/> class.
        /// </summary>
        public XOCellView()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the index of the cell being represented by this control.
        /// Index corresponds to the numbers on a 10-key.
        /// </summary>
        public int Index
        {
            get => this.index;

            set
            {
                this.index = value;
                this.CellButton.SetBinding(FrameworkElement.VisibilityProperty, new Binding($"EVisibility[{this.index}].Value") { Mode = BindingMode.OneWay });
                this.CellButton.CommandParameter = value;
                this.CellX.SetBinding(FrameworkElement.VisibilityProperty, new Binding($"XVisibility[{this.index}].Value") { Mode = BindingMode.OneWay });
                this.CellO.SetBinding(FrameworkElement.VisibilityProperty, new Binding($"OVisibility[{this.index}].Value") { Mode = BindingMode.OneWay });
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="ICommand"/> used by the <see cref="Button"/>.
        /// </summary>
        public ICommand Command
        {
            get => this.CellButton.Command;
            set => this.CellButton.Command = value;
        }
    }
}
