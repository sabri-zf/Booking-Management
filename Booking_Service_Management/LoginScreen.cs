using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;


namespace booking_Management
{
    public static class LoginScreen
    {
        //public static object ConfgrationManager { get; private set; }

        public static bool LogScrren(string UserName, string Password)
        {
            if (ClsUsers.IsExistUser(UserName, Password))
            { 
              ClsUtility.User = ClsUsers.FindByUserNameAndPassword(UserName, Password);
                return true;
            }

            return false;
        }

    }
}
