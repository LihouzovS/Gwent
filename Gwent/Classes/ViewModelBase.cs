namespace Gwent.Classes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //Обовязковий метод для реалізації INotifyPropertyChanged
        protected void NotifyPropertyChanged(string propertyName)
        {
            this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
