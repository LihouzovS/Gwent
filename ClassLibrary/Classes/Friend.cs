namespace GwentClasses.Classes
{
    using System;
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
