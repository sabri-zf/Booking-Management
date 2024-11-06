using BusinessLayer.EnventHandler;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service_Commponent
{
    public class ClsServices
    {
        enum Mode { Add, Edit }

        private Mode _mode = Mode.Add;
        public int? ID { get; private set; }

        public int? CategoryID { get; set; }
        public Clscategories CategoryInfo { get; set; }

        public string ServiceName { get; set; }

        public string Description { get; set; }

        public int? LocationID { get; set; }
        public ClsLocations LocationInfo { get; set; }
        public DateTime DurationTime { get; set; }

        public string ServiceProvider { get; set; }

        public short Capacity { get; set; }

        public string AdditionalNotes { get; set; }

        public byte? Discount { get; set; }


        static DateTime _DefualtDateTime = new DateTime(2000, 01, 01);

        public ClsServices()
        {
            this.ID = null;
            this.CategoryID = null;
            this.ServiceName = string.Empty;
            this.Description = string.Empty;
            this.LocationID = null;
            this.DurationTime = _DefualtDateTime;
            this.ServiceProvider = string.Empty;
            this.AdditionalNotes = string.Empty;
            this.Capacity = 0;
            this.Discount = 0;

            this._mode = Mode.Add;
        }


        private ClsServices(int iD, int? categoryID, string serviceName, string description, int? locationID, DateTime durationTime, string serviceProvider, short capacity, string additionalNotes, byte? discount)
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
            this.CategoryInfo = Clscategories.FindByID(categoryID);
            this.LocationInfo = ClsLocations.FindByID(locationID);
        }

        /// <summary>
        /// Get The ID OF Service And Find him in Data Base 
        /// </summary>
        /// <param name="ID"> Number of Service Object In Data Base</param>
        /// <returns> Object if Connection and Get data is True ,otherwise return null </returns>
        public static ClsServices FindByID(int ID)
        {
            int? CategoryID = null, LocationID = null;
            byte? Discount = null;
            string ServiceName = string.Empty, Description = string.Empty,ServiceProvider =string.Empty,AdditionalNotes = string.Empty;
            DateTime DurationTime = _DefualtDateTime;
            short Capacity = 0;

            if(Services.FindByID(ID ,ref CategoryID,ref ServiceName,ref Description,ref LocationID,ref DurationTime,ref ServiceProvider,
                ref Capacity,ref AdditionalNotes,ref Discount))
            {
                return new ClsServices(ID, CategoryID, ServiceName, Description, LocationID, DurationTime, ServiceProvider, Capacity, AdditionalNotes, Discount);
            }

            return null;
        }

        [Obsolete("This Function Is Not Run Now",true)]
        public static ClsServices FindByServiceName(string serviceName)
        {
            return null;
        }


        private bool _AddNew()
        {
            this.ID = Services.AddNewService(this.CategoryID,this.ServiceName,this.Description,this.LocationID,this.DurationTime,
                this.ServiceProvider,this.Capacity,this.AdditionalNotes,this.Discount);
            return (this.ID.HasValue && this.ID > 0);
        }

        private bool _Update()
        {
            return Services.UpdateService(this.ID, this.CategoryID, this.ServiceName, this.Description, this.LocationID, this.DurationTime,
                this.ServiceProvider, this.Capacity, this.AdditionalNotes, this.Discount);
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

        public bool Delete(int ID)
        {
            return Services.Delete(ID);
        }

        /// <summary>
        /// Get The List Of Services from Data Base And Return
        /// </summary>
        /// <returns> All Recored if the Result True ,Otherwise Return Empty if False</returns>
        public static async Task<DataTable> GetEachService()
        {
            return await Services.GetListOfServicesAsync();
        }



        public void Subscribe(NoticeNewService publishNotice )
        {
            publishNotice.OnServiceOpen += GetNewNotification;
        }

        public void UnSubscribe(NoticeNewService publishNotice)
        {
            publishNotice.OnServiceOpen -= GetNewNotification;
        }

        private static void GetNewNotification(object obj ,NewOfferEventHandler e)
        {
            // write Any Code To Notification New service

            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("\n-- New Service N :");
            Console.WriteLine($"Type OF Service   : {e.TypeOfOfferService}");
            Console.WriteLine($"Service Name      : {e.ServiceName}");
            Console.WriteLine($"Service Provider  : {e.ServiceProvider}");
            Console.WriteLine($"Publish Date      : {e.PublishDate}");
            Console.WriteLine("\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
           
        }


        public override string ToString()
        {
            return $"\nCatigory Name     : {CategoryInfo.CategoryName} " +
                   $"\nService Name      : {ServiceName} " +
                   $"\nService Provder   : {ServiceProvider}" +
                   $"\nService Location  : {LocationInfo.CountryInfo.CountryName}" +
                   $"\nCapacity          : {Capacity}" +
                   $"\nDescription       : {Description}"+
                   $"\nDiscount          : {Discount}"+
                   $"\nAddtional Note    : {AdditionalNotes}";

        }
    }
}
