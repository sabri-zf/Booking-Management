using BusinessLayer;
using BusinessLayer.Booking_Commponent;
using BusinessLayer.DetailsPeople;
using BusinessLayer.Service_Commponent;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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


        enum pickToWayService
        {
            ProgressService = 1,
            FavoritService = 2,
            GoBack = 3,
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
                    Console.Clear();
                    ShowOffer();
                    BackToMenu();
                    //MenuOption();
                    break;
                case OptionMenu.YourProFile:
                    Console.Clear();
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
            //Console.Clear();
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

        private static  void ViewShooes(OptionOfBookingPage Result)
        {
            switch (Result)
            {
                case OptionOfBookingPage.ShowService:
                    Console.Clear();
                    ServiceListAsync();
                    BackToOffer();
                    break;
                case OptionOfBookingPage.ShowFavoriteService:
                    Console.Clear();
                    FavoriteServiceList();
                    BackToOffer();
                    break;
                case OptionOfBookingPage.ShowHistory:
                    Console.Clear();
                    BookingHistory();
                    break;
                case OptionOfBookingPage.Exit:
                    Console.Clear();
                    BackToMenu();
                    break;
            }
        }

        private static void BookingHistory()
        {
            Console.WriteLine("History Booking");
        }

        private static void FavoriteServiceList()
        {
            Console.WriteLine("Coming Soon .... :) ");
        }

        private static  void ServiceListAsync()
        {
            Console.WriteLine("\n+++++++++++++++ New Service Offer +++++++++++++++\n");
            
            Task<DataTable> ListOfService =   ClsServices.GetEachService();

             ListOfService.Wait();

            int count = 1;

            Console.WriteLine("\n:::::::::::::::::::::::::::::::::::::::::::::::::\n");

            foreach (DataRow Service in ListOfService.Result.Rows)
            {
                Console.WriteLine($"Service {count} :[Category => {Clscategories.GetCategoryName((int)Service["CategoryID"])}] " +
                    $"{Service["ServiceName"]} - [{Service["ServiceProvider"]}]" +
                    $"\n Description : {Service["Description"]}");

                count++;
            }

            Console.WriteLine("\n:::::::::::::::::::::::::::::::::::::::::::::::::");


            Console.WriteLine($"\n Shooes Any Offer Between [1 - {ListOfService.Result.Rows.Count}] - Enter 12 To Go Back");

            short Number = Convert.ToInt16(Console.ReadLine());

            if(Number == 12) return;
            //Console.Clear();
            ViewServiceItem(Number);

        }

        private static void ViewServiceItem(short number)
        {
            
            Console.Clear();
            Thread.Sleep(50);

            byte value = 0;
            ClsServices Item = ClsServices.FindByID(number);

            if (Item == null) return;

            Console.WriteLine("\r:::::::::::::::::::::::::::::::::::::::::::::::::::");
            Console.WriteLine("\r:                 Service Detail                  :");
            Console.WriteLine("\r:::::::::::::::::::::::::::::::::::::::::::::::::::");
            Item.PrintService();


            DateTime LastBooked = ClsBooking.CheckLastBooked(Item.ID) != null ? ClsBooking.CheckLastBooked(Item.ID)[1]:DateTime.Now.AddDays(2);


            if (!ClsDateInfo.CompareBetweenDates(DateTime.Now, LastBooked) || !(ClsBooking.TotalExistCapacity(Item.ID) >= Item.Capacity))
            {

                Console.WriteLine("\n--------  Option -------");

                Console.WriteLine("[1] - Progress Booking ");
                Console.WriteLine("[2] - Favorite Service ");
                Console.WriteLine("[3] - Go Back ");
            }
            else
            {
                Console.WriteLine("\n\nOops! , This Service Already booked from another user \n");
                Console.WriteLine("\n--------  Option -------");
                Console.WriteLine("[1] - Go Back ");
                value = 2;
            }
            
            Console.WriteLine("------------------------");

            Console.Write("\n\nPlease pick option [ 1 - 3 ] ");
             value += Convert.ToByte(Console.ReadLine());


            ProgressTheShooes((pickToWayService)value,Item);
            // next step show to user do booking  or  add to favorite or go back 
            // another idea is in start program take for user option to shooes if already heve account or not 
            // if user not have account can create one otherwise not 

           
        }

        private static void ProgressTheShooes(pickToWayService value,ClsServices item)
        {
            switch (value)
            {
                case pickToWayService.ProgressService:
                    Console.Clear();
                    Processing_Booking(item);
                    BackToList();
                    break;
                case pickToWayService.FavoritService:
                    Console.Clear();
                    AddFavorite();
                    BackToList();
                    break;
                case pickToWayService.GoBack:
                    //Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
                    BackToList();
                    break;
            }
        }

        private static  void BackToList()
        {
            Console.SetCursorPosition(0, 0);
            Console.Clear();
           
            Console.WriteLine("\n\nPress Any Key To Enter.....");
            Console.ReadLine();
            ShowOffer();
        }

        private static void AddFavorite()
        {
          Console.Clear();

            /*
             * Create non_Relational DataBase Managment non-RDBM to save just id of Specific Service 
             * write here just if progrees done or not 
             */

            Console.WriteLine(" Favaorite Service Page ");
        }

        private static void Processing_Booking(ClsServices Item)
        {
            Console.Clear();

            ClsBooking CreateNewBooking = new ClsBooking();

            CreateNewBooking.ServiceID = Item.ID;
            CreateNewBooking.StatusID = ClsBooking.eStatus.Pending;
            CreateNewBooking.UserID = ClsUtility.User.ID;

            if(!AskUser(ref CreateNewBooking,Item)) return;



            if (CreateNewBooking.Save())
            {
                //Console.WriteLine(CreateNewBooking + "\n");
                Console.WriteLine("Processing Is Successfully :)");
                Console.ReadLine() ;

                return;
            }

            Console.WriteLine("Processing Is Failer :(");
            Console.WriteLine("Enter Any Key ... ");
            Console.ReadLine() ;
            /*
             * what we need :
             * 1 - when User validaet your order  on specific service 
             * so we are do check in the data base 
             * if :
             * I  - nessecary that Cpacity  
             * ==> check to get all booking already booked from this service and Comper between total Capaity and capacity resrvation
             * II - Thes Service is not booked another customer ,if the capacity of this service already have Just one 
             * ==> using query sql and find last booking of this service , and compare between date of user need take this service 
             * and and service is not cancel it mean not Failed...!
             */

            //if( check by logic with data already own )
            // print message to user and  
            //else


        }

        private static bool AskUser(ref ClsBooking createNewBooking,ClsServices Item)
        {
            Console.WriteLine("::::::::::::::::::::::::::::::::::");
            Console.WriteLine("\t Reservation Information");
            Console.WriteLine("::::::::::::::::::::::::::::::::::");

            DateTime LastBooked;
           if ( ClsBooking.CheckLastBooked(Item.ID) != null){

                LastBooked = ClsBooking.CheckLastBooked(Item.ID)[1];

            Console.Write($" Start Date To Take Booked  is : {LastBooked.ToShortDateString()} \n" );

               createNewBooking.DateStart = LastBooked;

            }
            else
            {

                Console.Write($" Start Date To Take Booked  is : {DateTime.Now.ToShortDateString()} \n" );
                createNewBooking.DateStart = DateTime.Now;

            }
           
            

            Console.Write("Please Enter End Date To Take Booked ");

            if (DateTime.TryParse(Console.ReadLine(), out DateTime DE))
            {
                createNewBooking.DateEnd = DE;
            }


            if (!ClsDateInfo.CompareBetweenDates(createNewBooking.DateStart, createNewBooking.DateEnd))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Oops, The Shooes of Date Of Booking Is Reservaed From Another User");
                Console.ResetColor();
                Console.ReadKey();

                return false;
            }

            createNewBooking.IntialReservationDay = ClsDateInfo.DiffBetweenDates(createNewBooking.DateStart,createNewBooking.DateEnd,true);
            createNewBooking.IntialTotalDueAmount = (ClsPrices.PriceOfService(createNewBooking.ServiceID) * createNewBooking.IntialReservationDay) ;

            createNewBooking.PaymentID =  DetailsPayment(createNewBooking.IntialTotalDueAmount);

            if(createNewBooking.PaymentID == 0) return false;

            return true;
        }

        private static int? DetailsPayment(decimal intialTotalDueAmout)
        {

            if (ClsUtility.User.PersonInfo.PaymentInfo == null)
            {

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("You have not method Payment ... !! \n");
                Console.ResetColor();

               while(!AddPaymentInformation())
                {

                    Console.WriteLine("is Very important To Add Payment infromation Successed");
                    Console.WriteLine("Try Again");
                    Console.WriteLine("Press Any Key to Continue....");
                    Console.ReadLine();
                    Console.Clear();

                }

            }
            
            
            Console.Clear();
            Console.WriteLine("::::::::::::::::::::::::::::::::::");
            Console.WriteLine("\t Payment ");
            Console.WriteLine("::::::::::::::::::::::::::::::::::");

            ClsPayments Pay = new ClsPayments();

            Pay.PaymentDate = DateTime.Now;
            Pay.IntialTotalDueAmount = intialTotalDueAmout;

            // here process of Payment With Bank do debit from card of user

            if (Pay.Save())
            {
                return  Pay.ID;
            }

            return 0;
        }

        private static bool AddPaymentInformation()
        {
            //Console.Clear();
            Console.WriteLine("::::::::::::::::::::::::::::::::::");
            Console.WriteLine("\t Payment Information ");
            Console.WriteLine("::::::::::::::::::::::::::::::::::");

            Console.WriteLine("");

            ClsUtility.User.PersonInfo.PaymentInfo = new ClsPaymentsInfo();

            // Check valdition if user ley Number not letter
            Console.Write("Enter Card Number         : ");
            if(int.TryParse(Console.ReadLine(),out int CardNum)) ClsUtility.User.PersonInfo.PaymentInfo.CardNumber = CardNum.ToString();

            Console.Write("Enter Card Holder     : ");
            ClsUtility.User.PersonInfo.PaymentInfo.CardHolderName = Console.ReadLine();

            Console.Write("Enter Expiration Date : ");
            ClsUtility.User.PersonInfo.PaymentInfo.ExpirationDate = DateTime.Parse(Console.ReadLine());

            Console.Write("Enter CVV Code        : ");
            if (short.TryParse(Console.ReadLine(), out short CVV)) ClsUtility.User.PersonInfo.PaymentInfo.CVV_Code = CVV.ToString();


            char ans = 'n';


            Console.Write("Are you certain from your detail's payment info ...?");
            ans = Convert.ToChar(Console.ReadLine());
            if( ans.ToString().ToLower() == "y")
            {
                if (ClsUtility.User.PersonInfo.PaymentInfo.Save())
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Save Is Successfully @W@ ");
                    ClsUtility.User.PersonInfo.PaymentInformationID = ClsUtility.User.PersonInfo.PaymentInfo.ID;
                    Console.ReadKey();
                    Console.ResetColor();

                    return true;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Save Is Failer @M@ ");
                Console.ResetColor();

            }
                return false;
        }
    }
}
