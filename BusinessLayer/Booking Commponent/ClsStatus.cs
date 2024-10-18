using DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Booking_Commponent
{
    public class ClsStatus
    {


        public sbyte ID { get; private set; }
        public string StatusName { get; set; }


        ClsStatus(sbyte iD, string statusName)
        {
            this.ID = iD;
            this.StatusName = statusName;
        }


        public static ClsStatus FindByID(sbyte ID)
        {
            string StatusName = string.Empty;

            if (StatusOfBooking.FindByID(ID ,ref StatusName))
            {
                return new ClsStatus(ID, StatusName);
            }

            return null;
        }

        /*
         public static ClsStatus FindStatusByName(string StatusName)
        {
            sbyte ID = -1;

            if (true)
            {
                return new ClsStatus(ID, StatusName);
            }
            return null;
        }
        */


        static string GetStatusName(sbyte ID)
        {
            return StatusOfBooking.GetStatusName(ID);
        }

        //static byte GetStatusID(string Status)
        //{
        //    return null;
        //}


        public static DataTable ListOfStatusValue()
        {
            return StatusOfBooking.GetAllStatusValue();
        }
    }
}
