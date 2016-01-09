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

    public class MainWindowVM : ViewModelBase
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
        // это перекинуть в дженерал
        public ObservableCollection<Card> Cards { get; set; }
        public ObservableCollection<CardDeck> CardDecks { get; set; }
        public ObservableCollection<Deck> Decks { get; set; }
        public ObservableCollection<Effect> Effects { get; set; }
        public ObservableCollection<Fraction> Franks { get; set; }
        public ObservableCollection<Friend> Friends { get; set; }
        public ObservableCollection<Gamer> Gamers { get; set; }
        public ObservableCollection<Party> Parties { get; set; }
        public ObservableCollection<CreatureCard> CreatureCards { get; set; }

        public MainWindowVM()
        {
            Cards = new ObservableCollection<Card>();
            //и т.д. доделать когда понадобится
            //тест            
        }

        public string login { get; set; }
        public string pass { get; set; }
        public bool rememberMe { get; set; }
        public bool showPassword { get; set; }
        public bool admin { get; set; }

        //посмотреть, можно ли дальше тоже в приват переделать
        public ICommand LoginCommand
        {
            get { return new RelayCommand(p => this.Login()); }
        }
        public void Login()
        {

            var mainMenu = new MainMenu();
            mainMenu.ShowDialog();
        }
        public ICommand ChooseDeckCommand
        {
            get { return new RelayCommand(p => this.ChooseDeck()); }
        }
        public void ChooseDeck()
        {
            var chooseDeck = new ChooseDeck();
            chooseDeck.ShowDialog();
        }
        public ICommand PlayTutorCommand
        {
            get { return new RelayCommand(p => PlayTutor()); }
        }
        public void PlayTutor()
        {
            var game = new Game();
            game.ShowDialog();
        }
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

    }
}
