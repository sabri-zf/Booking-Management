using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IAdmin
    {
        int? ID { get; }

        int? PersonID { get; set; }

        string UserName { get; set; }
        string Password { get; set; }

        int? DepartmentID { get; set; }
        // ClsDepartments DepartmentInfo {get;set;}

        string profile_Pictrue { get; set; }

        string Emargency_Contact { get; set; }

        byte Permission { get; set; }

        bool IsActive { get; set; }

        bool Save();

    }
}
