using System;
using System.Data;
using System.Security.Cryptography;
using System.IO;


public class SimpleAES
{
    // Рандомные ключи
    private byte[] Key = { 123, 217, 19, 11, 24, 26, 85, 45, 114, 184, 27, 162, 37, 112, 222, 209, 241, 24, 175, 144, 173, 53, 196, 29, 24, 26, 17, 218, 131, 236, 53, 209 };
    private byte[] Vector = { 146, 64, 191, 111, 23, 3, 113, 119, 231, 121, 2521, 112, 79, 32, 114, 156 };


    private ICryptoTransform EncryptorTransform, DecryptorTransform;
    private System.Text.UTF8Encoding UTFEncoder;

    public SimpleAES()
    {
        //Метод шифрования
        RijndaelManaged rm = new RijndaelManaged();

        //Создаем метод шифрования и дешифрования, используя для этого ключ и вектор
        EncryptorTransform = rm.CreateEncryptor(this.Key, this.Vector);
        DecryptorTransform = rm.CreateDecryptor(this.Key, this.Vector);

        //Используем чтобы перевести байты в текст и наоборот
        UTFEncoder = new System.Text.UTF8Encoding();
    }

    // Формирует ключ шифрования
    static public byte[] GenerateEncryptionKey()
    {
        //Создаем ключ
        RijndaelManaged rm = new RijndaelManaged();
        rm.GenerateKey();
        return rm.Key;
    }

    // Создаем вектор шифрования
    static public byte[] GenerateEncryptionVector()
    {
        
        RijndaelManaged rm = new RijndaelManaged();
        rm.GenerateIV();
        return rm.IV;
    }


    // Шифрование текста и возвращение строки
    public string EncryptToString(string TextValue)
    {
        return ByteArrToString(Encrypt(TextValue));
    }

    /// Шифрование текста и возвращение зашифрованного массива байт
    public byte[] Encrypt(string TextValue)
    {
        
        Byte[] bytes = UTFEncoder.GetBytes(TextValue);

        //Используем для потоковой передачи данных из CryptoStream.
        MemoryStream memoryStream = new MemoryStream();

        //Нам нужно записать расшифрованные байты в поток и прочитать зашифрованные обратны из потока
        #region пишем расшированные значения
        CryptoStream cs = new CryptoStream(memoryStream, EncryptorTransform, CryptoStreamMode.Write);
        cs.Write(bytes, 0, bytes.Length);
        cs.FlushFinalBlock();
        #endregion

        #region считываем зашированные значения обратно из stream
        memoryStream.Position = 0;
        byte[] encrypted = new byte[memoryStream.Length];
        memoryStream.Read(encrypted, 0, encrypted.Length);
        #endregion

        //Очистка
        cs.Close();
        memoryStream.Close();

        return encrypted;
    }

    // Метод расшировки
    public string DecryptString(string EncryptedString)
    {
        return Decrypt(StrToByteArray(EncryptedString));
    }

    //Расширофвка при работе с массивами байт    
    public string Decrypt(byte[] EncryptedValue)
    {
        #region 
        MemoryStream encryptedStream = new MemoryStream();
        CryptoStream decryptStream = new CryptoStream(encryptedStream, DecryptorTransform, CryptoStreamMode.Write);
        decryptStream.Write(EncryptedValue, 0, EncryptedValue.Length);
        decryptStream.FlushFinalBlock();
        #endregion

        #region 
        encryptedStream.Position = 0;
        Byte[] decryptedBytes = new Byte[encryptedStream.Length];
        encryptedStream.Read(decryptedBytes, 0, decryptedBytes.Length);
        encryptedStream.Close();
        #endregion
        return UTFEncoder.GetString(decryptedBytes);
    }

    
    public byte[] StrToByteArray(string str)
    {
        if (str.Length == 0)
            throw new Exception("Недопустимое значение строки");

        byte val;
        byte[] byteArr = new byte[str.Length / 3];
        int i = 0;
        int j = 0;
        do
        {
            val = byte.Parse(str.Substring(i, 3));
            byteArr[j++] = val;
            i += 3;
        }
        while (i < str.Length);
        return byteArr;
    }

       
    public string ByteArrToString(byte[] byteArr)
    {
        byte val;
        string tempStr = "";
        for (int i = 0; i <= byteArr.GetUpperBound(0); i++)
        {
            val = byteArr[i];
            if (val < (byte)10)
                tempStr += "00" + val.ToString();
            else if (val < (byte)100)
                tempStr += "0" + val.ToString();
            else
                tempStr += val.ToString();
        }
        return tempStr;
    }
}