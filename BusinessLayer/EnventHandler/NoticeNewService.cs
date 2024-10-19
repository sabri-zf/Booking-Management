using BusinessLayer.Service_Commponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.EnventHandler
{

    public delegate void ServiceIsProvide(object obj ,NewOfferEventHandler e);
    public class NoticeNewService
    {

        public event ServiceIsProvide OnServiceOpen;
        public void NoticeNew(bool IsSave, DateTime PublishDate,string TypoeOfService = "", string ServiceName = "", string ServiceProvider = "")
        {
            // If Save new Service in Data Base Do Raise To Event Notice Otherwise not Notice :)
            if (IsSave)
            {
                var NewOffer = new NewOfferEventHandler(TypoeOfService, ServiceName, ServiceProvider, PublishDate);

                NoticeNewService_OnServiceOpen(NewOffer);

            }

        }

        protected virtual void NoticeNewService_OnServiceOpen(NewOfferEventHandler e)
        {
            // Invoke objcet To Any Subscribe 
            OnServiceOpen?.Invoke(this,e);
        }
    }
}
