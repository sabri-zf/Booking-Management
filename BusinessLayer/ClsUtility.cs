using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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


       
    }
}
