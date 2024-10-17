using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DataLayer
{
    public class ContactsInformation
    {

        public static bool FindByID(int? ID, ref string Phone, ref string Email, ref string Address)
        {
            if (ID <= 0 || ID == null) return false;

            bool FillGaps = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {

                    string querey = @"select * from Contacts where ContactID = @ID";

                    using (SqlCommand Command = new SqlCommand(querey, connection))
                    {

                        connection.Open();

                        Command.Parameters.AddWithValue(@"ID",ID);

                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {

                            if (Reader.HasRows)
                            {
                                FillGaps = true;

                                Phone = (string)Reader["Phone"];
                                Email = (string)Reader["Email"];
                                Address = (string)Reader["Address"];

                            }
                        }


                    }
                }

            }
            catch (Exception ex)
            {
                return false;
            }
        

            return FillGaps;
    
        }

        public static bool FindByPhone(ref int? ID, string Phone, ref string Email, ref string Address)
        {
            if (string.IsNullOrEmpty(Phone)) return false;

            bool FillGaps = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {

                    string querey = @"select * from Contacts where Phone = @Phone";

                    using (SqlCommand Command = new SqlCommand(querey, connection))
                    {

                        connection.Open();

                        Command.Parameters.AddWithValue(@"Phone", Phone);

                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {

                            if (Reader.HasRows)
                            {
                                FillGaps = true;

                                ID = (int)Reader["ContactID"];
                                Email = (string)Reader["Email"];
                                Address = (string)Reader["Address"];

                            }
                        }


                    }
                }

            }
            catch (Exception ex)
            {
                return false;
            }


            return FillGaps;

        }

        public static bool FindByEmail(ref int? ID, ref string Phone,  string Email, ref string Address)
        {
            if (string.IsNullOrEmpty(Email)) return false;

            bool FillGaps = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {

                    string querey = @"select * from Contacts where Email = @Email";

                    using (SqlCommand Command = new SqlCommand(querey, connection))
                    {

                        connection.Open();

                        Command.Parameters.AddWithValue(@"Email", Email);

                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {

                            if (Reader.HasRows)
                            {
                                FillGaps = true;

                                ID = (int)Reader["ContactID"];
                                Phone = (string)Reader["Phone"];
                                Address = (string)Reader["Address"];

                            }
                        }


                    }
                }

            }
            catch (Exception ex)
            {
                return false;
            }


            return FillGaps;

        }

        public static bool FindByAddress(ref int? ID, ref string Phone,ref string Email,  string Address)
        {
            if (string.IsNullOrEmpty(Address)) return false;

            bool FillGaps = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {

                    string querey = @"select * from Contacts where Address = @Address";

                    using (SqlCommand Command = new SqlCommand(querey, connection))
                    {

                        connection.Open();

                        Command.Parameters.AddWithValue(@"Address", Address);

                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {

                            if (Reader.HasRows)
                            {
                                FillGaps = true;

                                ID = (int)Reader["ContactID"];
                                Phone = (string)Reader["Phone"];
                                Email = (string)Reader["Email"];

                            }
                        }


                    }
                }

            }
            catch (Exception ex)
            {
                return false;
            }


            return FillGaps;

        }


        public static int? AddNewContact (string Phone , string Email,string Address)
        {

            int? ContactID = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {

                    string query = @"insert into Contacts values(@Phone ,@Email,@Address)
                                    select scope_identity();
                                    ";

                    using (SqlCommand Command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        Command.Parameters.AddWithValue(@"Phone", Phone);
                        Command.Parameters.AddWithValue(@"Email", Email);
                        Command.Parameters.AddWithValue(@"Address", Address);


                        object value = Command.ExecuteScalar();

                        if (value != null && int.TryParse(value.ToString(), out int ID))
                        {
                            ContactID = ID;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                return null;
            }

            return ContactID;
        }


        public static bool UpdateContact(int? id,string Phone, string Email, string Address)
        {

            if (id <= 0 || id == null) return false;

            byte rowEffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {

                    string query = @"update Contacts 
                                    set Phone = @Phone,
                                        Email = @Email,
                                        Address = @Address,
                                        where ContactID = @ID
                                    ";

                    using (SqlCommand Command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        Command.Parameters.AddWithValue(@"ID", id);
                        Command.Parameters.AddWithValue(@"Phone", Phone);
                        Command.Parameters.AddWithValue(@"Email", Email);
                        Command.Parameters.AddWithValue(@"Address", Address);

                        
                       rowEffected = (byte)Command .ExecuteNonQuery();

                    }
                }

            }
            catch (Exception ex)
            {
                return false;
            }

            return (rowEffected > 0);
        }

        public static bool DeleteContact(int? id)
        {
            if(id <= 0 || id == null) { return false; }

            byte rowEffected = 0;


            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {

                    string querey = @"delete Contacts where ContactID = @ID";

                    using (SqlCommand Command = new SqlCommand(querey, connection))
                    {

                        connection.Open();

                        Command.Parameters.AddWithValue(@"ID", id);

                        rowEffected = (byte)Command.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
            }

            return (rowEffected > 0);
        }


        public static DataTable GetEachContacts()
        {

            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {

                    string query = @"select * From Contacts";

                    using (SqlCommand Command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using (SqlDataReader reader = Command.ExecuteReader())
                        {

                            if (reader.HasRows)
                            {
                                dt.Load(reader);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }

            return dt;
        }
    }
}
