namespace Gwent.ViewModel
{
    using System;
    using Gwent.ViewModel;
    using GwentClasses.Classes;
    using MVVMCommon;
    using ICommand = System.Windows.Input.ICommand;

    public class RegistrationVM : GwentViewModelBase
    {
        public string login;
        public string pass;
        public string passRepeat;
        #region Command
        ICommand AcceptRegistrationCommand
        {
            get { return new RelayCommand(p => RegistrationAccept()); }
        }
        #endregion

        #region func
        public void RegistrationAccept()
        {
            if (pass == passRepeat)
            {
                WaitingWindow w = new WaitingWindow();
                w.Show();
                //как сделать?
                this.IsEnabled = false;
                Gamer.ConnectToDatabase();
                Gamer g = new Gamer(login, pass);
                if (g.login == (Gamer.FindByField("login", login)).login)
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
        #endregion
    }
}
