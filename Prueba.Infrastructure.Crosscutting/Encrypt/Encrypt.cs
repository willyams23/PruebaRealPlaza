using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Prueba.Infrastructure.Crosscutting.ExceptionsTypes;

namespace Prueba.Infrastructure.Crosscutting.Encrypt
{
    public class Encrypt
    {
        private readonly EncryptionSettings _encryptionSettings;

        public Encrypt(IOptions<EncryptionSettings> encryptionSettings)
        {
            _encryptionSettings = encryptionSettings.Value;
        }

        public string EncryptString(string plainText)
        {
            // Instantiate a new RijndaelManaged object to perform string symmetric encryption
            RijndaelManaged rijndaelCipher = new RijndaelManaged();

            // Set key and IV
            rijndaelCipher.Key = Convert
                .FromBase64String(_encryptionSettings.Key);
            rijndaelCipher.IV = Convert
                .FromBase64String(_encryptionSettings.IV);

            // Instantiate a new MemoryStream object to contain the encrypted bytes
            MemoryStream memoryStream = new MemoryStream();

            // Instantiate a new encryptor from our RijndaelManaged object
            ICryptoTransform rijndaelEncryptor = rijndaelCipher.CreateEncryptor();

            // Instantiate a new CryptoStream object to process the data and write it to the 
            // memory stream
            CryptoStream cryptoStream = new CryptoStream(memoryStream, rijndaelEncryptor, CryptoStreamMode.Write);

            // Convert the plainText string into a byte array
            byte[] plainBytes = Encoding.ASCII.GetBytes(plainText);

            // Encrypt the input plaintext string
            cryptoStream.Write(plainBytes, 0, plainBytes.Length);

            // Complete the encryption process
            cryptoStream.FlushFinalBlock();

            // Convert the encrypted data from a MemoryStream to a byte array
            byte[] cipherBytes = memoryStream.ToArray();

            // Close both the MemoryStream and the CryptoStream
            memoryStream.Close();
            cryptoStream.Close();

            // Convert the encrypted byte array to a base64 encoded string
            string cipherText = Convert.ToBase64String(cipherBytes, 0, cipherBytes.Length);

            // Return the encrypted data as a string
            return cipherText;
        }


        public string DecryptString(string cipherText)
        {
            try
            {
                // Instantiate a new RijndaelManaged object to perform string symmetric encryption
                RijndaelManaged rijndaelCipher = new RijndaelManaged();

                // Set key and IV
                rijndaelCipher.Key = Convert
                    .FromBase64String(_encryptionSettings.Key);
                rijndaelCipher.IV = Convert
                    .FromBase64String(_encryptionSettings.IV);

                // Instantiate a new MemoryStream object to contain the encrypted bytes
                MemoryStream memoryStream = new MemoryStream();

                // Instantiate a new encryptor from our RijndaelManaged object
                ICryptoTransform rijndaelDecryptor = rijndaelCipher.CreateDecryptor();

                // Instantiate a new CryptoStream object to process the data and write it to the 
                // memory stream
                CryptoStream cryptoStream = new CryptoStream(memoryStream, rijndaelDecryptor, CryptoStreamMode.Write);

                // Will contain decrypted plaintext
                string plainText = String.Empty;

                try
                {
                    // Convert the ciphertext string into a byte array
                    byte[] cipherBytes = Convert.FromBase64String(cipherText);

                    // Decrypt the input ciphertext string
                    cryptoStream.Write(cipherBytes, 0, cipherBytes.Length);

                    // Complete the decryption process
                    cryptoStream.FlushFinalBlock();

                    // Convert the decrypted data from a MemoryStream to a byte array
                    byte[] plainBytes = memoryStream.ToArray();

                    // Convert the encrypted byte array to a base64 encoded string
                    plainText = Encoding.ASCII.GetString(plainBytes, 0, plainBytes.Length);
                }
                finally
                {
                    // Close both the MemoryStream and the CryptoStream
                    memoryStream.Close();
                    cryptoStream.Close();
                }

                // Return the encrypted data as a string
                return plainText;
            }
            catch (Exception error)
            {
                throw new ValidarCredencialesException("Error al desencriptar datos. " + error.Message);
            }
        }
    }
}