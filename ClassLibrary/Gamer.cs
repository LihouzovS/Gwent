using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Gamer : Base<Gamer>
    {
        //пароль будет зашифрован
        //[SaveAttribute]
        //public bool isUsed { get; set; }
        [SaveAttribute]
        public string login { get; set; }
        [SaveAttribute]
        public int rate { get; set; }       
        [SaveAttribute]
        public string password { get; set; }
        public Gamer() { }
        public Gamer(string p_login, string p_pass, int p_rate = 0)
        {
            //isUsed = false;
            login = p_login;
            Crypto tmp = new Crypto();
           // password =  tmp.Encrypt(p_pass);
            password = p_pass;
            rate = p_rate;
        }

    }
}
