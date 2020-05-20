using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length != 1) {
            Console.WriteLine("Must be called with the path to the .plr file");
            return;
        }
        decryptFile(args[0], "terraria.plr.decrypted");
    }

    static void decryptFile(string inputFile, string outputFile)
    {
        string s = "h3y_gUyZ";
        UnicodeEncoding unicodeEncoding = new UnicodeEncoding();
        byte[] bytes = unicodeEncoding.GetBytes(s);
        FileStream fileStream = new FileStream(inputFile, FileMode.Open);
        RijndaelManaged rijndaelManaged = new RijndaelManaged();
        CryptoStream cryptoStream = new CryptoStream(fileStream, rijndaelManaged.CreateDecryptor(bytes, bytes), CryptoStreamMode.Read);
        FileStream fileStream2 = new FileStream(outputFile, FileMode.Create);
        try
        {
            int num;
            while ((num = cryptoStream.ReadByte()) != -1)
            {
                fileStream2.WriteByte((byte) num);
            }
            fileStream2.Close();
            cryptoStream.Close();
            fileStream.Close();
        }
        catch
        {
            fileStream2.Close();
            fileStream.Close();
            File.Delete(outputFile);
        }
    }
}
