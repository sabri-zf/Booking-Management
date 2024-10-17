using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LogicLayer.Booking
{
    public class ClsBooking
    {

        enum Mode { Add, Edit }
        public enum eStatus { Pending, Confiram, InProgress, Completed, Failed }

        //public eStatus BookingStatus = eStatus.Pending; 
        private Mode _mode = Mode.Add;

        public int? ID { get; private set; }
        public int? UserID { get; set; }
        public ClsUsers? UserInfo { get; set; }

        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }

        public int? ServiceID { get; set; }
        public ClsService? ServiceInfo { get; set; }
       

        public eStatus Status { get; set; }
        public ClsStatus? StatusInfo { get; set; }
        public decimal Price { get; set; }

        public short IntialReservationDay { get; set; }
        public decimal TotalDueAmount { get; set; }

        public string? Notes { get; set; }

        public int? PaymentID { get; set; }

        ClsPayments? PaymentInfo { get; set; }




        public ClsBooking()
        {
            ID = null;
            UserID = null;
            DateStart = null;
            DateEnd = null;
            ServiceID = null;
            Status = eStatus.Pending;
            Price = 0;
            IntialReservationDay = 0;
            TotalDueAmount = 0;
            Notes = null;
            PaymentID = null;

            _mode = Mode.Add;
        }


        private ClsBooking(int ID, int userID, DateTime dateStart, DateTime dateEnd, int serviceID, eStatus status, decimal price, short intialReservationDay, decimal totalDueAmount, string? notes, int paymentID)
        {

            this.ID = ID;
            this.UserID = userID;
            this.DateStart = dateStart;
            this.DateEnd = dateEnd;
            this.ServiceID = serviceID;
            this.Status = status;
            this.Price = price;
            this.IntialReservationDay = intialReservationDay;
            this.TotalDueAmount = totalDueAmount;
            this.Notes = notes;
            this.PaymentID = paymentID;
            //

            this._mode = Mode.Edit;

            // composision

            this.UserInfo = ClsUsers.FindByID(userID);
            this.StatusInfo = ClsStatus.FindByID((byte)status);
            this.ServiceInfo = ClsService.FindByID(serviceID);
            this.PaymentInfo = ClsPayments.FindByID(paymentID);
        }



        public void UpdateStatus(eStatus Mode)
        {
            this.Status = Mode;
        }

        public void UpdatePrice(decimal Money)
        {
            this.Price = Money;
        }



        public static ClsBooking? FindBookingByID(int ID)
        {
            return null;
        }

        private bool _AddNew()
        {
            return true;
        }
        private bool _Update()
        {
            return true;
        }

        public bool Save()
        {
            return true;
        }

        public static bool Delete(int ID)
        {
            return false;
        }


        public static DataTable? GetEachBooking()
        {
            return null;
        }



    }
}
