using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ORAServices.Tasks
{
    public class HashingData
    {
        #region---Hashing---
        public static string ComputeSHA512Hash(string rawData)
        {
            // Create a SHA512
            using (SHA512 sha512Hash = SHA512.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha512Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2")); //x2
                }
                return builder.ToString();
            }
        }//
        #endregion---Hashing---

        #region---Data Hashing Commented---
        //public const int SALT_BYTE_SIZE = 24;
        //public const int HASH_BYTE_SIZE = 24;
        //public const int PBKDF2_ITERATIONS = 1000;

        //public const int ITERATION_INDEX = 0;
        //public const int SALT_INDEX = 1;
        //public const int PBKDF2_INDEX = 2;

        //public static string CreateHash(string password)
        //{
        //    // Generate a random salt
        //    RNGCryptoServiceProvider csprng = new RNGCryptoServiceProvider();
        //    byte[] salt = new byte[SALT_BYTE_SIZE];
        //    csprng.GetBytes(salt);

        //    // Hash the password and encode the parameters
        //    byte[] hash = PBKDF2(password, salt, PBKDF2_ITERATIONS, HASH_BYTE_SIZE);
        //    return PBKDF2_ITERATIONS + ":" +
        //        Convert.ToBase64String(salt) + ":" +
        //        Convert.ToBase64String(hash);
        //}

        //private static byte[] PBKDF2(string password, byte[] salt, int iterations, int outputBytes)
        //{
        //    Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt);
        //    pbkdf2.IterationCount = iterations;
        //    return pbkdf2.GetBytes(outputBytes);
        //}

        //public static bool ValidatePassword(string password, string correctHash)
        //{
        //    // Extract the parameters from the hash
        //    char[] delimiter = { ':' };
        //    string[] split = correctHash.Split(delimiter);
        //    int iterations = Int32.Parse(split[ITERATION_INDEX]);
        //    byte[] salt = Convert.FromBase64String(split[SALT_INDEX]);
        //    byte[] hash = Convert.FromBase64String(split[PBKDF2_INDEX]);

        //    byte[] testHash = PBKDF2(password, salt, iterations, hash.Length);
        //    return SlowEquals(hash, testHash);
        //}

        //private static bool SlowEquals(byte[] a, byte[] b)
        //{
        //    uint diff = (uint)a.Length ^ (uint)b.Length;
        //    for (int i = 0; i < a.Length && i < b.Length; i++)
        //        diff |= (uint)(a[i] ^ b[i]);
        //    return diff == 0;
        //}

        //[WebMethod(EnableSession = true)]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //public bool HashingTest(string myPassword)
        //{
        //    string hashedPass = CreateHash(myPassword);
        //    bool tf = ValidatePassword(myPassword, hashedPass);
        //    return tf;
        //}
        #endregion---Data Hashing Commented---
    }
}