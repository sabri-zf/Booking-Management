using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer;
using BusinessLayer.DetailsPeople;
using BusinessLayer.EnventHandler;
using BusinessLayer.Service_Commponent;
//using BusinessLayer;

namespace booking_Management
{
    internal class Program
    {

       
        static void Main(string[] args)
        {

           

           StartProgram();

            Console.ReadKey();
        
        }

        private static void StartProgram()
        {
            
            int count = 0;
            while (count <= 3)
            {
                Console.Clear();
                Console.ResetColor();


                if (count > 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"Plese Remember your UserName And Password , You Have Just {3- count} Tried Again");
                    Console.ResetColor();
                }

                //ClsUsers clsUsers = new ClsUsers();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.BackgroundColor = ConsoleColor.White;
                Console.WriteLine(":::::::::::::::::::::: Login Screen :::::::::::::::::::::::::::::::::");
                Console.ResetColor();

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

            if(count < 3)
            MainScreen.RunMain();
            else
                return;
        }
    }
}
