using LogicLayer.Interface;
using LogicLayer.PeopleModule;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class ClsAdmins:IAdmin
    {

        enum Mode { Add , Edit}

        Mode? _mode = null;
        public int? ID {  get; private set; }

        public int? PersonID { get;  set; }
        public ClsPerson? PersonInfo { get; set; }

        public string? UserName { get; set; }
        public string? Password { get; set; }

        public int? DepartmentID { get; set; }
        public ClsDepartments? DepartmentInfo { get; set; }

        public string? profile_Pictrue{ get; set; }

        public string? Emargency_Contact {  get; set; }

        public byte Permission {  get; set; }

        public bool? IsActive { get; set; }

        public ClsAdmins()
        {
            this.ID = null;
            this.PersonID = null;
            this.UserName = null;
            this.Password = null;
            this.DepartmentID = null;
            this.Emargency_Contact = null;
            this.profile_Pictrue = null;
            this.Permission = 0;
            this.IsActive = null;

            this._mode = Mode.Add;
        }

        private ClsAdmins(int ID , int PersonID, string UserName, string Password,int DepartmentID,
            string? picture ,string? EmargencyContact,byte permission,bool IsActive)
        {
            this.ID = ID;
            this.PersonID = PersonID;
            this.UserName = UserName;
            this.Password = Password;
            this.DepartmentID = DepartmentID;
            this.profile_Pictrue= picture;
            this.Emargency_Contact= EmargencyContact;
            this.Permission = permission;
            this.IsActive = IsActive;

            this._mode = Mode.Edit;

            this.PersonInfo = ClsPerson.FindByID(PersonID);
            this.DepartmentInfo = ClsDepartments.FindByID(DepartmentID);

        }

        public static ClsAdmins? FindByID(int ID)
        {
            return null;
        }

        public static ClsAdmins? FindByUserName(string UserName)
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
            return true;
        }
        public static bool Delete(int ID)
        {
            return false;
        }
        public static bool Delete(string Username)
        {
            return false;
        }

        public static DataTable? GetAdmins()
        {
            return null ;
        }


    }
}
