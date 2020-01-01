namespace Play.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Text;

    /// <summary>
    /// An instance of type T that provides data binding.
    /// </summary>
    /// <typeparam name="T">The type to use.</typeparam>
    public class BindingValue<T> : INotifyPropertyChanged
    {
        private T value;

        /// <summary>
        /// Implement INotifyPropertyChanged.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the public property to be bound to.
        /// </summary>
        public T Value
        {
            get => this.value;
            set
            {
                if (!value.Equals(this.value))
                {
                    this.value = value;
                    this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Value)));
                }
            }
        }
    }
}
