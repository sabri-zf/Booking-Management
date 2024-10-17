using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Booking
{
    public class ClsStatus
    {

        public byte ID { get; private set; }    
        public string StatusName { get;set; }


        ClsStatus(byte iD, string statusName)
        {
            this.ID = iD;
            this.StatusName = statusName;
        }


        public static ClsStatus? FindByID(byte ID)
        {
            return null;
        }

        public static ClsStatus? FindStatusByName(string StatusName)
        {
            return null;
        }


        static string? GetStatusName(int ID)
        {
            return null;
        }

        static byte? GetStatusID(string Status)
        {
            return null;
        }
    }
}
