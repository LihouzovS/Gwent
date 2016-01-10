namespace GwentClasses.Classes
{
    using System;

    public class Fraction : Base<Fraction>
    {
        // нужно для связывания разных сущностей. да и мало ли, вдруг понадобится?
        [SaveAttribute]
        public string name { get; set; }
        [SaveAttribute]
        public Guid royalEffect { get; set; }
    }
}
