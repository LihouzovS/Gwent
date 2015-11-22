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
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Shapes;
    using ClassLibrary;
    public partial class MainMenu : Window
    {
        public MainMenu(string p_login, string p_pass)
        {
            this.InitializeComponent();
            Gamer.ConnectToDatabase();
            ////вот тут надо хорошо поработать
            ////то есть тут даже защиты от экзепшона нет. заебись
            ////геймера создать выше
            Gamer g = Gamer.FindByField("password", p_pass);
            Gamer.Disconnect();
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void ChooseDeck(object sender, RoutedEventArgs e) { }
        private void LoadCollection(object sender, RoutedEventArgs e) { }
        private void btnSettings_Click(object sender, RoutedEventArgs e) { }
    }
}
