namespace Gwent.ViewModel
{
    using System;
    using System.Collections.ObjectModel;
    using Gwent.Base;
    using MVVMCommon;
    using GwentClasses.Classes;
    using ICommand = System.Windows.Input.ICommand;

    public class MainWindowVM : GwentViewModelBase
    {
        private static MainWindowVM _mainWindowViewModel;
        private static object _lock = new object();
        public static MainWindowVM instance
        {
            get
            {
                if (_mainWindowViewModel == null)
                {
                    lock (_lock)
                    {
                        if (_mainWindowViewModel == null)
                        {
                            _mainWindowViewModel = new MainWindowVM();
                        }
                    }
                }


                return _mainWindowViewModel;
            }
        }

        public MainWindowVM() {}
        public string login { get; set; }
        public string pass { get; set; }
        public bool rememberMe { get; set; }
        public bool showPassword { get; set; }
        public bool admin { get; set; }

        //вот тут начинается игра! апдейт перекинуть в вм нужный 
        private Gamer _currentGamer;
        public Gamer currentGamer
        {
            get { return this._currentGamer; }
            set
            {
                this._currentGamer = value;
                OnPropertyChanged("currentGamer");
            }
        }
        public Gamer opponentGamer { get; set; }
        //надо ли делать с ним то же, что и с текущим? ничего же менять не будем
        public void Login()
        {
            var mainMenu = new MainMenu();
            mainMenu.ShowDialog();
        }
        public void ChooseDeck()
        {
            var chooseDeck = new ChooseDeck();
            chooseDeck.ShowDialog();
        }
        public void PlayTutor()
        {
            var game = new Game();
            game.ShowDialog();
        }
        public void Registration()
        {
            var registration = new Reg();
            registration.ShowDialog();
        }
        public void RememberMe()
        {
            //вот тут запилить сериализацию в настройки, скорее всего, пароля и логика. как вариант
        }
        //нужно ли это тут или как там у Крохмаля сделано
        public void ShowPassword()
        {
            //он и так пока показывается, поэтому я просто уберу видимость того чекбокса. потом доделать
        }
        public void Exit()
        {
            Environment.Exit(0);
        }

        public ICommand RegistrarionCommand
        {
            get { return new RelayCommand(p => Registration()); }
        }
        public ICommand ChooseDeckCommand
        {
            get { return new RelayCommand(p => this.ChooseDeck()); }
        }
        public ICommand PlayTutorCommand
        {
            get { return new RelayCommand(p => PlayTutor()); }
        }
        public ICommand IsRememberMeCommand
        {
            get { return new RelayCommand(p => RememberMe()); }
        }
        public ICommand ExitCommand
        {
            get { return new RelayCommand(p => this.Exit()); }
        }
        public ICommand LoginCommand
        {
            get { return new RelayCommand(p => this.Login()); }
        }
        public ICommand ShowPasswordCommand
        {
            get { return new RelayCommand(p => this.Login()); }
        }
    }
}
