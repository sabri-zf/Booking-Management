using LogicLayer.Service_Commponent;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class ClsService
    {


        enum Mode { Add ,Edit}

        private Mode _mode=Mode.Add;
        public int? ID { get; private set; }

        public int? CategoryID { get;set; }
        public ClsCategories? CategoryInfo { get; set; }

        public string ServiceName { get; set; }

        public string? Description { get; set; }

        public int? LocationID { get; set; }
        public ClsLocations? LocationInfo { get; set; }
        public DateTime? DurationTime { get; set; }

        public string ServiceProvider { get; set; }

        public short? Capacity {  get; set; }
        
        public string? AdditionalNotes { get; set; }

        public byte? Discount { get; set; }




        public ClsService()
        {
            this.ID = null;
            this.CategoryID = null;
            this.ServiceName = string.Empty;
            this.Description = string.Empty;
            this.LocationID = null;
            this.DurationTime = null;
            this.ServiceProvider = string.Empty;
            this.AdditionalNotes = string.Empty;
            this.Capacity = null;
            this.Discount = null;

            this._mode = Mode.Add;
        }


        private ClsService(int iD, int categoryID, string serviceName, string? description, int locationID, DateTime? durationTime, string serviceProvider, short? capacity, string? additionalNotes, byte? discount)
        {
            this._mode = Mode.Edit;

            this.ID = iD;
            this.CategoryID = categoryID;
            this.ServiceName = serviceName;
            this.Description = description;
            this.LocationID = locationID;
            this.DurationTime = durationTime;
            this.ServiceProvider = serviceProvider;
            this.Capacity = capacity;
            this.AdditionalNotes = additionalNotes;
            this.Discount = discount;
            //
            this.CategoryInfo = ClsCategories.FindByID(categoryID);
            this.LocationInfo = ClsLocations.FindByID(locationID);
        }


        public static ClsService? FindByID(int ID)
        {
            return null;
        }

        public static ClsService? FindByServiceName(string serviceName) 
        {
            return null;
        }


        private bool _AddNew()
        {
            return true;
        }

        private bool _Update()
        {
            return true;
        }

        public bool Save()
        {
            return true;
        }

        public bool Delete()
        {
            return true;
        }


        public DataTable? GetEachService()
        {
            return null;
        }
    }
}
