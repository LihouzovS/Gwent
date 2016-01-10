namespace GwentClasses.Classes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public class Crypto
    {
        public int key = 666;
        public Crypto() { }
        public string Encrypt(string p_info)
        {
            byte[] arr = Encoding.Unicode.GetBytes(p_info);
            for (int i = 0; i < p_info.Length; i++)
            {
                arr[i] = (byte)(arr[i] ^ key);
            }
            return Encoding.Unicode.GetString(arr);
        }
        public string Decrypt(string p_info)
        {
            return Encrypt(p_info);
        }
    }
}
