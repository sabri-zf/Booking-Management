using DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DetailsPeople
{
    public class ClsContactsInfo
    {
        enum Mode { Add, Edit }

        Mode _mode;
        public int? ID { get; private set; }

        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public ClsContactsInfo()
        {
            this.ID = null;
            this.Phone = string.Empty;
            this.Email = string.Empty;
            this.Address = string.Empty;
            this._mode = Mode.Add;
        }

        private ClsContactsInfo(int? iD, string phone, string email, string address)
        {
            this.ID = iD;
            this.Phone = phone;
            this.Email = email;
            this.Address = address;
            this._mode = Mode.Edit;
        }


        public static ClsContactsInfo FindbyID(int ID)
        {
            string Phone = string.Empty, Email = string.Empty, Address = string.Empty;


            if (ContactsInformation.FindByID(ID, ref Phone, ref Email, ref Address))
            {
                return new ClsContactsInfo(ID, Phone, Email, Address);
            }

            return null;
        }

        public static ClsContactsInfo FindByPhone(string phone)
        {
            string Email = string.Empty, Address = string.Empty;
            int? contactID = null;


            if (ContactsInformation.FindByPhone(ref contactID, phone, ref Email, ref Address))
            {
                return new ClsContactsInfo(contactID, phone, Email, Address);
            }
            return null;
        }

        public static ClsContactsInfo FindByEmail(string email)
        {
            string Phone = string.Empty, Address = string.Empty;
            int? contactID = null;


            if (ContactsInformation.FindByEmail(ref contactID, ref Phone, email, ref Address))
            {
                return new ClsContactsInfo(contactID, Phone, email, Address);
            }
            return null;

        }


        public static bool DeleteContact(int ID)
        {
            return ContactsInformation.DeleteContact(ID);
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
            this.ID = ContactsInformation.AddNewContact(Phone, Email, Address);

            return (this.ID != null);
        }

        private bool _Update()
        {
            return ContactsInformation.UpdateContact(this.ID, this.Phone, this.Email, this.Address);
        }


        public static DataTable GetContacts()
        {
            return ContactsInformation.GetEachContacts();
        }
    }

}
