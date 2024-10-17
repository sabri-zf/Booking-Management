using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using End = System.Console;

namespace DataLayer
{
    public class Admins
    {


        public static bool FindByID(int ID, ref int PersonID, ref string UserName, ref string Password, ref int DepartmentID
             , ref string Profile_Picture, ref string Emeargency_Contact, ref byte Permission, ref bool IsActive)
        {
            if (ID <= 0) return false;

            bool PassAccessData = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {
                    connection.Open();

                    string query = @"select * from Admins where AdminID = @ID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue(@"ID", ID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            if (reader.Read())
                            {

                                PersonID = (int)reader["PersonID"];
                                UserName = (string)reader["UserName"];
                                Password = (string)reader["Password"];
                                DepartmentID = (int)reader["DepartmentID"];
                                Profile_Picture = (reader["Profile_Picture"] == DBNull.Value) ? string.Empty : (string)reader["Profile_Picture"];
                                Emeargency_Contact = (reader["Emergency_Contact"] == DBNull.Value) ? string.Empty : (string)reader["Emergency_Contact"];
                                Permission = (reader["Permissoin"] == DBNull.Value) ? (byte)0 : (byte)reader["Permissoin"];
                                IsActive = (bool)reader["IsActive"];

                                PassAccessData = true;

                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                End.WriteLine(ex.Message);
                PassAccessData = false;
            }



            return PassAccessData;

        }


        public static bool FindByPersonID(ref int ID, int PersonID, ref string UserName, ref string Password, ref int DepartmentID
           , ref string Profile_Picture, ref string Emeargency_Contact, ref byte Permission, ref bool IsActive)
        {
            if (PersonID <= 0) return false;

            bool PassAccessData = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {
                    connection.Open();

                    string query = @"select * from Admins where PersonID = @ID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue(@"ID", PersonID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            if (reader.Read())
                            {

                                ID = (int)reader["AdminID"];
                                UserName = (string)reader["UserName"];
                                Password = (string)reader["Password"];
                                DepartmentID = (int)reader["DepartmentID"];
                                Profile_Picture = (reader["Profile_Picture"] == DBNull.Value) ? string.Empty : (string)reader["Profile_Picture"];
                                Emeargency_Contact = (reader["Emergency_Contact"] == DBNull.Value) ? string.Empty : (string)reader["Emergency_Contact"];
                                Permission = (reader["Permissoin"] == DBNull.Value) ? (byte)0 : (byte)reader["Permissoin"];
                                IsActive = (bool)reader["IsActive"];

                                PassAccessData = true;

                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                End.WriteLine(ex.Message);
                PassAccessData = false;
            }



            return PassAccessData;

        }

        public static bool FindByUserName(ref int ID, ref int PersonID, string UserName, ref string Password, ref int DepartmentID
          , ref string Profile_Picture, ref string Emeargency_Contact, ref byte Permission, ref bool IsActive)
        {
            if (string.IsNullOrEmpty(UserName)) return false;

            bool PassAccessData = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {
                    connection.Open();

                    string query = @"select * from Admins where UserName = @UserName";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue(@"UserName", UserName);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            if (reader.Read())
                            {

                                ID = (int)reader["AdminID"];
                                PersonID = (System.Int32)reader["PersonID"];
                                Password = (string)reader["Password"];
                                DepartmentID = (int)reader["DepartmentID"];
                                Profile_Picture = (reader["Profile_Picture"] == DBNull.Value) ? string.Empty : (string)reader["Profile_Picture"];
                                Emeargency_Contact = (reader["Emergency_Contact"] == DBNull.Value) ? string.Empty : (string)reader["Emergency_Contact"];
                                Permission = (reader["Permissoin"] == DBNull.Value) ? (byte)0 : (byte)reader["Permissoin"];
                                IsActive = (bool)reader["IsActive"];

                                PassAccessData = true;

                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                End.WriteLine(ex.Message);
                PassAccessData = false;
            }



            return PassAccessData;

        }

        public static int? AddNewAdmin(int? PersonID, string UserName, string Password, int? DepartmentID, string Profile_Picture, string Emergency_Contact
            , byte Permission, bool IsActive)
        {
            if (PersonID == null || DepartmentID == null || PersonID <= 0 || DepartmentID <= 0) return null;

            int? AdminID = null;
            try
            {

                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {
                    connection.Open();


                    string query = @"insert into Admins values 
                                    (@PersonID,@UserName,@Password,@DepartmentID,@Profile_Picture,@Emergency_Contact,@Permissoin , @IsActive)
                                    select Scope_Identity();";


                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue(@"PersonID", PersonID);
                        command.Parameters.AddWithValue(@"UserName", UserName);
                        command.Parameters.AddWithValue(@"Password", Password);
                        command.Parameters.AddWithValue(@"DepartmentID", DepartmentID);

                        if (string.IsNullOrEmpty(Profile_Picture))
                        {

                        command.Parameters.AddWithValue(@"Profile_Picture", DBNull.Value);
                        }
                        else
                        {

                        command.Parameters.AddWithValue(@"Profile_Picture", Profile_Picture);
                        }

                        command.Parameters.AddWithValue(@"Emergency_Contact", Emergency_Contact);

                        if(Permission == 0)
                        {
                            command.Parameters.AddWithValue(@"Permissoin", DBNull.Value);

                        }
                        else
                        {

                           command.Parameters.AddWithValue(@"Permissoin", Permission);
                        }

                           command.Parameters.AddWithValue(@"IsActive", IsActive);


                        object value = command.ExecuteScalar();

                        if (value != null && int.TryParse(value.ToString(), out int ID))
                        {
                            AdminID = ID;
                        }
                    }


                }

            }
            catch (Exception ex)
            {
                End.WriteLine(ex.Message);

                return null;
            }


            return AdminID;
        }

        public static bool UpdateAdmin(int? ID, int? PersonID, string UserName, string Password, int? DepartmentID, string Profile_Picture, string Emergency_Contact
          , byte Permission, bool IsActive)
        {
            if (ID <= 0 || ID == null) return false;

            byte RowEffected = 0;
            try
            {

                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {
                    connection.Open();


                    string query = @"Update Admins 
                                    set PersonID = @PersonID,
                                        UserName = @UserName,
                                        Password = @Password,
                                        DepartmentID = @DepartmentID,
                                        Profile_Picture = @Profile_Picture,
                                        Emergency_Contact = @Emergency_Contact,
                                        Permissoin = @Permissoin,
                                        IsActive = @IsActive
                                        where AdminID = @ID;";


                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue(@"ID", ID);
                        command.Parameters.AddWithValue(@"PersonID", PersonID);
                        command.Parameters.AddWithValue(@"UserName", UserName);
                        command.Parameters.AddWithValue(@"Password", Password);
                        command.Parameters.AddWithValue(@"DepartmentID", DepartmentID);

                        if (string.IsNullOrEmpty(Profile_Picture))
                        {
                            command.Parameters.AddWithValue(@"Profile_Picture", DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue(@"Profile_Picture", Profile_Picture);
                        }

                        command.Parameters.AddWithValue(@"Emergency_Contact", Emergency_Contact);
                        if (Permission == 0)
                        {
                            command.Parameters.AddWithValue(@"Permissoin", DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue(@"Permissoin", Permission);
                        }
                        command.Parameters.AddWithValue(@"IsActive", IsActive);



                        RowEffected = (byte)command.ExecuteNonQuery();


                    }


                }

            }
            catch (Exception ex)
            {
                End.WriteLine(ex.Message);

                return false;
            }


            return (RowEffected > 0);
        }

        public static bool DeleteAdminn(int? AdminID)
        {

            if (AdminID <= 0 || AdminID == null) return false;

            byte rowEffected = 0;
            try
            {

                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {
                    connection.Open();

                    string query = @"Delete Admins where AdminID = @ID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue(@"ID", AdminID);


                        rowEffected = (byte)command.ExecuteNonQuery();

                    }

                }

            }
            catch (Exception ex)
            {
                End.WriteLine(ex.Message);
                return false;

            }

            return (rowEffected > 0);
        }

        public static bool DeleteAdminn(string UserName)
        {

            if (string.IsNullOrEmpty(UserName)) return false;

            byte rowEffected = 0;
            try
            {

                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {
                    connection.Open();

                    string query = @"Delete Admins where UserName = @UserName";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue(@"UserName", UserName);


                        rowEffected = (byte)command.ExecuteNonQuery();

                    }

                }

            }
            catch (Exception ex)
            {
                End.WriteLine(ex.Message);
                return false;

            }

            return (rowEffected > 0);
        }

        public static  DataTable GetEachAdmins()
        {

            DataTable table = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {
                    connection.Open();

                    string query = @"select * form Admins";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                table.Load(reader);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                End.WriteLine(ex.Message);
                return table;
            }
            return table;
        }

    }

}
