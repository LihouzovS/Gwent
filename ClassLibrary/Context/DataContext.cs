namespace ClassLibrary.Context
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using GwentClasses.Classes;
    public class DataContext
    {
        public ObservableCollection<Card> Cards { get; set; }
        public ObservableCollection<CardDeck> CardDecks { get; set; }
        public ObservableCollection<Deck> Decks { get; set; }
        public ObservableCollection<Effect> Effects { get; set; }
        public ObservableCollection<Fraction> Fractions { get; set; }
        public ObservableCollection<Friend> Friends { get; set; }
        public ObservableCollection<Gamer> Gamers { get; set; }
        public ObservableCollection<Party> Parties { get; set; }
        public ObservableCollection<CreatureCard> CreatureCards { get; set; }
        public DataContext() 
        {
            Cards = new ObservableCollection<Card>();
            CardDecks = new ObservableCollection<CardDeck>();
            Decks = new ObservableCollection<Deck>();
            Effects = new ObservableCollection<Effect>();
            Fractions = new ObservableCollection<Fraction>();
            Friends = new ObservableCollection<Friend>();
            Gamers = new ObservableCollection<Gamer>();
            Parties = new ObservableCollection<Party>();
        }
    }
}
