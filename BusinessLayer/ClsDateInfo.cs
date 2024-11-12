using System;


namespace BusinessLayer
{


    // in next featrue Try To write code using Exantion method
    public class ClsDateInfo
    {

        private byte[] DefaultDayOfMonth = { 0, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        public DateTime DateStar { get; set; }
        public DateTime DateEnd { get; set; }

        public ClsDateInfo(DateTime DateStar, DateTime DateEnd)
        {

            if (CheckIsDayinRangeDayOfMonth(DateStar) && CheckIsDayinRangeDayOfMonth(DateEnd)) {

                if (CheckIsDayinRangeMonthOfYear(DateStar) && CheckIsDayinRangeMonthOfYear(DateEnd)){

                        if (CheckIsDayinRangeYear(DateStar) && CheckIsDayinRangeYear(DateEnd)) {
                        {
                            this.DateStar = DateStar;
                            this.DateEnd = DateEnd;
                        }
                    }
                }
            }

            throw new NotImplementedException();

           //throw new ArgumentException("The Date Range Is Not Valid....");
        }



        private bool IsLapYear(int year)
        {
            return ((year % 4 == 0 && year % 100 != 0) || (year % 400 == 0));
        }

        private bool CheckIsDayinRangeDayOfMonth(DateTime date)
        {
            byte Days = (IsLapYear(date.Year) && date.Month == 2 ? (byte)(DefaultDayOfMonth[date.Month] + 1): DefaultDayOfMonth[date.Month]);

            return (date.Day >= 1 && date.Day <= Days);
        }

        private bool CheckIsDayinRangeMonthOfYear(DateTime date)
        {
            return (date.Month >= 1 && date.Month <= 12);
        }
        private bool CheckIsDayinRangeYear(DateTime date)
        {
            int year = DateTime.Now.Year;
            return (date.Year >= year && date.Month <= 9999);
        }



        public static bool CompareBetweenDates(DateTime getReservationDateOfUser,DateTime getEndDateOfLastReservation)
        {
            sbyte CheckCompare = (sbyte)DateTime.Compare(getReservationDateOfUser, getEndDateOfLastReservation);
            return CheckCompare == -1  || CheckCompare == 0;
        }


        public static short DiffBetweenDates(DateTime DateStart, DateTime DateEnd,bool includeCurrentDay=false)
        {
            short Diff = 0;
            while (DateStartBeforDateEnd(DateStart, DateEnd))
            {

                //immurable Struct 
                DateStart = DateStart.AddDays(1);
                Diff++;

            }

            return (includeCurrentDay) ? ++Diff : Diff;
        }

        private static bool DateStartBeforDateEnd(DateTime dateStart, DateTime dateEnd)
        {
            return (dateStart.Year < dateEnd.Year) ? true : ((dateStart.Year == dateEnd.Year) ? ((dateStart.Month < dateEnd.Month) ? true : ((dateStart.Month == dateEnd.Month) ? (dateStart.Day < dateEnd.Day) : false)) : false);
        }
    }


}
