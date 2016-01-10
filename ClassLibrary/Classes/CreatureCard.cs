namespace GwentClasses.Classes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public class CreatureCard : Card
    {
        [SaveAttribute]
        public int basePower { get; set; }
        [SaveAttribute]
        public int currentPower { get; set; }
        [SaveAttribute]
        public bool isHero { get; set; }
        [SaveAttribute]
        public Guid fraction { get; set; }
        public CreatureCard(int p_basePower, int p_currentPower, bool p_isHero, Guid p_fraction)
        {
            // доделать эту гадость
        }
    }
}
