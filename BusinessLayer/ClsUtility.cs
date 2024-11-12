using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public  class ClsUtility
    {
       
       public static ClsUsers User = null;

        public static  string ComputeHash(string Value)
        {

            using(SHA256 sha = SHA256.Create())
            {

                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(Value));

                return BitConverter.ToString(bytes).Replace("-","").ToLower();
            }

        }


        public static bool WriteDataToEnvironmentVar(string Name, string Value)
        {
            try
            {
                Environment.SetEnvironmentVariable(Name, Value);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

            return true;
        }


        public static string ReadDataFromEnvironmentVar(string Name)
        {
            string ValueToRead = null;
            try
            {
                ValueToRead = Environment.GetEnvironmentVariable(Name);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
               return null;
            }
            return ValueToRead;
        }

        public static string EncryptoRSA(string PlainText, string PublicKey)
        {
            string ResultOfEncrypted = null;
            try
            {

                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
                {
                    rsa.FromXmlString(PublicKey);


                    byte[] EncryptedData = rsa.Encrypt(System.Text.Encoding.UTF8.GetBytes(PlainText),false);

                    ResultOfEncrypted = Convert.ToBase64String(EncryptedData);

                }

            }
            catch (CryptographicException ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }

            return ResultOfEncrypted;
        }

        public static string DecryptoRSA(string cipherText, string PrivateKey)
        {
            string ResultOfDencrypted = null;

            try
            {
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
                {
                    rsa.FromXmlString(PrivateKey);

                    byte[] DecryptedData = rsa.Decrypt(Encoding.UTF8.GetBytes(cipherText),false);

                    ResultOfDencrypted = Convert.ToBase64String(DecryptedData);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null ;
            }

            return ResultOfDencrypted;
        }


        public static string HidePassword(string OriginPassword)
        {
            string NewPassword = string.Empty;

            for (byte i = 0; i < OriginPassword.Length; i++)
            {

                if (i > OriginPassword.Length - 3)
                {
                    NewPassword += OriginPassword[i];
                }
                else
                {
                    NewPassword += '*';
                }
            }

            return NewPassword;
        }
      
       
    }
}
