using BusinessLayer.Service_Commponent;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Booking_Commponent
{
    public class ClsBooking
    {

        enum Mode { Add, Edit }
        public enum eStatus { Pending=1, Confiram=2, InProgress=3, Completed=4, Failed=5 }

       // public eStatus BookingStatus = eStatus.Pending; 

        private Mode _mode = Mode.Add;

        public int? ID { get; private set; }
        public int? UserID { get; set; }
        public ClsUsers UserInfo { get; set; }

        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }

        public int? ServiceID { get; set; }
       public ClsServices ServiceInfo { get; set; }


        public eStatus StatusID { get; set; }
        public ClsStatus StatusInfo { get; set; }

        //public decimal Price { get; set; }

        public short IntialReservationDay { get; set; }
        public decimal IntialTotalDueAmount { get; set; }

        public string Notes { get; set; }

        public int? PaymentID { get; set; }

        ClsPayments PaymentInfo { get; set; }


        static DateTime _DefaultDate = new DateTime(2000,01,01);

        public ClsBooking()
        {
            ID = null;
            UserID = null;
            DateStart = _DefaultDate;
            DateEnd = _DefaultDate;
            ServiceID = null;
            StatusID = eStatus.Pending;
          //?  Price = 0;
            IntialReservationDay = 0;
            IntialTotalDueAmount = 0;
            Notes = null;
            PaymentID = null;

            _mode = Mode.Add;
        }


        private ClsBooking(int ID, int userID, DateTime dateStart, DateTime dateEnd, 
            int serviceID, byte statusID,short intialReservationDay, decimal totalDueAmount, string notes, int paymentID)
        {

            this.ID = ID;
            this.UserID = userID;
            this.DateStart = dateStart;
            this.DateEnd = dateEnd;
            this.ServiceID = serviceID;
            this.StatusID = (eStatus)statusID;
            this.IntialReservationDay = intialReservationDay;
            this.IntialTotalDueAmount = totalDueAmount;
            this.Notes = notes;
            this.PaymentID = paymentID;
            //

            this._mode = Mode.Edit;

            // composision

            this.UserInfo = ClsUsers.FindByID(userID);

            this.StatusInfo = ClsStatus.FindByID((sbyte)statusID);
            this.ServiceInfo = ClsServices.FindByID(serviceID);

            this.PaymentInfo = ClsPayments.FindByID(paymentID);
        }



        public bool UpdateStatus(eStatus Mode)
        {

            if (Booking.UpdateStatus(this.ID, (byte)Mode))
            {

            this.StatusID = Mode;
                return true;

            }

            return false;
        }

        public static ClsBooking FindBookingByID(int ID)
        {

            int UserID = -1 , ServiceID = -1 ,PaymentID = -1 ;
            byte statusID = 0;
            short IntialReservitionDay = 0 ;
            decimal IntialTotalDueAmount = 0 ;
            DateTime dateStart = _DefaultDate,dateEnd = _DefaultDate;
            string Notes = string.Empty;

            if (Booking.FindByID(ID, ref UserID, ref dateStart, ref dateEnd, ref ServiceID, ref statusID
                , ref IntialReservitionDay, ref IntialTotalDueAmount, ref Notes, ref PaymentID))
            {
                return new ClsBooking(ID, UserID, dateStart, dateEnd, ServiceID, statusID, IntialReservitionDay, IntialTotalDueAmount
                    , Notes, PaymentID);
            }
            return null;
        }

        public static ClsBooking FindBookingByUserID(int UserID)
        {

            int ID = -1, ServiceID = -1, PaymentID = -1;
            byte statusID = 0;
            short IntialReservitionDay = 0;
            decimal IntialTotalDueAmount = 0;
            DateTime dateStart = _DefaultDate, dateEnd = _DefaultDate;
            string Notes = string.Empty;

            if (Booking.FindByUserID(ref ID,  UserID, ref dateStart, ref dateEnd, ref ServiceID, ref statusID
                , ref IntialReservitionDay, ref IntialTotalDueAmount, ref Notes, ref PaymentID))
            {
                return new ClsBooking(ID, UserID, dateStart, dateEnd, ServiceID, statusID, IntialReservitionDay, IntialTotalDueAmount
                    , Notes, PaymentID);
            }
            return null;
        }

        private bool _AddNew()
        {
            this.ID = Booking.AddNewBooking(this.UserID,this.DateStart,this.DateEnd,this.ServiceID,(byte)this.StatusID,this.IntialReservationDay,this.IntialTotalDueAmount
                ,this.Notes,this.PaymentID);
            return (this.ID != null && this.ID > 0);
        }

        private bool _Update()
        {
            return Booking.UpdateBooking(this.ID, this.UserID, this.DateStart, this.DateEnd, this.ServiceID, (byte)this.StatusID, this.IntialReservationDay, this.IntialTotalDueAmount
                , this.Notes, this.PaymentID);
        }

        public bool Save()
        {
            switch (_mode)
            {
                case Mode.Add:

                    if (_AddNew())
                    {
                        _mode = Mode.Edit;
                        return true;
                    }

                    return false;

                case Mode.Edit:

                    return _Update();
            }

            return false;
        }

        public static bool Delete(int ID)
        {
            return Booking.DetetBooking(ID);
        }


        public static async Task<DataTable> GetEachBookingAsync()
        {
            return await Booking.GetListsOFBookingAsync();
        }


    }
}
