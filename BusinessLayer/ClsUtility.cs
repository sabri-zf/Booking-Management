using System;
using System.Collections.Generic;
using System.Linq;
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
