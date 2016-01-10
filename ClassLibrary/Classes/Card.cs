namespace GwentClasses.Classes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public class Card : Base<Card>
    {
        [SaveAttribute]
        public string name { get; set; }
        [SaveAttribute]
        public int cost { get; set; }
        [SaveAttribute]
        public string description { get; set; }
        // [SaveAttribute]
        // а не лучше ли не в лейнинг, а в стринг лейнинг. да и зачем эта переменная тут, может просто добавлять во временный список?
        // public Guid laning { get; set; }
        [SaveAttribute]
        // вот тут вопрос, надо ли лист, а, может, сделать несколько эффектов по типу названия
        // первый второй или наоборот, "при входе в игру, тип того"
        public Guid effect;
        public void Play() { }
        public void Die() { }
        public void ReturnToHand() { }
        public Card() { }
        public Card(string p_name, Effect p_effect, int p_cost = 0, string p_desc = "none")
        {
            this.effect = p_effect.ID;
            this.name = p_name;
            this.cost = p_cost;
            this.description = p_desc;
        }
    }
}
