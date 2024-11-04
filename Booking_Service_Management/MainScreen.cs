using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace booking_Management
{
    public class MainScreen
    {
        public static void RunMain()
        {
            Console.Clear();

            SayHello();
        }

        private static void SayHello()
        {
            Console.WriteLine($"Hello MR ,{ClsUtility.User.UserName} Welcome To Booking Reservation >L:");
        }
    }
}
