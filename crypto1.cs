//Для шифрования
FileStream stream = new FileStream(“C:\\test.txt”, 
         FileMode.OpenOrCreate,FileAccess.Write);

DESCryptoServiceProvider cryptic = new DESCryptoServiceProvider();

cryptic.Key = ASCIIEncoding.ASCII.GetBytes(“ABCDEFGH”);
cryptic.IV = ASCIIEncoding.ASCII.GetBytes(“ABCDEFGH”);

CryptoStream crStream = new CryptoStream(stream,
   cryptic.CreateEncryptor(),CryptoStreamMode.Write);


byte[] data = ASCIIEncoding.ASCII.GetBytes(“Hello World!”);

crStream.Write(data,0,data.Length);

crStream.Close();
stream.Close();

//Для расшифровки 
FileStream stream = new FileStream(“C:\\test.txt”, 
                              FileMode.Open,FileAccess.Read);

DESCryptoServiceProvider cryptic = new DESCryptoServiceProvider();

cryptic.Key = ASCIIEncoding.ASCII.GetBytes(“ABCDEFGH”);
cryptic.IV = ASCIIEncoding.ASCII.GetBytes(“ABCDEFGH”);

CryptoStream crStream = new CryptoStream(stream,
    cryptic.CreateDecryptor(),CryptoStreamMode.Read);

StreamReader reader = new StreamReader(crStream);

string data = reader.ReadToEnd();

reader.Close();
stream.Close();