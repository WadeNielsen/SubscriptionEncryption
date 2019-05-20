using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SubscriptionEncryption
{
    public class Security
    {
        /// <summary>
        /// The key used to encrypt and decrypt data given to this class.
        /// </summary>
        private static byte[] key = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16 };

        /// <summary>
        /// The initialization vector used to encrypt and decrypt data given to this class.
        /// </summary>
        private static byte[] iv = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16 };

        /// <summary>
        /// Encodes the specified clear text.
        /// </summary>
        /// <returns>Encoded string</returns>
        /// <param name="clearText">Clear text.</param>
        public static string Encode(string clearText)
        {
            string result = null;

            // Check input.
            if (clearText is null)
            {
                throw new ArgumentNullException();
            }

            // Setup AES Encryption
            using (AesManaged aesAlg = new AesManaged())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Using a memory stream to store the encrypted data.
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    // Encrypt the given clear text to the memory stream
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    using (var sw = new StreamWriter(cryptoStream))
                    {
                        sw.Write(clearText);
                    }

                    // set result to friendly string from given byte data.
                    result = Convert.ToBase64String(memoryStream.ToArray());
                }
            }

            return result;
        }

        /// <summary>
        /// Decode the specified encoded text.
        /// </summary>
        /// <returns>Decoded text.</returns>
        /// <param name="encodedText">Encoded text.</param>
        public static string Decode(string encodedText)
        {
            string result = null;

            // Check input.
            if (encodedText is null)
            {
                throw new ArgumentNullException();
            }

            // Setup AES Encryption
            using (AesManaged aesAlg = new AesManaged())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Get byte data from given string and place into memory stream for use with the cryptostream.
                byte[] bytes = Convert.FromBase64String(encodedText);

                using (MemoryStream memoryStream = new MemoryStream(bytes))
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                using (var sr = new StreamReader(cryptoStream))
                {
                    // Decode string and place into result.
                    result = sr.ReadToEnd();
                }
            }

            return result;
        }
    }
}
