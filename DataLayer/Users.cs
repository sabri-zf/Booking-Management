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
    public class Users
    {

        public static bool FindByID(int? ID ,ref int PersonID ,ref string UserName ,ref string Password ,ref bool IsActive)
        {
            if(ID == null || ID <= 0 ) return false;

            bool CorrectData = false;
            try
            {

                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {
                    connection.Open();

                    string query = @"select * from Users where UserID = @ID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue(@"ID", ID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                CorrectData = true;

                                PersonID = (int)reader["PersonID"];
                                UserName = (string)reader["UserName"];
                                Password = (string)reader["password"];
                                IsActive = (bool)reader["IsActive"];

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

        public static bool FindByPersonID(ref int ID,  int? PersonID, ref string UserName, ref string Password, ref bool IsActive)
        {
            if (PersonID == null || PersonID <= 0) return false;

            bool CorrectData = false;
            try
            {

                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {
                    connection.Open();

                    string query = @"select * from Users where PersonID = @ID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue(@"ID", PersonID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                CorrectData = true;

                                ID = (int)reader["UserID"];
                                UserName = (string)reader["UserName"];
                                Password = (string)reader["password"];
                                IsActive = (bool)reader["IsActive"];

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

        public static bool FindByUserName(ref int ID, ref int PersonID,  string UserName, ref string Password, ref bool IsActive)
        {
            if (string.IsNullOrEmpty(UserName)) return false;

            bool CorrectData = false;
            try
            {

                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {
                    connection.Open();

                    string query = @"select * from Users where UserName = @UserName";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue(@"UserName", UserName);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                CorrectData = true;

                                ID = (int)reader["UserID"];
                                PersonID = (int)reader["PersonID"];
                                Password = (string)reader["password"];
                                IsActive = (bool)reader["IsActive"];

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

        public static bool FindByUserNameAndPassword(ref int ID, ref int PersonID, string UserName,  string Password, ref bool IsActive)
        {
            if (string.IsNullOrEmpty(UserName) && string.IsNullOrEmpty(Password)) return false;

            bool CorrectData = false;
            try
            {

                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {
                    connection.Open();

                    string query = @"select * from Users where UserName = @UserName and password = @Password";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue(@"UserName", UserName);
                        command.Parameters.AddWithValue(@"Password", Password);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                CorrectData = true;

                                ID = (int)reader["UserID"];
                                PersonID = (int)reader["PersonID"];
                                IsActive = (bool)reader["IsActive"];

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

        public static int? AddNewUser(int? PersonID, string UserName, string Password, bool IsActive)
        {
            if(PersonID <= 0 && string.IsNullOrEmpty (UserName) && string.IsNullOrEmpty (Password)) return null;

            int? UserID = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {
                    connection.Open();

                    string query = @"insert into Users Values (@PersonID,@UserName ,@Password,@IsActive)
                                     select Scope_identity();";

                    using(SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue(@"PersonID", PersonID);
                        command.Parameters.AddWithValue(@"UserName", UserName);
                        command.Parameters.AddWithValue(@"Password", Password);
                        command.Parameters.AddWithValue(@"IsActive", IsActive);

                        object value = command.ExecuteScalar();

                        if (value != null && int.TryParse(value.ToString(), out int ID))
                        {
                            UserID = ID;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            return UserID;
        }

        public static bool UpdateUser(int? ID,int? PersonID, string UserName, string Password, bool IsActive)
        {
            if (ID == null || ID <= 0) return false;

            byte RowEffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {
                    connection.Open();

                    string query = @"update Users
                                    set PersonID = @PersonID,
                                        UserName = @UserName,
                                        Password = @Password,
                                        IsActive = @IsActive
                                        where UserID = @ID;";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue(@"ID", ID);
                        command.Parameters.AddWithValue(@"PersonID", PersonID);
                        command.Parameters.AddWithValue(@"UserName", UserName);
                        command.Parameters.AddWithValue(@"Password", Password);
                        command.Parameters.AddWithValue(@"IsActive", IsActive);

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


        public static bool DeleteUserByID(int? ID)
        {
            if(ID == null || ID <= 0)  return false; 

            byte RowEffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {
                    connection.Open();

                    string query = @"Delete Users where UserID = @ID";

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

        public static bool DeleteUserByPersonID(int? PersonID)
        {
            if(PersonID == null || PersonID <= 0)  return false; 


            byte RowEffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {
                    connection.Open();

                    string query = @"Delete Users where UserID = @ID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue(@"ID", PersonID);


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

        public static bool DeleteUserByUserName(string UserName)
        {
            if(string.IsNullOrEmpty(UserName)) return false;

            byte RowEffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {
                    connection.Open();

                    string query = @"Delete Users where UserID = @ID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue(@"ID", UserName);


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



        public static async Task<DataTable> GetEachUsersAsync()
        {
            DataTable Table = new DataTable();


            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {
                    await connection.OpenAsync();

                    string query = @"select * from Users";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if(await reader.ReadAsync())
                            {
                                Table.Load(reader);
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Table;
            }
            return Table;
        }

        public static DataTable GetEachUsersSync()
        {
            DataTable Table = new DataTable();


            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {
                     connection.Open();

                    string query = @"select * from Users";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if ( reader.Read())
                            {
                                Table.Load(reader);
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Table;
            }
            return Table;
        }

    }
}
