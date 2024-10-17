using LogicLayer.Interface;
using LogicLayer.PeopleModule;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class ClsUsers:IUser
    {
        enum Mode { Add, Edit}

        Mode? _mode = null;
        public int? ID { get; private set; }

        public int? PersonID { get;set; }
        public ClsPerson? PersonInfo { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public bool? IsActive { get; set; }


        public ClsUsers()
        {
            this.ID = null;
            this.PersonID = null;
            this.UserName = string.Empty;
            this.Password = string.Empty;
            this.IsActive = false;
            this._mode = Mode.Add;
        }

        private ClsUsers(int iD, int personID,string userName, string password, bool isActive)
        {
            this._mode = Mode.Edit;
            this.ID = iD;
            this.PersonID = personID;
            this.UserName = userName;
            this.Password = password;
            this.IsActive = isActive;

            //call Class
            this.PersonInfo = ClsPerson.FindByID(personID);
        }


       static bool Delete(int ID)
        {
            return false;
        }

        public static ClsUsers? FindByUserName(string UserName)
        {
            return null;
        }
        public static ClsUsers? FindByUserNameAndPassword(string UserName, string Password)
        {
            return null;
        }

        public static ClsUsers? FindByID(int ID)
        {
            return null;
        }

        public static ClsUsers? FindByPersonID(int PersonID)
        {
            return null;
        }

        public bool Save()
        {
            return false;
        }

        private bool _AddNew()
        {
            return true;
        }
        private bool _Update()
        {
            return false;
        }

        public static DataTable? GetCollectionUser()
        {
            return null;
        }

    }
}
