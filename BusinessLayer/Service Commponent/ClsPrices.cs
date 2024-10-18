using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service_Commponent
{
    public class ClsPrices
    {

        enum Mode { Add, Edit }
        Mode? _mode = null;
        public int? ID { get; private set; }

        public decimal Amount { get; set; }
        public int? ServiceID { get; set; }
        public ClsServices ServiceInfo { get; set; }

        public ClsPrices()
        {
            this.ID = null;
            this.Amount = 0;
            this.ServiceID = null;

            this._mode = Mode.Add;
        }

        private ClsPrices(int iD, decimal amount, int serviceID)
        {

            this.ID = iD;
            this.Amount = amount;
            this.ServiceID = serviceID;

            this._mode = Mode.Edit;

            this.ServiceInfo = ClsServices.FindByID(serviceID);
        }

        public static ClsPrices FindByService(int serviceID)
        {
            return null;
        }

        public static bool Delete(int ID)
        {
            return false;
        }
        public static bool Delete(decimal Amount)
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

        public DataTable GetEachPrice()
        {
            return null;
        }
    }
}
