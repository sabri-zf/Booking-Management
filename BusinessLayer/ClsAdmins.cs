using BusinessLayer.DetailsPeople;
using BusinessLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;

namespace BusinessLayer
{
    public  class ClsAdmins : IAdmin
    {

        enum Mode { Add, Edit }

        Mode? _mode = null;
        public int? ID { get; private set; }

        public int? PersonID { get; set; }
        public ClsPerson PersonInfo { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }

        public int? DepartmentID { get; set; }
        public ClsDepartments DepartmentInfo { get; set; }

        public string profile_Pictrue { get; set; }

        public string Emargency_Contact { get; set; }

        public byte Permission { get; set; }

        public bool IsActive { get; set; }

        public ClsAdmins()
        {
            this.ID = null;
            this.PersonID = null;
            this.UserName = string.Empty;
            this.Password = string.Empty;
            this.DepartmentID = null;
            this.Emargency_Contact = string.Empty;
            this.profile_Pictrue = string.Empty;
            this.Permission = 0;
            this.IsActive = false;

            this._mode = Mode.Add;
        }

        private ClsAdmins(int ID, int PersonID, string UserName, string Password, int DepartmentID,
            string picture, string EmargencyContact, byte permission, bool IsActive)
        {
            this.ID = ID;
            this.PersonID = PersonID;
            this.UserName = UserName;
            this.Password = Password;
            this.DepartmentID = DepartmentID;
            this.profile_Pictrue = picture;
            this.Emargency_Contact = EmargencyContact;
            this.Permission = permission;
            this.IsActive = IsActive;

            this._mode = Mode.Edit;

            this.PersonInfo = ClsPerson.FindByID(PersonID);
            this.DepartmentInfo = ClsDepartments.FindByID(DepartmentID);

        }

        public static ClsAdmins FindByID(int ID)
        {
            int PersonID = -1,DepartmentID = -1;
            bool IsActive = false; 
            byte Permission = 0;
            string UserName  = string.Empty, Password = string.Empty,ProFile_Picture = string.Empty,Emargency_contact = string.Empty;


            if(Admins.FindByID(ID ,ref PersonID , ref UserName,ref Password,ref DepartmentID,ref ProFile_Picture,ref 
                Emargency_contact , ref Permission,ref IsActive))
            {

                return new ClsAdmins(ID,PersonID,UserName,Password,DepartmentID, ProFile_Picture,Emargency_contact,Permission,IsActive);
            }
            return null;
        }

        public static ClsAdmins FindByPersonID(int PersonID)
        {
            int ID = -1, DepartmentID = -1;
            bool IsActive = false;
            byte Permission = 0;
            string UserName = string.Empty, Password = string.Empty, ProFile_Picture = string.Empty, Emargency_contact = string.Empty;


            if (Admins.FindByPersonID(ref ID,  PersonID, ref UserName, ref Password, ref DepartmentID, ref ProFile_Picture, ref
                Emargency_contact, ref Permission, ref IsActive))
            {

                return new ClsAdmins(ID, PersonID, UserName, Password, DepartmentID, ProFile_Picture, Emargency_contact, Permission, IsActive);
            }
            return null;
        }

        public static ClsAdmins FindByUserName(string UserName)
        {
            int PersonID = -1, DepartmentID = -1,ID=-1;
            bool IsActive = false;
            byte Permission = 0;
             string Password = string.Empty, ProFile_Picture = string.Empty, Emargency_contact = string.Empty;


            if (Admins.FindByUserName(ref ID, ref PersonID,  UserName, ref Password, ref DepartmentID, ref ProFile_Picture, ref
                Emargency_contact, ref Permission, ref IsActive))
            {

                return new ClsAdmins(ID, PersonID, UserName, Password, DepartmentID, ProFile_Picture, Emargency_contact, Permission, IsActive);
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
            this.ID = Admins.AddNewAdmin(this.PersonID,this.UserName,this.Password,this.DepartmentID,this.profile_Pictrue,this.Emargency_Contact
                ,this.Permission,this.IsActive);

            return (this.ID != null || this.ID > 0);
        }

        private bool _Update()
        {
            return Admins.UpdateAdmin(this.ID,this.PersonID,this.UserName,this.Password,this.DepartmentID,this.profile_Pictrue,this.Emargency_Contact,this.Permission,this.IsActive);
        }
        public static bool Delete(int ID)
        {
            return Admins.DeleteAdminn(ID);
        }
        public static bool Delete(string Username)
        {
            return Admins.DeleteAdminn(Username);
        }

        public static DataTable GetAdmins()
        {
            return Admins.GetEachAdmins();
        }

    }
}
