using BusinessLayer.DetailsPeople;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IUser
    {
        int? ID { get; }

        int? PersonID { get; set; }
        ClsPerson PersonInfo { get; set; }

        string UserName { get; set; }
        string Password { get; set; }

        bool IsActive { get; set; }


        bool Save();


    }
}
