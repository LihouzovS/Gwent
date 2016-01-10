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
        private void Accept(object sender, RoutedEventArgs e)
        {
            if (tbPass.Text == tbPassRepeat.Text)
            {
                WaitingWindow w = new WaitingWindow();
                w.Show();
                this.IsEnabled = false;
                Gamer.ConnectToDatabase();
                Gamer g = new Gamer(tbLogin.Text, tbPass.Text);
                if (g.login == (Gamer.FindByField("login", tbLogin.Text)).login)
                {
                    lblIsUsed.Visibility = System.Windows.Visibility.Visible;
                    w.Close();
                    this.IsEnabled = true;
                }
                else
                {
                    lblIsUsed.Visibility = System.Windows.Visibility.Hidden;
                    g.Save();
                    Gamer.Disconnect();
                    w.Close();
                    this.IsEnabled = true;
                    this.Close();
                }
            }
        }
        private void Cancel(object sender, RoutedEventArgs e)
        {
            Gamer.Disconnect();
            this.Close();
        }
    }
}
