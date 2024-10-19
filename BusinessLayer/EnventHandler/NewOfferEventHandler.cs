using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.EnventHandler
{
    public class NewOfferEventHandler:EventArgs
    {
        //private int _id;
        private string _TypeOfOfferService;
        private string _ServiceName;
        private string _ServiceProvider;
        private DateTime _publishDate;


        //public int ID { get}

        public string TypeOfOfferService
        {
            get { return _TypeOfOfferService; }
            private set { _TypeOfOfferService = value; }
        }
        public string ServiceName
        {
            get { return _ServiceName; }
            private set { _ServiceName = value; }
        }

        public string ServiceProvider
        { 
            get { return _ServiceProvider; } 
            private set { _ServiceProvider = value; }
        }

        public DateTime PublishDate
        {
            get { return _publishDate; }
           private  set { _publishDate = value; }
        }

        public NewOfferEventHandler(string typeOfOfferService, string ServiceName,string serviceProvider, DateTime publishDate)
        {
            //this._id = Convert.ToInt32(Guid.NewGuid());
            this.TypeOfOfferService = typeOfOfferService;
            this.ServiceName = ServiceName;
            this.ServiceProvider = serviceProvider;
            this.PublishDate = publishDate;
        }


    }
}
