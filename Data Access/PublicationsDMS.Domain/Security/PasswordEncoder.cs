using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PublicationsDMS.Domain.Security
{
    internal class PasswordEncoder
    {
        static RNGCryptoServiceProvider rand = new RNGCryptoServiceProvider();

        private static byte[] GetSHA512Hash(string password, byte[] salt)
        {
            SHA512 hashFunction = new SHA512CryptoServiceProvider();
            Encoding encoding = new UTF8Encoding();
            byte[] passByteArray = encoding.GetBytes(password);
            byte[] saltedPassword = new byte[passByteArray.Length + salt.Length];

            Array.Copy(salt, saltedPassword, salt.Length);
            Array.Copy(passByteArray, 0, saltedPassword, salt.Length, passByteArray.Length);

            return hashFunction.ComputeHash(saltedPassword);
        }

        public static void EncryptPassword(string password, out byte[] salt, out byte[] hashedPassword)
        {
            salt = new byte[64];
            rand.GetNonZeroBytes(salt);
            hashedPassword = GetSHA512Hash(password, salt);
        }

        public static bool CheckPassword(string password, byte[] applicationUserSalt, byte[] applicationUserHashedPassword)
        {
            byte[] hashedPassword = GetSHA512Hash(password, applicationUserSalt);

            if (hashedPassword.Length == applicationUserHashedPassword.Length)
            {
                for (int i = 0; i < hashedPassword.Length; i++)
                {
                    if (hashedPassword[i] != applicationUserHashedPassword[i])
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
            return true;
        }
    }
}
