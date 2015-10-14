using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class CardDeck : Base<CardDeck>
    {
        [SaveAttribute]
        public Guid Deck { get; set; }
        [SaveAttribute]
        public Guid Card { get; set; }
        public CardDeck(Guid p_Deck, Guid p_Card)
        {
            Deck = p_Deck;
            Card = p_Card;
        }
    }
}
