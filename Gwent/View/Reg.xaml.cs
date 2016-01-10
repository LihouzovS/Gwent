namespace Gwent
{
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
    using GwentClasses.Classes;

    public partial class Reg : Window
    {
        public Reg()
        {
            this.InitializeComponent();
        }
        private void Cancel(object sender, RoutedEventArgs e)
        {
            Gamer.Disconnect();
            this.Close();
        }
    }
}
