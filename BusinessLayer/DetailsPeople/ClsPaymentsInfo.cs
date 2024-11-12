using DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
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
                string privateKey = Environment.GetEnvironmentVariable(ID.ToString() + " Private");
                return new ClsPaymentsInfo(ID, ClsUtility.DecryptoRSA(cardNumber, privateKey), ClsUtility.DecryptoRSA(cardHolderName, privateKey), expirationDate, ClsUtility.DecryptoRSA(CVV_Code, privateKey));
            }
            return null;
        }

        public static ClsPaymentsInfo FindByCardHolderName(string CardHolderName)
        {
            int ? ID = null;
            string cardNumber = string.Empty, CVV_Code = string.Empty;
            DateTime expirationDate = DateTime.Now;

            if (PaymentsInformation.FindByHolderName(ref ID, ref cardNumber, CardHolderName, ref expirationDate, ref CVV_Code))
            {
                string privateKey = Environment.GetEnvironmentVariable(ID.ToString() + " Private");
                return new ClsPaymentsInfo(ID,ClsUtility.DecryptoRSA(cardNumber,privateKey),ClsUtility.DecryptoRSA( CardHolderName,privateKey), expirationDate,ClsUtility.DecryptoRSA( CVV_Code,privateKey));
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
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {

                string publicKey = rsa.ToXmlString(false);
                string privateKey = rsa.ToXmlString(true);
                this.ID = PaymentsInformation.AddNewPaymentInfo(ClsUtility.EncryptoRSA(CardNumber,publicKey), ClsUtility.EncryptoRSA(CardHolderName,publicKey),ExpirationDate, ClsUtility.EncryptoRSA(CVV_Code, publicKey));

                if((this.ID.HasValue || this.ID > 0))
                {

                    ClsUtility.WriteDataToEnvironmentVar(this.ID.ToString()+" Private", privateKey);
                    ClsUtility.WriteDataToEnvironmentVar(this.ID.ToString()+" Public", publicKey);
                    return true;

                }

                return false;
            }
        }
        private bool _Update()
        {
            string publicKey = Environment.GetEnvironmentVariable(this.ID.ToString() + " Public");
            return PaymentsInformation.UpdatePaymentInfo(this.ID,ClsUtility.EncryptoRSA(CardNumber, publicKey), ClsUtility.EncryptoRSA(CardHolderName, publicKey), ExpirationDate, ClsUtility.EncryptoRSA(CVV_Code, publicKey));
        }


        public static DataTable GetAllPaymentsInformation()
        {
            return PaymentsInformation.GetEachPaymentInformationCard();
        }




    }

}
