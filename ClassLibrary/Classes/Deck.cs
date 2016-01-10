namespace GwentClasses.Classes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public class Deck : Base<Deck>
    {
        [SaveAttribute]
        public Guid fraction { get; set; }
        [SaveAttribute]
        public List<Guid> cards { get; set; }
        public List<Card> realCards;

        public void MakeDeck()
        {
            this.realCards = new List<Card>();
        }
    }
}
