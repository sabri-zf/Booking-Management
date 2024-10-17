using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Departments
    {

        public static bool FindByID(int? ID , ref string DepartmentName)
        {
            if(ID <= 0 || ID == null) return false;

            bool DataIsExist=false;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {
                    connection.Open();

                    string query = @"select * from Departments Where DepartmentID = @ID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue(@"ID", ID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                DataIsExist = true;

                                DepartmentName = (string)reader["Name"];
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        public static bool FindDepartmentName(ref int ID, string DepartmentName)
        {
            if (string.IsNullOrEmpty(DepartmentName)) return false;

            bool DataIsExist = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {
                    connection.Open();

                    string query = @"select * from Departments Where Name = @DepartmentName";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue(@"DepartmentName", DepartmentName);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                DataIsExist = true;

                                ID = (int)reader["DepartmentID"];
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        public static int? AddNewDepartment(string DepartmentName)
        {
            if (string.IsNullOrEmpty(DepartmentName)) return null;


            int? DepartmentID = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {
                    connection.Open();

                    string query = @"insert into Departments values(@Name)
                                   select Scope_Identity();";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", DepartmentName);

                        object Result = command.ExecuteScalar();

                        if (Result != null && int.TryParse(Result.ToString(), out int ID))
                        {
                            DepartmentID = ID;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

            return DepartmentID;
        }

        public static bool UpdateDepartment(int? ID , string DepartmentName)
        {
            if(ID<=0 || ID == null) return false;


            byte Roweffected = 0;
            try
            {

                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                { 
                    connection.Open();

                    string query = @"Update Departments 
                                     set Name = @DepartmentName
                                     where DepartmentID = @ID;";

                    using(SqlCommand command = new SqlCommand(query,connection))
                    {
                        command.Parameters.AddWithValue(@"ID", ID);

                        Roweffected = (byte)command.ExecuteNonQuery();
                    }
                }

            }
            catch
            (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

            return (Roweffected > 0);
        }

        public static bool DeleteDepartment(int? ID)
        {
            if(ID <= 0 || ID == null) return false;


            byte RowEffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                { 
                    connection.Open();

                    string query = @"Delete Departments where DepartmentID = @ID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue(@"ID", ID);

                        RowEffected = (byte)command.ExecuteNonQuery();
                    }
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

            return (RowEffected > 0);
        }

        public static bool DeleteDepartment(string DepartmentName)
        {
            if (string.IsNullOrEmpty(DepartmentName)) return false;


            byte RowEffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {
                    connection.Open();

                    string query = @"Delete Departments where Name = @DepartmentName";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue(@"DepartmentName", DepartmentName);

                        RowEffected = (byte)command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

            return (RowEffected > 0);
        }

        public static DataTable GetEachDepartments()
        {
            DataTable dt = new DataTable();

            try
            {

                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {
                    connection.Open();

                    string query = @"select * from Departments";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                dt.Load(reader);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return dt;
        }

    }
}
