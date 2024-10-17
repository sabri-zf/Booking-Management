using System.Data;
using System;
using LogicLayer.Interface;
using DataLayer;
using System.Text;
namespace LogicLayer.PeopleModule
{
    public class ClsPerson : IPerson
    {

        enum Mode { Add, Edit }
        Mode _mode;
        public int? ID { get; private set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime? BirthDay { get; set; }

        public byte? Age { get; set; }

        public int? CountryID { get; set; }
        public ClsCountries? CountryInfo { get; set; }
        public int? ContactInformationID { get; set; }
        public ClsContactsInfo? ContactInfo { get; set; }

        public int? PaymentInformationID { get; set; }
        public ClsPaymentsInfo? PaymentInfo { get; set; }
        public string? AdditionalNotes { get; set; }

        public ClsPerson()
        {
            this.ID = null;
            this.FirstName = string.Empty;
            this.LastName = string.Empty;
            this.BirthDay = null;
            this.Age = null;
            this.CountryID = null;
            this.ContactInformationID = null;
            this.PaymentInformationID = null;
            this.AdditionalNotes = string.Empty;
            this._mode = Mode.Add;

        }

        private ClsPerson(int? iD, string firstName, string lastName, DateTime? birthDay, byte age, int countryID, int contactInformation, int? paymentInformation, string? additionalNotes)
        {
            this.ID = iD;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.BirthDay = birthDay;
            this.Age = age;
            this.CountryID = countryID;
            this.ContactInformationID = contactInformation;
            this.PaymentInformationID = paymentInformation;
            this.AdditionalNotes = additionalNotes;
            this._mode = Mode.Edit;

            // Call Classes
            this.CountryInfo = ClsCountries.FindByID(countryID);
            this.ContactInfo = ClsContactsInfo.FindbyID(contactInformation);
            this.PaymentInfo = ClsPaymentsInfo.FindByID(paymentInformation);
        }

        public static ClsPerson? FindByID(int ID)
        {
            string FirstName=string.Empty, LastName= string.Empty;
            string? AddtitionalNotes = null;
            DateTime? birthDay= null;
            int countryID = 0, contactInformation = 0;
            int? paymentInformation = null;
            byte age = 0;
            

            if (People.FindByID(ID,ref FirstName,ref LastName,ref birthDay,ref age,ref countryID,ref contactInformation,ref paymentInformation,ref AddtitionalNotes))
            {
                return new ClsPerson(ID,FirstName,LastName,birthDay,age,countryID,contactInformation,paymentInformation, AddtitionalNotes);
            }

            return null;
        }
        public static ClsPerson? FindByBirthDay(DateTime BirthDay)
        {
            string FirstName = string.Empty, LastName = string.Empty;
            string? AddtitionalNotes = null;
            int countryID = -1, contactInformation = -1, ID = -1;
            int? paymentInformation = null;
            byte age = 0;


            if (People.FindByBirthDay(ref ID, ref FirstName, ref LastName, BirthDay, ref age, ref countryID, ref contactInformation, ref paymentInformation, ref AddtitionalNotes))
            {
                return new ClsPerson(ID, FirstName, LastName, BirthDay, age, countryID, contactInformation, paymentInformation, AddtitionalNotes);
            }

            return null;
        }
        public static ClsPerson? FindByFullName(string FirstName, string LastName)
        {
            //string FirstName = string.Empty, LastName = string.Empty;
            string? AddtitionalNotes = null;
            int countryID = -1, contactInformation = -1, ID = -1;
            DateTime? birthDay = null;
            int? paymentInformation = null;
            byte age = 0;


            if (People.FindByFullName(ref ID,  FirstName,  LastName,ref birthDay, ref age, ref countryID, ref contactInformation, ref paymentInformation, ref AddtitionalNotes))
            {
                return new ClsPerson(ID, FirstName, LastName, birthDay, age, countryID, contactInformation, paymentInformation, AddtitionalNotes);
            }

            return null;
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
        private bool _AddNew()
        {
           this.ID = People.AddNewPerson(this.FirstName, this.LastName,this.BirthDay,this.CountryID,
               this.ContactInformationID,this.PaymentInformationID,this.AdditionalNotes);

            return (this.ID != null);
        }
        private bool _Update()
        {
            return People.UpdatePerson(this.ID, this.FirstName, this.LastName, this.BirthDay, this.CountryID, this.ContactInformationID,
                this.PaymentInformationID, this.AdditionalNotes);
        }

        public static bool Delete(int ID)
        {
            if(ID <= 0) return false;

            return People.DeletePerson(ID);
        }

        public static DataTable? GetOnAllPeople()
        {
            return People.GetEachPeople();
        }

    }
}
