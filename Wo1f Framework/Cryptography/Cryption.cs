using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Wo1f_Framework.Cryptography
{
    public class Cryption
    {
        private static HashAlgorithm Alg;

        public static string Hash(string input)
        {

            using (MD5 hash = MD5.Create())
            {
                return GetMd5Hash(hash, input);
            }

        }


        //from msdn
        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash. 
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes 
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data  
            // and format each one as a hexadecimal string. 
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string. 
            return sBuilder.ToString();
        }
        //end from msdn
        public static byte[] Hash(byte Info, int keysize)
        {
            byte[] ret = new byte[64];

            return ret;
        }
    }
}
