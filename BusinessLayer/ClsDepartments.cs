using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class ClsDepartments
    {

        enum Mode { Add,Edit}
        Mode _mode = Mode.Add;


        public int? ID { get; private set; }
        public string? DepatmentName { get;set; }

        public ClsDepartments()
        {
            this.ID = null;
            this.DepatmentName = null;

            this._mode = Mode.Add;
        }

        private ClsDepartments(int id, string depatmentName)
        {
            this._mode = Mode.Edit;

            this.ID = id;
            this.DepatmentName = depatmentName;
        }

        public static ClsDepartments? FindByID(int id)
        {
            return null;
        }

        public static ClsDepartments? FindByName(string name)
        {
            return null;
        }

        public static bool Delete(int ID)
        {
            return false;
        }

        private bool _AddNew()
        {
            return false;
        }
        private bool _Update()
        {
            return false;
        }

        public bool Save()
        {
            return false;
        }

        public static DataTable? GetEachDepartment()
        {
            return null;
        }
    }
}
