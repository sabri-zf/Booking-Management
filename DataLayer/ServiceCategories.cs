using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class ServiceCategories
    {

        public static bool FindByID(int? ID, ref string CategoryName)
        {

            if (ID == null || ID <= 0) return false;

            bool CorrectData = false;

            try
            {

                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {
                    connection.Open();

                    string query = @"select * from Categories where CategoryID =@ID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue(@"ID", ID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                CorrectData = true;

                                CategoryName = (string)reader["Name"];
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

            return CorrectData;

        }

        public static bool FindByCategoryName(ref int ID, string CategoryName)
        {

            if (string.IsNullOrEmpty(CategoryName)) return false;

            bool CorrectData = false;

            try
            {

                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {
                    connection.Open();

                    string query = @"select * from Categories where Name =@Name";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue(@"Name", CategoryName);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                CorrectData = true;

                                ID = (int)reader["CategoryID"];

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

            return CorrectData;

        }


        public static int? AddNewCategory(string CategoryName)
        {
            if (string.IsNullOrEmpty(CategoryName)) return null;

            int? CategoryID = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {
                    connection.Open();

                    string query = @"insert into Categories values(@Name)
                                    select Scope_identity();";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue(@"Name", CategoryName);

                        object value = command.ExecuteScalar();

                        if (value != null && int.TryParse(value.ToString(), out int ID))
                        {
                            CategoryID = ID;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

            return CategoryID;
        }

        public static bool UpdateCategory(int? ID, string CategoryName)
        {
            if (string.IsNullOrEmpty(CategoryName) && (ID == null || ID <= 0)) return false;

            byte RowEffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {
                    connection.Open();

                    string query = @"update Categories
                                    set Name = @Name
                                        where CategoryID = @ID;";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue(@"Name", CategoryName);
                        command.Parameters.AddWithValue(@"ID", ID);

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

        public static bool DeleteCategory(int? ID)
        {
            if (ID == null || ID <= 0) return false;

            byte RowEffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {
                    connection.Open();

                    string query = @"delete Categories where CategoryID =@ID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue(@"ID", ID);

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

        public static bool DeleteCategory(string CategoryName)
        {
            if (string.IsNullOrEmpty(CategoryName)) return false;

            byte RowEffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {
                    connection.Open();

                    string query = @"delete Categories where Name =@Name";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue(@"Name", CategoryName);

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

        public static DataTable GetListOfCategoriesSync()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {
                    connection.Open();

                    string querey = @"select * from Categories";

                    using (SqlCommand command = new SqlCommand(querey, connection))
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
            catch
            (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return dt;
            }

            return dt;
        }

    }
}
