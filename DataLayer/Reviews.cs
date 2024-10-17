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
    public class Reviews
    {

        public static bool FindByID(int? ID, ref int UserID, ref bool Status, ref string Comment)
        {
            if (ID == null || ID <= 0) return false;

            bool CorracteData = false;

            try
            {

                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {
                    connection.Open();

                    string query = @"Select * Form Reviews where ReviewID =@ID ";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue(@"ID", ID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                CorracteData = true;

                                UserID = (int)reader["UserID"];
                                Status = (bool)reader["ReviewStatus"];
                                Comment = (string)reader["comment"];

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

            return CorracteData;
        }

        public static bool FindByUserID(ref int ID, int? UserID, ref bool Status, ref string Comment)
        {
            if (UserID == null || UserID <= 0) return false;

            bool CorracteData = false;

            try
            {

                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {
                    connection.Open();

                    string query = @"Select * Form Reviews where UserID =@ID ";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue(@"ID", ID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                CorracteData = true;
                                ID = (int)reader["ReviewID"];
                                Status = (bool)reader["ReviewStatus"];
                                Comment = (string)reader["comment"];

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

            return CorracteData;
        }

        public static int? AddNewReview(int? UserID, bool Status, string Comment)
        {

            int? ReviewID = null;

            try
            {

                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {
                    connection.Open();

                    string query = @"insert into Reviews values(@UserID , @Status,@Comment)
                                    select Scope_Identity();";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue(@"UserID", UserID);
                        command.Parameters.AddWithValue(@"Status", Status);
                        command.Parameters.AddWithValue(@"Comment", Comment);

                        object value = command.ExecuteScalar();

                        if (value != null && int.TryParse(value.ToString(), out int ID))
                        {
                            ReviewID = ID;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

            return ReviewID;
        }

        public static bool UpdateReview(int? ID, int? UserID, bool Status, string Comment)
        {

            if (ID == null || ID <= 0) return false;

            byte RowEffected = 0;

            try
            {

                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {
                    connection.Open();

                    string query = @"Update Reviews 
                                      set UserID = @UserID,
                                          ReviewStatus = @Status
                                          Comment = @Comment
                                           ReviewID = @ID";


                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue(@"ID", ID);
                        command.Parameters.AddWithValue(@"UserID", UserID);
                        command.Parameters.AddWithValue(@"Status", Status);
                        command.Parameters.AddWithValue(@"Comment", Comment);


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

        public static bool DeleteByID(int? ID)
        {
            if (ID == null || ID <= 0) return false;

            byte RowEffected = 0;


            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {
                    connection.Open();

                    string query = @"Delete Reviews where ReviewID = @ID";

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

        public static bool DeleteByUserID(int? UserID)
        {
            if (UserID == null || UserID <= 0) return false;

            byte RowEffected = 0;


            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {
                    connection.Open();

                    string query = @"Delete Reviews where UserID = @ID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue(@"ID", UserID);

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

        public static async  Task<DataTable> GetEeachReviewsAsync()
        {
            DataTable Table = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {
                    await connection.OpenAsync();

                    string query = @"Select * From Reviews";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            if (await reader.ReadAsync())
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

        public static  DataTable GetEeachReviewsSync()
        {
            DataTable Table = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {
                     connection.Open();

                    string query = @"Select * From Reviews";

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
