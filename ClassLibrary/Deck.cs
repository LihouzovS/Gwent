using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ClassLibrary
{
    public class Deck: Base<Deck>
    {
        [SaveAttribute]
        public Guid fraction { get; set; } 
        [SaveAttribute]
        public List<Guid> cards { get; set; }
        public List<Card> realCards;

        public void makeDeck()
        {
            realCards = new List<Card>();

        }

    }
}
