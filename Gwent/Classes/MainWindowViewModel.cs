namespace Gwent.Classes
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ClassLibrary;
    using Gwent.Classes;
    using MVVMCommon;
    using ICommand = System.Windows.Input.ICommand;

    class MainWindowViewModel : ViewModelBase
    {
        private static MainWindowViewModel _mainWindowViewModel;
        private static object _lock = new object();


        public static MainWindowViewModel instance
        {
            get
            {
                if (_mainWindowViewModel == null)
                {
                    lock (_lock)
                    {
                        if (_mainWindowViewModel == null)
                        {
                            _mainWindowViewModel = new MainWindowViewModel();
                        }
                    }
                }


                return _mainWindowViewModel;
            }
        }

        public ObservableCollection<Card> Cards { get; set; }
        public ObservableCollection<CardDeck> CardDecks { get; set; }
        public ObservableCollection<Deck> Decks { get; set; }
        public ObservableCollection<Effect> Effects { get; set; }
        public ObservableCollection<Fraction> Franks { get; set; }
        public ObservableCollection<Friend> Friends { get; set; }
        public ObservableCollection<Gamer> Gamers { get; set; }
        public ObservableCollection<Party> Parties { get; set; }
        public ObservableCollection<CreatureCard> CreatureCards { get; set; }

        public MainWindowViewModel()
        {
            Cards = new ObservableCollection<Card>();
            //и т.д. доделать когда понадобится
        }

        private string login { get; set; }
        private string pass { get; set; }
        private bool rememberMe { get; set; }
        private bool showPassword { get; set; }
        private bool admin { get; set; }
        //посмотреть, можно ли дальше тоже в приват переделать
        public ICommand LoginCommand
        {
            get { return new RelayCommand(p => Login()); }
        }
        public void Login()
        {
            pass = "";
            var mainMenu = new MainMenu();
            mainMenu.ShowDialog();
        }
    }
}
