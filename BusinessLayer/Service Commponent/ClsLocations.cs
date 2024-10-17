using LogicLayer.PeopleModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LogicLayer.Service_Commponent
{
    public class ClsLocations
    {

        enum Mode { Add,Edit}

        Mode _mode = Mode.Add;
        public int? ID { get; private set; }
        public int?CountryID { get;set; }
        public ClsCountries? CountryInfo { get; set; }
        public ClsLocations() 
        {
            this.ID = null;
            this.CountryID = null;

            this._mode = Mode.Add;
        }
        private ClsLocations(int ID , int CountryID)
        {
            this.ID = ID;
            this.CountryID = CountryID;
            this.CountryInfo = ClsCountries.FindByID(CountryID);

            this._mode = Mode.Edit;
        }

        public static ClsLocations? FindByID(int ID)
        {
            return null;
        }


        private bool _AddNew()
        {
            return false;
        }

        private bool _Update()
        {
            return false;
        }

        public static bool Delete(int ID)
        {
            return false;
        }

        public bool Save()
        {
            return false;
        }


        

    }
}
