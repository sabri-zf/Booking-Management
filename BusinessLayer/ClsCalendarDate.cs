
using System;

namespace BusinessLayer
{
    public static  class ClsCalendarDate
    {


       //public static DateInfo Date { get; set; }

        private static bool CheckGoodReservationByYear(ClsDateInfo Date_A , ClsDateInfo Date_B)
        {
            return ((Date_A.DateStar.Year > Date_B.DateEnd.Year) || (Date_A.DateEnd.Year < Date_B.DateStar.Year)); 
            // this mean The year of Date of User is Grate than Date of Last User Reservatin
        }

        private static bool CheckGoodReservationByMonth(ClsDateInfo Date_A, ClsDateInfo Date_B)
        {
           return ((Date_A.DateStar.Month > Date_B.DateEnd.Month) || (Date_A.DateEnd.Month < Date_B.DateStar.Month));
        }

        private static bool CheckGoodReservationByDay(ClsDateInfo Date_A, ClsDateInfo Date_B)
        {
            return ((Date_A.DateStar.Day > Date_B.DateEnd.Day) || (Date_A.DateEnd.Day < Date_B.DateStar.Day));

        }
        public static bool IsOverLap(ClsDateInfo DateOne, ClsDateInfo DateTwo)
        {

            if (CheckGoodReservationByYear(DateOne, DateTwo))
            {
                return true;

            }

            if (CheckGoodReservationByMonth(DateOne, DateTwo))
            {
                return true;

            }
            
            if (CheckGoodReservationByDay(DateOne, DateTwo))
            {
                return true;
            }

            return false;
        }
        public static TimeSpan Duration(ClsDateInfo Date)
        {

            TimeSpan DurationTime = new TimeSpan();

            DurationTime = Date.DateEnd.Subtract(Date.DateStar);

            return DurationTime;
        }


    }
}
