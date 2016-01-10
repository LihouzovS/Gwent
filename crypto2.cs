public class CryptoStream : Stream
{
    private static byte[] _salt = Encoding.ASCII.GetBytes("42kb$2fs$@#GE$^%gdhf;!M807c5o666");
    private Stream _stream;

    public CryptoStream(Stream innerStream)
    {
        _stream = innerStream;
    }

    public override void Write(byte[] bytes)
    {
        var cyphered = Cypher(bytes);
        _stream.Write(cyphered);
    }

    public override byte[] Read()
    {
        var cyphered = _stream.Read();
        return Decypher(cyphered);
    }

    private static Stream CreateCryptoStreamAESWrite(string sharedSecret, Stream stream)
    {
        if (string.IsNullOrEmpty(sharedSecret))
            throw new ArgumentNullException("sharedSecret");

        // Генерация ключа
        Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(sharedSecret, _salt);

        // Создание объекта RijndaelManaged object
        RijndaelManaged aesAlg = new RijndaelManaged();
        aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);

        // Создание дешифратора для выполнения потока преобразования
        ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

        // Указать вначале IV
        stream.Write(BitConverter.GetBytes(aesAlg.IV.Length), 0, sizeof(int));
        stream.Write(aesAlg.IV, 0, aesAlg.IV.Length);

        CryptoStream csEncrypt = new CryptoStream(stream, encryptor, CryptoStreamMode.Write);
        return csEncrypt;
    }

    private static Stream CreateCryptoStreamAESRead(string sharedSecret, Stream stream)
    {
        if (string.IsNullOrEmpty(sharedSecret))
            throw new ArgumentNullException("sharedSecret");

        // Генерация ключа от общих секретных
        Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(sharedSecret, _salt);

        // Создать объект RijndaelManaged 
        // с указанным ключом и IV.
        RijndaelManaged aesAlg = new RijndaelManaged();
        aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);

        // Получить вектор инициализации из потока зашифрованных
        aesAlg.IV = ReadByteArray(stream);

        // Создать декриптор выполнить поток transform 
        ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
        CryptoStream csDecrypt = new CryptoStream(stream, decryptor, CryptoStreamMode.Read);
        return csDecrypt;
    }

    private static byte[] ReadByteArray(Stream s)
    {
        byte[] rawLength = new byte[sizeof(int)];
        if (s.Read(rawLength, 0, rawLength.Length) != rawLength.Length)
            throw new SystemException("Stream did not contain properly formatted byte array");

        byte[] buffer = new byte[BitConverter.ToInt32(rawLength, 0)];
        if (s.Read(buffer, 0, buffer.Length) != buffer.Length)
            throw new SystemException("Did not read byte array properly");

        return buffer;
    }
}

//Проверка на работоспособность
   FileStream fs = new FileStream("c:\\temp\\test.dat", FileMode.Create, FileAccess.ReadWrite, FileShare.None);
        Stream cs = Crypto.CreateCryptoStreamAESWrite("test", fs);
        BinaryWriter bw = new BinaryWriter(cs);
        bw.Write("test");
        cs.Flush();
        fs.Close();
        fs = new FileStream("c:\\temp\\test.dat", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        cs = Crypto.CreateCryptoStreamAESRead("test", fs);
        BinaryReader br = new BinaryReader(cs);
        string test = br.ReadString();
        if (test != "test")
            throw new Exception("Your Kungfu is not good!");
        fs.Close();