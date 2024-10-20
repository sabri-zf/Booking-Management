﻿using BusinessLayer.DetailsPeople;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IPerson
    {

        int? ID { get; }

        string FirstName { get; set; }
        string LastName { get; set; }

        DateTime BirthDay { get; set; }

        byte Age { get; set; }

        int? CountryID { get; set; }
        ClsCountries CountryInfo { get; set; }

        int? ContactInformationID { get; set; }
        ClsContactsInfo ContactInfo { get; set; }

        int? PaymentInformationID { get; set; }
        ClsPaymentsInfo PaymentInfo { get; set; }

        string AdditionalNotes { get; set; }


        bool Save();

    }

}
