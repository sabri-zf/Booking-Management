using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;

namespace LogicLayer.PeopleModule
{
    public class ClsCountries
    {

        public int? ID { get; private set; }
        public string? CountryName { get;set; }
        public string? ISO {  get; set; }


        ClsCountries(int? ID , string? CountryName , string ISO)
        {
            this.ID = ID;
            this.CountryName = CountryName;
            this.ISO = ISO;
        }

        public static ClsCountries? FindByID(int ID)
        {
            string? countryName = null,ISO = null;

            if(DataLayer.Countries.FindbyID(ID,ref countryName,ref ISO))
            {
                return new ClsCountries(ID,countryName,ISO);
            }
            return null;
        }
       public static ClsCountries? FindByISO(string iso)
        {

            string? countryName = null;
            int? ID = null;

            if (DataLayer.Countries.FindbyISO(ref ID, ref countryName,  iso))
            {
                return new ClsCountries(ID, countryName, iso);
            }
            return null;
        }
        public static ClsCountries? FindByCountryName(string CountryName)
        {
            return null;
        }
        public static DataTable? CountriesList()
        {
            return Countries.GetEachCountries();
        }

        public static string? GetCountryName(int ID)
        {
            return Countries.GetToCountryName(ID);
        }

        public static string? GetCountryName(string ISO)
        {
            return Countries.GetToCountryName(ISO);
        }

        public static int? GetCountryID(string CountryName)
        {
            return null;
        }

        public static string? GetISO(string CountryName)
        {
            return null;
        }
        public static string? GetISO(int ID)
        {
            return null;
        }

        public static DataTable? GetEachCountry()
        {
            return Countries.GetEachCountries() ;
        }

    }
}
