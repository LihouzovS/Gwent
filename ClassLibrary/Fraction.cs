namespace ClassLibrary
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public class Fraction : Base<Fraction>
    {
        // нужно для связывания разных сущностей. да и мало ли, вдруг понадобится?
        [SaveAttribute]
        public string name { get; set; }
        [SaveAttribute]
        public Guid royalEffect { get; set; }
    }
}
