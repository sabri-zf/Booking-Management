using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Service_Commponent
{
    public class ClsCategories
    {

        enum Mode { Add, Edit}

        Mode _mode = Mode.Add;
        public int? ID { get;private set; }
        public string? CategoryName { get; set; }

#if false
        public ClsCategories()
        {
            this.ID = null;
            this.CategoryName = null;
            this._mode = Mode.Add;
        }
#endif

        private ClsCategories(int ID , string categoryName)
        {
            this.ID = ID;
            this.CategoryName = categoryName;

            this._mode = Mode.Edit;
        }


        public static ClsCategories? FindByID(int ID)
        {
            return null;
        }
        public static ClsCategories? FindByName(int Name)
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

        private bool Delete(int ID)
        {
            return false;
        }

        private bool Save()
        {
            return true;
        }

        public static DataTable? GetAllCategories()
        {
            return null;
        }

    }
}
