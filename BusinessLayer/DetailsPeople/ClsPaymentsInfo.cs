using DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DetailsPeople
{
    public class ClsPaymentsInfo
    {

        enum Mode { Add, Edit }

        Mode _mode;
        public int? ID { get; private set; }

        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }

        public DateTime ExpirationDate { get; set; }

        public string CVV_Code { get; set; }


        public ClsPaymentsInfo()
        {
            this.ID = null;
            this.CardNumber = string.Empty;
            this.CardHolderName = string.Empty;
            this.ExpirationDate = DateTime.Now;
            this.CVV_Code = string.Empty;
            this._mode = Mode.Add;
        }


        private ClsPaymentsInfo(int? ID, string CardNumber, string CardHolderName, DateTime ExpirationDate, string CVV_Code)
        {
            this.ID = ID;
            this.CardNumber = CardNumber;
            this.CardHolderName = CardHolderName;
            this.ExpirationDate = ExpirationDate;
            this.CVV_Code = CVV_Code;
            this._mode = Mode.Edit;
        }


        public static ClsPaymentsInfo FindByID(int? ID)
        {
            string cardNumber = string.Empty, cardHolderName = string.Empty, CVV_Code = string.Empty;
            DateTime expirationDate = DateTime.Now;

            if (PaymentsInformation.FindByID(ID, ref cardNumber, ref cardHolderName, ref expirationDate, ref CVV_Code))
            {
                return new ClsPaymentsInfo(ID, cardNumber, cardHolderName, expirationDate, CVV_Code);
            }
            return null;
        }

        public static ClsPaymentsInfo FindByCardHolderName(string CardHolderName)
        {
            int? ID = null;
            string cardNumber = string.Empty, CVV_Code = string.Empty;
            DateTime expirationDate = DateTime.Now;

            if (PaymentsInformation.FindByHolderName(ref ID, ref cardNumber, CardHolderName, ref expirationDate, ref CVV_Code))
            {
                return new ClsPaymentsInfo(ID, cardNumber, CardHolderName, expirationDate, CVV_Code);
            }
            return null;
        }
        public static bool Delete(int ID)
        {
            return PaymentsInformation.DeletePaymentInfo(ID);
        }
        public bool Save()
        {
            switch (_mode)
            {
                case Mode.Add:
                    if (_AddNew())
                    {
                        _mode = Mode.Add;
                        return true;
                    }
                    return false;
                case Mode.Edit:
                    return _Update();
            }

            return false;
        }
        private bool _AddNew()
        {
            this.ID = PaymentsInformation.AddNewPaymentInfo(CardNumber, CardHolderName, ExpirationDate, CVV_Code);

            return (this.ID != null || this.ID <= 0);
        }
        private bool _Update()
        {
            return PaymentsInformation.UpdatePaymentInfo(this.ID, this.CardNumber, this.CardHolderName, this.ExpirationDate, this.CVV_Code);
        }


        public static DataTable GetAllPaymentsInformation()
        {
            return PaymentsInformation.GetEachPaymentInformationCard();
        }




    }

}
