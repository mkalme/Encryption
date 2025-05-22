using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Diagnostics;

namespace Encryption
{
    class Encry
    {
        public static String text = "";
        public static Form1 form1;

        public static void Enc()
        {
            try
            {

                // Create a new instance of the RijndaelManaged 
                // class.  This generates a new key and initialization  
                // vector (IV). 
                using (RijndaelManaged myRijndael = new RijndaelManaged())
                {

                    myRijndael.GenerateKey();
                    myRijndael.GenerateIV();
                    // Encrypt the string to an array of bytes. 
                    byte[] encrypted = EncryptStringToBytes(text, myRijndael.Key, myRijndael.IV);

                    form1.richTextBox2.Text = Encoding.Default.GetString(myRijndael.IV) + Encoding.Default.GetString(encrypted);
                    form1.textBox1.Text = Encoding.Default.GetString(myRijndael.Key);
                }

            }
            catch (Exception e)
            {
                
            }
        }

        public static void Dec(byte[] key)
        {
            try
            {
                // Create a new instance of the RijndaelManaged 
                // class.  This generates a new key and initialization  
                // vector (IV). 
                using (RijndaelManaged myRijndael = new RijndaelManaged())
                {
                    // Encrypt the string to an array of bytes.
                    byte[] IV = Encoding.Default.GetBytes((form1.richTextBox1.Text.Substring(0, 16)));
                    string decrypted = DecryptStringFromBytes(Encoding.Default.GetBytes(form1.richTextBox1.Text.Substring(16)), key, IV);

                    form1.richTextBox2.Text = decrypted;
                }

            }
            catch (Exception e)
            {

            }
        }

        static byte[] EncryptStringToBytes(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments. 
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;
            // Create an RijndaelManaged object 
            // with the specified key and IV. 
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for encryption. 
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {

                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }


            // Return the encrypted bytes from the memory stream. 
            return encrypted;

        }

        static string DecryptStringFromBytes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments. 
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold 
            // the decrypted text. 
            string plaintext = null;

            // Create an RijndaelManaged object 
            // with the specified key and IV. 
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for decryption. 
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream 
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }

            return plaintext;

        }
    }
}
