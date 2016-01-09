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
    using System.Windows.Navigation;
    using System.Windows.Shapes;
    using ClassLibrary;
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void Login(object sender, RoutedEventArgs e)
        {
            ////вот тут надо удалить старые настройки или сериализировать их для того, чтоб не получилось два синглтона. вот

            ///ЭТО работает
            ///Crypto c = new Crypto();
            ///string n =  c.Encrypt("pass");
            ///string wi = c.Decrypt(n);

            if (admin.IsChecked == true)
            {
                MainMenu w = new MainMenu("admin", "123");
                w.Show();
                this.Hide();
            }
            tbPass.Clear();
            ////MainMenu w = new MainMenu();
        }
        private void Exit(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
        private void btn_Registration_Click(object sender, RoutedEventArgs e)
        {
            Reg w = new Reg();
            w.ShowDialog();
        }
        private void Show_pass_Checked(object sender, RoutedEventArgs e) { }


    }
}
