using BusinessLayer.DetailsPeople;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service_Commponent
{
    public class ClsLocations
    {

        enum Mode { Add, Edit }

        Mode _mode = Mode.Add;
        public int? ID { get; private set; }
        public int? CountryID { get; set; }
        public ClsCountries CountryInfo { get; set; }
        public ClsLocations()
        {
            this.ID = null;
            this.CountryID = null;

            this._mode = Mode.Add;
        }
        private ClsLocations(int? ID, int CountryID)
        {
            this.ID = ID;
            this.CountryID = CountryID;
            this.CountryInfo = ClsCountries.FindByID(CountryID);

            this._mode = Mode.Edit;
        }

        public static ClsLocations FindByID(int? ID)
        {
            int countryID = -1;

            if(ServiceLocations.FindByID(ID,ref countryID))
            {
                return new ClsLocations(ID ,countryID);
            }
            return null;
        }


        private bool _AddNew()
        {
            this.ID = ServiceLocations.AddNewLocation(this.CountryID);
            return (this.ID != null && this.ID > 0);
        }

        private bool _Update()
        {
            return ServiceLocations.UpdateLocation(this.ID,this.CountryID);
        }

        public static bool Delete(int ID)
        {
            return ServiceLocations.Delete(ID);
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


        public static string GetLocationName(int ID ,int CountryID)
        {
            return ServiceLocations.GetLocationName(ID,CountryID);
        }
        public  string GetLocationName()
        {
            return ServiceLocations.GetLocationName(this.ID, this.CountryID);
        }


    }
}
