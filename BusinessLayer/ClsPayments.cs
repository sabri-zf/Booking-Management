using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;

namespace BusinessLayer
{
    public class ClsPayments
    {
        enum Mode { Add, Edit }

        Mode _mode = Mode.Add;
        public int? ID { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal IntialTotalDueAmount { get; set; }
        public decimal? ActualTotalDueAmount { get; set; }
        public decimal? TotalRemainingCost { get; set; }
        public decimal? TotalAddationalCost { get; set; }
        public DateTime UpdatePaymentDate { get; set; }

        private static DateTime DefaultDateTime = new DateTime(2000, 01, 01);
        public ClsPayments()
        {
            this.ID = null;
            this.PaymentDate = DefaultDateTime;
            this.IntialTotalDueAmount = 0;
            this.ActualTotalDueAmount = 0;
            this.TotalRemainingCost = 0;
            this.TotalAddationalCost = 0;
            this.UpdatePaymentDate = DefaultDateTime;

            this._mode = Mode.Add;
        }


        private ClsPayments(int ID, DateTime paymentDate, decimal intialTotalDueAmount, decimal? acutalTotalDueAmount, decimal? totalRemainingCost, decimal? totalAddationalCost, DateTime updatePaymentDate)
        {
            this._mode = Mode.Edit;
            this.ID = ID;
            this.PaymentDate = paymentDate;
            this.IntialTotalDueAmount = intialTotalDueAmount;
            this.ActualTotalDueAmount = acutalTotalDueAmount;
            this.TotalRemainingCost = totalRemainingCost;
            this.TotalAddationalCost = totalAddationalCost;
            this.UpdatePaymentDate = updatePaymentDate;
        }

        public static ClsPayments FindByID(int ID)
        {
            DateTime PaymentDate = DefaultDateTime, UpdatePaymentDate = DefaultDateTime;
            decimal IntinalTotalDueAmount = 0;
            decimal? ActualTotalDueAmount = 0,TotalRemainingCost = 0, TotalAddationalCost = 0;

            if (Payments.FindByID(ID, ref PaymentDate, ref IntinalTotalDueAmount, ref ActualTotalDueAmount, ref TotalRemainingCost,
                ref TotalAddationalCost, ref UpdatePaymentDate))
            {
                return new ClsPayments(ID, PaymentDate, IntinalTotalDueAmount, ActualTotalDueAmount, TotalRemainingCost, TotalAddationalCost,UpdatePaymentDate);
            }

            return null;
        }

        public static ClsPayments FindByDate(DateTime PaymentDate)
        {
            int ID = -1;
            DateTime UpdatePaymentDate = DefaultDateTime;
            decimal IntinalTotalDueAmount = 0;
            decimal? ActualTotalDueAmount = 0, TotalRemainingCost = 0, TotalAddationalCost = 0;

            if (Payments.FindPaymentDate(ref ID,  PaymentDate, ref IntinalTotalDueAmount, ref ActualTotalDueAmount, ref TotalRemainingCost,
                ref TotalAddationalCost, ref UpdatePaymentDate))
            {
                return new ClsPayments(ID, PaymentDate, IntinalTotalDueAmount, ActualTotalDueAmount, TotalRemainingCost, TotalAddationalCost, UpdatePaymentDate);
            }

            return null;
        }

        public static bool Delete(int ID)
        {
            return Payments.DeletePayment(ID);
        }

        public static bool Delete(DateTime PaymentDate)
        {
            return Payments.DeletePayment(PaymentDate);
        }

        private bool _AddNew()
        {
            this.ID = Payments.AddNewPayment(this.PaymentDate,this.IntialTotalDueAmount,this.ActualTotalDueAmount,
                this.TotalRemainingCost,this.TotalAddationalCost,this.UpdatePaymentDate);

            return (this.ID != null && this.ID > 0);
        }

        private bool _Update()
        {
            return Payments.UpdatePayment(this.ID,this.PaymentDate,this.IntialTotalDueAmount,this.ActualTotalDueAmount,
                this.TotalRemainingCost,this.TotalAddationalCost,this.UpdatePaymentDate);
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


        public static async Task<DataTable> GetEachPayments()
        {
            return await Payments.GetEachPaymentsAsync();
        }
    }
}
