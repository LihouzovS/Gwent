namespace ClassLibrary
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public class Friend
    {
        [SaveAttribute]
        public Guid firstFriend { get; set; }
        [SaveAttribute]
        public Guid secondFriend { get; set; }
        public Friend(Guid p_firstFriend, Guid p_secondFriend)
        {
            firstFriend = p_firstFriend;
            secondFriend = p_secondFriend;
        }
    }
}
