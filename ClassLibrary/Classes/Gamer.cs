namespace GwentClasses.Classes
{
    using System;
    public class Gamer : Base<Gamer>
    {
        // пароль будет зашифрован
        // [SaveAttribute]
        // public bool isUsed { get; set; }
        [SaveAttribute]
        public string login { get; set; }
        [SaveAttribute]
        public int rate { get; set; }
        [SaveAttribute]
        public string password { get; set; }
        public Gamer() { }
        public Gamer(string p_login, string p_pass, int p_rate = 0)
        {
            // isUsed = false;
            this.login = p_login;
            Crypto tmp = new Crypto();
            // password =  tmp.Encrypt(p_pass);
            this.password = p_pass;
            this.rate = p_rate;
        }
    }
}
