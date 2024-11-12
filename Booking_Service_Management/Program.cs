using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer;
using BusinessLayer.Booking_Commponent;
using BusinessLayer.DetailsPeople;
using BusinessLayer.EnventHandler;
using BusinessLayer.Extensions;
using BusinessLayer.Service_Commponent;
//using BusinessLayer;

namespace booking_Management
{
    public class Program
    {

       
        static void Main(string[] args)
        {



            //Console.WriteLine(DateTime.Compare(new DateTime(2024, 10, 25), new DateTime(2024, 10, 24)));


            //Console.WriteLine(ClsDateInfo.DiffBetweenDates(new DateTime(2023, 10, 21), new DateTime(2023, 10, 23),true));



            Console.SetCursorPosition(0, 0);
          StartProgram();

            Console.ReadKey();
        
        }

        public static void StartProgram()
        {
            
            byte count = 1;

            while (count <= 3)
            {
                Console.Clear();
                //Console.ResetColor();

                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(":::::::::::::::::::::: Login Screen :::::::::::::::::::::::::::::::::");
                Console.ResetColor();

                if (count > 1)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"\nAttention !\n Plese Remember your UserName And Password , You Have Just {4 - count} Tried Again\n");
                    Console.ResetColor();
                }

                Console.Write("\n Enter User Name  => ");
                string UserName = Console.ReadLine();
                Console.Write(" Enter PassWord   => ");
                string Password = Console.ReadLine();

                if(LoginScreen.LogScrren(UserName, Password))
                {
                    break;
                }else
                {
                    Console.BackgroundColor= ConsoleColor.Red;
                    Console.WriteLine("Invalid UserName And Password...!");
                    Console.ResetColor();
                    count++;
                }
            }

            if(count <= 3)
            MainScreen.RunMain();
            else
                return;
        }
    }
}
