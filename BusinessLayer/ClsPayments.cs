using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class ClsPayments
    {
        enum Mode { Add,Edit}

        Mode _mode = Mode.Add;
        public int? ID {  get; set; }
        public DateTime? PaymentDate { get; set; }
        public decimal IntialTotalDueAmount { get; set; }
        public decimal? AcutalTotalDueAmount { get; set; }
        public decimal? TotalRemainingCost { get; set; }
        public decimal? TotalAddationalCost { get; set; }
        public DateTime? UpdatePaymentDate { get; set; }

        public ClsPayments() 
        {
            this.ID = null;
            this.PaymentDate = null;
            this.IntialTotalDueAmount = 0;
            this.AcutalTotalDueAmount = 0;
            this.TotalRemainingCost = 0;
            this.TotalAddationalCost = 0;
            this.UpdatePaymentDate = null;

            this._mode = Mode.Add;
        }


        private ClsPayments( int ID, DateTime paymentDate, decimal intialTotalDueAmount, decimal? acutalTotalDueAmount, decimal? totalRemainingCost, decimal? totalAddationalCost, DateTime? updatePaymentDate)
        {
            this._mode = Mode.Edit;
            this.ID = ID;
            this.PaymentDate = paymentDate;
            this.IntialTotalDueAmount = intialTotalDueAmount;
            this.AcutalTotalDueAmount = acutalTotalDueAmount;
            this.TotalRemainingCost = totalRemainingCost;
            this.TotalAddationalCost = totalAddationalCost;
            this.UpdatePaymentDate = updatePaymentDate;
        }

        public static ClsPayments? FindByID(int ID)
        {
            return null;
        }

        public static ClsPayments? FindByDate(DateTime Date)
        {
            return null;
        }

        public static bool Delete(int ID)
        {
            return false;
        }

        private bool _AddNew()
        {
            return false;
        }

        private bool _Update()
        {
            return false;
        }


        public bool Save()
        {
            return false;
        }


        public static DataTable? GetAllTransaction()
        {
            return null;
        }

    }
}
