using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    /*
     * in this Code We need Call with Data base using Diffiant Access T-SQL Language
     * This  and ADO.net both is Fast  But the T-SQL Its more fixable to using 
     */
    public class Booking
    {




        public static bool FindByID(int? ID ,ref int UserID , ref DateTime DateStart ,ref DateTime DateEnd ,ref int ServiceID ,ref byte StatusID,
            ref short IntialReservitionDay , ref decimal IntialTotalDueAmount,ref string Notes ,ref int PaymentID)
        {

            if(ID == null || ID <= 0) return false;


            bool CorrectData = false;

            try
            {

                using (SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings
                    ["BookingMangement_DB"].ConnectionString))
                {
                    Connection.Open ();

                    // Using Store Procedure of T-SQL
                    using (SqlCommand Command = new SqlCommand("SP_FindBy_BookingID", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.AddWithValue("@NewBookingID", ID);


                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {
                            if (Reader.Read())
                            {
                                CorrectData = true;

                                UserID = (int)Reader["UserID"];
                                DateStart = (DateTime)Reader["DateStart"];
                                DateEnd = (DateTime)Reader["DateEnd"];
                                ServiceID = (int)Reader["ServiceID"];
                                StatusID = (byte)Reader["StatusID"];
                                IntialReservitionDay = (short)Reader["IntialReservitionDay"];
                                IntialTotalDueAmount = (decimal)Reader["IntialTotalDueAmount"];
                                Notes = (Reader["Notes"] == DBNull.Value) ? string.Empty : (string)Reader["Notes"];
                                PaymentID = (int)Reader["PaymentID"];

                            }

                        }
                    }
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }

            return CorrectData;
        }

        public static bool FindByUserID(ref int ID,  int? UserID, ref DateTime DateStart, ref DateTime DateEnd, ref int ServiceID, ref byte StatusID,
          ref short IntialReservitionDay, ref decimal IntialTotalDueAmount, ref string Notes, ref int PaymentID)
        {

            if (UserID == null || UserID <= 0) return false;


            bool CorrectData = false;

            try
            {

                using (SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings
                    ["BookingMangement_DB"].ConnectionString))
                {
                    Connection.Open();

                    // Using Store Procedure of T-SQL
                    using (SqlCommand Command = new SqlCommand("SP_FindByBooking_UserID", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.AddWithValue("@UserID", UserID);


                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {
                            if (Reader.Read())
                            {
                                CorrectData = true;

                                ID = (int)Reader["BookingID"];
                                DateStart = (DateTime)Reader["DateStart"];
                                DateEnd = (DateTime)Reader["DateEnd"];
                                ServiceID = (int)Reader["ServiceID"];
                                StatusID = (byte)Reader["StatusID"];
                                IntialReservitionDay = (short)Reader["IntialReservitionDay"];
                                IntialTotalDueAmount = (decimal)Reader["IntialTotalDueAmount"];
                                Notes = (Reader["Notes"] == DBNull.Value) ? string.Empty : (string)Reader["Notes"];
                                PaymentID = (int)Reader["PaymentID"];

                            }

                        }
                    }
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }

            return CorrectData;
        }

        public static int? AddNewBooking(int? UserID, DateTime DateStart, DateTime DateEnd
            , int? ServiceID, byte StatusID, short IntialReservitionDay, decimal IntialTotalDueAmount
            , string Notes, int? PaymentID)
        {

            int? BookingID = null;


            try
            {

                using (SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings
                   ["BookingMangement_DB"].ConnectionString))
                {
                    Connection.Open();

                    using (SqlCommand Command = new SqlCommand("SP_AddNewBooking", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("@UserID", UserID);
                        Command.Parameters.AddWithValue("@DataStart", DateStart);
                        Command.Parameters.AddWithValue("@DataEnd", DateEnd);
                        Command.Parameters.AddWithValue("@ServiceID", ServiceID);
                        Command.Parameters.AddWithValue("@StatusID", StatusID);
                        Command.Parameters.AddWithValue("@IntialReservitionDay", IntialReservitionDay);
                        Command.Parameters.AddWithValue("@intialTotalDueAmount", IntialTotalDueAmount);
                        
                        if (string.IsNullOrEmpty(Notes))
                        {
                            Command.Parameters.AddWithValue("@Notes", DBNull.Value);
                        }
                        else
                        {
                            Command.Parameters.AddWithValue("@Notes", Notes);
                        }

                        Command.Parameters.AddWithValue("@PaymentID", PaymentID);

                        SqlParameter NewBookingID = new SqlParameter("@NewBookingID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };

                        Command.Parameters.Add(NewBookingID);

                        if (Command.ExecuteNonQuery() > 0)
                        {

                            BookingID = (int)Command.Parameters["@NewBookingID"].Value;

                        }




                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }

            return BookingID;
        }


        public static bool UpdateBooking(int? ID,int? UserID, DateTime DateStart, DateTime DateEnd
          , int? ServiceID, byte StatusID, short IntialReservitionDay, decimal IntialTotalDueAmount
          , string Notes, int? PaymentID)
        {

            if(ID <= 0 || ID == null) return false;

            //int? BookingID = null
            //
            //;
            byte RowEffected = 0;

            try
            {

                using (SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings
                   ["BookingMangement_DB"].ConnectionString))
                {
                    Connection.Open();

                    using (SqlCommand Command = new SqlCommand("SP_AddNewBooking", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue(@"BookingID", ID);
                        Command.Parameters.AddWithValue(@"UserID", UserID);
                        Command.Parameters.AddWithValue(@"DataStart", DateStart);
                        Command.Parameters.AddWithValue(@"DataEnd", DateEnd);
                        Command.Parameters.AddWithValue(@"ServiceID", ServiceID);
                        Command.Parameters.AddWithValue(@"StatusID", StatusID);
                        Command.Parameters.AddWithValue(@"IntialReservitionDay", IntialReservitionDay);
                        Command.Parameters.AddWithValue(@"intialTotalDueAmount", IntialTotalDueAmount);

                        if (string.IsNullOrEmpty(Notes))
                        {
                            Command.Parameters.AddWithValue(@"Notes", DBNull.Value);
                        }
                        else
                        {
                            Command.Parameters.AddWithValue(@"Notes", Notes);
                        }

                        Command.Parameters.AddWithValue(@"PaymentID", PaymentID);

                        SqlParameter ReturnValue = new SqlParameter();
                        ReturnValue.Direction = ParameterDirection.ReturnValue;

                        Command.Parameters.Add(ReturnValue);

                        Command.ExecuteNonQuery();

                        RowEffected = (byte)ReturnValue.Value;
                      
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }

            return (RowEffected > 0);

        }



        public static bool DetetBooking(int? ID)
        {

            byte RowEffected = 0;
            if(ID == null || ID <= 0) return false;

            try
            {
                using (SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings
                  ["BookingMangement_DB"].ConnectionString))
                {

                    Connection.Open();

                    using (SqlCommand Command = new SqlCommand("SP_Booking_Delete", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("@NewBooking", ID);

                        SqlParameter ReturnValue = new SqlParameter();
                        ReturnValue.Direction = ParameterDirection.ReturnValue;
                        Command.Parameters.Add(ReturnValue);

                        Command.ExecuteNonQuery();

                        RowEffected = (byte)(ReturnValue.Value);
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }

            return (RowEffected > 0);
        }


        public static async Task<DataTable> GetListsOFBookingAsync()
        {
            DataTable List = new DataTable();

            try
            {

                using (SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings
                  ["BookingMangement_DB"].ConnectionString))
                {
                    await Connection.OpenAsync();

                    string query = @"select * from Booking";

                    using (SqlCommand Command = new SqlCommand(query, Connection))
                    {
                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {

                            if (await Reader.ReadAsync())
                            {
                                List.Load(Reader);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return List;
            }

            return List;
        }

        public static DataTable GetListsOFBookingSync()
        {
            DataTable List = new DataTable();

            try
            {

                using (SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings
                  ["BookingMangement_DB"].ConnectionString))
                {
                     Connection.Open();

                    string query = @"select * from Booking";

                    using (SqlCommand Command = new SqlCommand(query, Connection))
                    {
                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {

                            if ( Reader.Read())
                            {
                                List.Load(Reader);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return List;
            }

            return List;
        }


        public static bool UpdateStatus(int? ID, byte statusID)
        {
            if(ID <= 0 || ID == null) return false;

            byte RowEffected = 0;
            try
            {
                using (SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings
                 ["BookingMangement_DB"].ConnectionString))
                {
                    Connection.Open();

                    string query = @"update Booking 
                                   set StatusID = @Status
                                       where BookingID = @ID";

                    using(SqlCommand Command = new SqlCommand(query, Connection))
                    {

                        Command.Parameters.AddWithValue(@"Status",statusID);
                        Command.Parameters.AddWithValue(@"ID",ID);

                        RowEffected= (byte)Command.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }


            return (RowEffected > 0);
        }

    }
}
