using DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service_Commponent
{
    public class Clscategories
    {


        enum Mode { Add, Edit }

        Mode _mode = Mode.Add;
        public int? ID { get; private set; }
        public string CategoryName { get; set; }

#if false
        public ClsCategories()
        {
            this.ID = null;
            this.CategoryName = null;
            this._mode = Mode.Add;
        }
#endif

        private Clscategories(int? ID, string categoryName)
        {
            this.ID = ID;
            this.CategoryName = categoryName;

            this._mode = Mode.Edit;
        }


        public static Clscategories FindByID(int? ID)
        {
            string Name = string.Empty;

            if(ServiceCategories.FindByID(ID , ref Name))
            {
                return new Clscategories(ID, Name);
            }
            return null;
        }
        public static Clscategories FindByName(string Name)
        {
            int ID = -1;
            if (ServiceCategories.FindByCategoryName(ref ID,  Name))
            {
                return new Clscategories(ID, Name);
            }
            return null;
        }

        private bool _AddNew()
        {
            this.ID = ServiceCategories.AddNewCategory(this.CategoryName);


            return (this.ID != null && this.ID > 0);
        }

        private bool _Update()
        {
            return ServiceCategories.UpdateCategory(this.ID,this.CategoryName);
        }

        private bool Delete(int ID)
        {
            return ServiceCategories.DeleteCategory(ID);
        }
        private bool Delete(string Name)
        {
            return ServiceCategories.DeleteCategory(Name);
        }

        private bool Save()
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
            return true;
        }

        public static DataTable GetAllCategories()
        {
            return ServiceCategories.GetListOfCategoriesSync();
        }
    }
}
