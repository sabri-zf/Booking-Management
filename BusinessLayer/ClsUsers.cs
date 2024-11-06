using BusinessLayer.DetailsPeople;
using BusinessLayer.Interface;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class ClsUsers:IUser
    {

        enum Mode { Add, Edit }

        Mode _mode;
        public int? ID { get; private set; }

        public int? PersonID { get; set; }
        public ClsPerson PersonInfo { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public bool IsActive { get; set; }


        public ClsUsers()
        {
            this.ID = null;
            this.PersonID = null;
            this.UserName = string.Empty;
            this.Password = string.Empty;
            this.IsActive = false;
            this._mode = Mode.Add;
        }

        private ClsUsers(int iD, int personID, string userName, string password, bool isActive)
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


        public static bool DeleteByID(int ID)
        {
            return Users.DeleteUserByID(ID);
        }

        public static bool DeleteByPersonID(int PersonID)
        {
            return Users.DeleteUserByPersonID(PersonID);
        }

        public static bool DeleteByUserName(string UserName)
        {
            return Users.DeleteUserByUserName(UserName);
        }

        public static ClsUsers FindByUserName(string UserName)
        {
            int ID = -1 , PersonID = -1 ;
            string Password = string.Empty;
            bool isActive = false;  

            if(Users.FindByUserName(ref ID,ref PersonID,UserName,ref Password,ref isActive))
            {
                return new ClsUsers(ID,PersonID,UserName,Password,isActive);
            }

            return null;
        }
        public static ClsUsers FindByUserNameAndPassword(string UserName, string Password)
        {
            int ID = -1, PersonID = -1;
            //string Password = string.Empty;
            bool isActive = false;

            if (Users.FindByUserNameAndPassword(ref ID, ref PersonID, UserName, ClsUtility.ComputeHash(Password), ref isActive))
            {
                return new ClsUsers(ID, PersonID, UserName, Password, isActive);
            }

            return null;
        }

        public static ClsUsers FindByID(int ID)
        {
            int PersonID = -1;
            string Password = string.Empty,UserName = string.Empty;
            bool isActive = false;

            if (Users.FindByID( ID, ref PersonID, ref UserName, ref Password, ref isActive))
            {
                return new ClsUsers(ID, PersonID, UserName, Password, isActive);
            }

            return null;
        }

        public static ClsUsers FindByPersonID(int PersonID)
        {
            int ID = -1;
            string Password = string.Empty, UserName = string.Empty;
            bool isActive = false;

            if (Users.FindByPersonID(ref ID,  PersonID, ref UserName, ref Password, ref isActive))
            {
                return new ClsUsers(ID, PersonID, UserName, Password, isActive);
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
            this.ID = Users.AddNewUser(this.PersonID, this.UserName,ClsUtility.ComputeHash(this.Password),this.IsActive);
            return (this.ID != null && this.ID > 0);
        }
        private bool _Update()
        {
            return Users.UpdateUser(this.ID,this.PersonID,this.UserName, ClsUtility.ComputeHash(this.Password), this.IsActive);
        }

        public static async Task<DataTable> GetCollectionUserAsync()
        {
            return await Users.GetEachUsersAsync();
        }

        public static DataTable GetCollectionUserSync()
        {
            return  Users.GetEachUsersSync();
        }


        public static bool IsExistUser(string UserName,string Password)
        {
            return Users.IsExistUser(UserName, ClsUtility.ComputeHash(Password));
        }


        public override string ToString()
        {
            return $"\n First Name : {PersonInfo.FirstName}" +
                   $"\n Last Name  : { PersonInfo.LastName} " +
                   $"\n Age        : {PersonInfo.Age} " +
                   $"\n Country    : {PersonInfo.CountryInfo.CountryName}" +
                   $"\n Birth Day  : {PersonInfo.BirthDay.ToString()}"+
                   $"\n User Name  : {UserName} " +
                   $"\n Password   : {ClsUtility.HidePassword(Password)}" +
                   $"\n Active     : {IsActive}";
        }
    }
}
