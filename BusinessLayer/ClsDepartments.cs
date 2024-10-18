using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;

namespace BusinessLayer
{
    public class ClsDepartments
    {


        enum Mode { Add, Edit }
        Mode _mode = Mode.Add;


        public int? ID { get; private set; }
        public string DepatmentName { get; set; }

        public ClsDepartments()
        {
            this.ID = null;
            this.DepatmentName = string.Empty;

            this._mode = Mode.Add;
        }

        private ClsDepartments(int id, string depatmentName)
        {
            this._mode = Mode.Edit;

            this.ID = id;
            this.DepatmentName = depatmentName;
        }

        public static ClsDepartments FindByID(int id)
        {
            string DepartmentName = string.Empty;

            if(Departments.FindByID(id ,ref DepartmentName))
            {
                return new ClsDepartments(id, DepartmentName);
            }

            return null;
        }

        public static ClsDepartments FindByName(string name)
        {
            int ID = -1;

            if (Departments.FindDepartmentName(ref ID,  name))
            {
                return new ClsDepartments(ID, name);
            }

            return null;
        }

        public static bool Delete(int ID)
        {
            return Departments.DeleteDepartment(ID);
        }

        public static bool Delete(string DepartmentName)
        {
            return Departments.DeleteDepartment(DepartmentName);
        }

        private bool _AddNew()
        {
            this.ID = Departments.AddNewDepartment(this.DepatmentName);

            return (this.ID != null || this.ID > 0);// retunr true;
        }
        private bool _Update()
        {
            return Departments.UpdateDepartment(this.ID,this.DepatmentName);
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

        public static DataTable GetEachDepartment()
        {
            return Departments.GetEachDepartments();
        }
    }
}
