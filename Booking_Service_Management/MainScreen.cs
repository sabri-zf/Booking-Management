using BusinessLayer;
using BusinessLayer.Service_Commponent;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace booking_Management
{
    public class MainScreen
    {

        enum OptionMenu
        {
            ShowBookingOffer = 1,
            YourProFile = 2,
            Exit = 3,
        }


        enum OptionOfBookingPage
        {
            ShowService = 1,
            ShowFavoriteService = 2,
            ShowHistory = 3,
            Exit = 4,
        }
        public static void RunMain()
        {
            Console.Clear();

            MenuOption();
        }

        private static void MenuOption()
        {
            Console.Clear();   
            Console.WriteLine(":::::::::::::::::::::::::::::::::::::::::::::::::::");
            Console.WriteLine(":                  MENU Screen                    :");
            Console.WriteLine(":::::::::::::::::::::::::::::::::::::::::::::::::::");

            Console.WriteLine("[1] - Show Booking Offer ");
            Console.WriteLine("[2] - Your Profile ");
            Console.WriteLine("[3] - Exit ");

            Console.WriteLine("...................................................");

            Console.Write("Enter Number Between [1-3] ");
            byte Value = Convert.ToByte(Console.ReadLine());

            try
            {
                ShooesAndAction((OptionMenu)Value);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        private static void ShooesAndAction(OptionMenu value)
        {
            switch (value)
            {
                case OptionMenu.ShowBookingOffer:
                    ShowOffer();
                    BackToMenu();
                    //MenuOption();
                    break;
                case OptionMenu.YourProFile:
                    ProfalieOfUser();
                    BackToMenu();
                    //MenuOption();
                    break;
                case OptionMenu.Exit:
                    BackToLogIn();
                    break;

                default:
                    throw new Exception("Error Exception ... !");

            }

        }

        private static void BackToLogIn()
        {
            byte Cycle = 0;
            while (Cycle <= 3)
            {
                Console.Clear() ;
                Console.Write("Exit Is Progress");
                Console.Write(' ');
                Thread.Sleep(100);
                Console.Write('.');
                Thread.Sleep(100);
                Console.Write('.');
                Thread.Sleep(100);
                Console.Write('.');
                Thread.Sleep(100);
                Console.Write('!');
                Task.Delay(300).Wait();
                Cycle++;
            }

            Task.Run(() =>
            {
                ClsUtility.User = null;

               Console.Clear() ;
               Program.StartProgram();
                
            } ).Wait();

        }

        private static void BackToMenu()
        {
           
            Console.WriteLine("\n\nPress Any Key To Enter.....");
            Console.ReadLine();
            MenuOption();
        }

        private static void BackToOffer()
        {

            Console.WriteLine("\n\nPress Any Key To Enter.....");
            Console.ReadLine();
            ShowOffer();
        }

        private static void ProfalieOfUser()
        {
            Console.Clear();
            Console.WriteLine(":::::::::::::::::::::::::::::::::::::::::::::::::::");
            Console.WriteLine(":                Profile Screen                   :");
            Console.WriteLine(":::::::::::::::::::::::::::::::::::::::::::::::::::");

            Console.WriteLine();
            Console.WriteLine(ClsUtility.User);
        }

        private static void ShowOffer()
        {
            Console.Clear();

            Console.WriteLine(":::::::::::::::::::::::::::::::::::::::::::::::::::");
            Console.WriteLine(":                 Booking Screen                  :");
            Console.WriteLine(":::::::::::::::::::::::::::::::::::::::::::::::::::");

            Console.WriteLine("[1] - Show Service List     ");
            Console.WriteLine("[2] - Show Favorite Service ");
            Console.WriteLine("[3] - Show History Booking  ");
            Console.WriteLine("[4] - Exit ");

            Console.WriteLine("...................................................");

            Console.Write("Enter Number Between [1-4] ");
            byte Value = Convert.ToByte(Console.ReadLine());

            ViewShooes((OptionOfBookingPage)Value);

        }

        private static async void ViewShooes(OptionOfBookingPage Result)
        {
            switch (Result)
            {
                case OptionOfBookingPage.ShowService:
                   await ServiceListAsync();
                    BackToOffer();
                    break;
                case OptionOfBookingPage.ShowFavoriteService:
                    FavoriteService();
                    BackToOffer();
                    break;
                case OptionOfBookingPage.ShowHistory:
                    BookingHistory();
                    break;
                case OptionOfBookingPage.Exit:
                    BackToOffer();
                    break;
            }
        }

        private static void BookingHistory()
        {
            Console.WriteLine("History Booking");
        }

        private static void FavoriteService()
        {
            Console.WriteLine("Coming Soon .... :) ");
        }

        private static async Task ServiceListAsync()
        {
           Console.Clear();
            Console.WriteLine(":::::::::::::::::::::::::::::::::::::::::::::::::::");
            Console.WriteLine(":                 Service Screen                  :");
            Console.WriteLine(":::::::::::::::::::::::::::::::::::::::::::::::::::");

            Console.WriteLine("\n::::::::::::::::New Service Offer::::::::::::::::");
            Console.WriteLine("\n:                                               :");

            Task<DataTable> ListOfService =   ClsServices.GetEachService();

            await ListOfService;

            int count = 1;
            
            foreach (DataRow Service in ListOfService.Result.Rows)
            {
                Console.WriteLine($":Service {count} : {Service["ServiceName"]} - [{Service["ServiceProvider"]}]:");
                count++;
            }

            Console.WriteLine("\n:::::::::::::::::::::::::::::::::::::::::::::::::::");

            Console.WriteLine($"\n Shooes Any Offer Between [1- {ListOfService.Result.Rows.Count}]");

            short Number = Convert.ToInt16(Console.ReadLine());

            ViewServiceItem(Number);

        }

        private static void ViewServiceItem(short number)
        {
            Console.Clear();
            ClsServices Item = ClsServices.FindByID(number);

            if (Item == null) return;
            
                Console.WriteLine(Item);


            // next step show to user do booking  or  add to favorite or go back 
            // another idea is in start program take for user option to shooes if already heve account or not 
            // if user not have account can create one otherwise not 

           
        }
    }
}
