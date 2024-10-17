using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Payments
    {

        public static bool FindByID(int? ID, ref DateTime PaymentDate, ref decimal IntialTotalDueAmount, ref decimal? ActualTotalDueAmount
           , ref decimal? TotalRemainingCost, ref decimal? TotalAddtionalCost, ref DateTime UpdatePaymentDate)
        {
            if (ID == null || ID <= 0) return false;


            bool CorrectData = false;
            try
            {

                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {
                    connection.Open();

                    string query = @"select * from Payments where PaymentID = @ID ";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue(@"ID", ID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            if (reader.HasRows)
                            {
                                CorrectData = true;

                                PaymentDate = (DateTime)reader["PaymentDate"];
                                IntialTotalDueAmount = (decimal)reader["IntialTotalDueAmount"];
                                ActualTotalDueAmount = (reader["ActualTotalDueAmount"]) == DBNull.Value ? 0 : (decimal)reader["ActualTotalDueAmount"];
                                TotalRemainingCost = (reader["TotalRemainingCost"]) == DBNull.Value ? 0 : (decimal)reader["TotalRemainingCost"];
                                TotalAddtionalCost = (reader["TotalAdditionalCost"] == DBNull.Value) ? 0 : (decimal)reader["TotalAdditionalCost"];
                                UpdatePaymentDate = (reader["UpdatePaymentDate"] == DBNull.Value) ? new DateTime(2000, 01, 01) : (DateTime)reader["UpdatePaymentDate"];
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

        public static bool FindPaymentDate(ref int ID, DateTime PaymentDate, ref decimal IntialTotalDueAmount, ref decimal? ActualTotalDueAmount
           , ref decimal? TotalRemainingCost, ref decimal? TotalAddtionalCost, ref DateTime UpdatePaymentDate)
        {
            if (PaymentDate == null) return false;


            bool CorrectData = false;
            try
            {

                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {
                    connection.Open();

                    string query = @"select * from Payments where PaymentDate = @Date ";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue(@"Date", PaymentDate);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            if (reader.HasRows)
                            {
                                CorrectData = true;

                                ID = (int)reader["PaymentID"];
                                IntialTotalDueAmount = (decimal)reader["IntialTotalDueAmount"];
                                ActualTotalDueAmount = (reader["ActualTotalDueAmount"]) == DBNull.Value ? 0 : (decimal)reader["ActualTotalDueAmount"];
                                TotalRemainingCost = (reader["TotalRemainingCost"]) == DBNull.Value ? 0 : (decimal)reader["TotalRemainingCost"];
                                TotalAddtionalCost = (reader["TotalAdditionalCost"] == DBNull.Value) ? 0 : (decimal)reader["TotalAdditionalCost"];
                                UpdatePaymentDate = (reader["UpdatePaymentDate"] == DBNull.Value) ? new DateTime(2000, 01, 01) : (DateTime)reader["UpdatePaymentDate"];
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


        public static int? AddNewPayment(DateTime PaymentDate, decimal IntialTotalDueAmount, decimal? ActualTotalDueAmount
           , decimal? TotalRemainingCost, decimal? TotalAddtionalCost, DateTime UpdatePaymentDate)
        {
            int? PaymentID = null;
            try
            {

                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {
                    connection.Open();

                    string query = @"Insert into Payments values(@PaymentDate,@IntialTotalDueAmount,@ActualTotalDueAmount,@TotalRemainingCost
                                    ,@TotalAddtionalCost,@UpdatePaymentDate)
                                    Select Scope_Identity();";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue(@"PaymentDate", PaymentDate);
                        command.Parameters.AddWithValue(@"IntialTotalDueAmount", IntialTotalDueAmount);

                        if (ActualTotalDueAmount == 0)
                        {
                            command.Parameters.AddWithValue(@"ActualTotalDueAmount", DBNull.Value);

                        }
                        else
                        {
                            command.Parameters.AddWithValue(@"ActualTotalDueAmount", ActualTotalDueAmount);
                        }

                        if (TotalRemainingCost == 0)
                        {
                            command.Parameters.AddWithValue(@"TotalRemainingCost", DBNull.Value);

                        }
                        else
                        {
                            command.Parameters.AddWithValue(@"TotalRemainingCost", TotalRemainingCost);
                        }

                        if (TotalAddtionalCost == 0)
                        {
                            command.Parameters.AddWithValue(@"TotalAddtionalCost", DBNull.Value);

                        }
                        else
                        {
                            command.Parameters.AddWithValue(@"TotalAddtionalCost", TotalAddtionalCost);
                        }

                        if (UpdatePaymentDate == new DateTime(2000, 01, 01))
                        {
                            command.Parameters.AddWithValue(@"UpdatePaymentDate", DBNull.Value);

                        }
                        else
                        {
                            command.Parameters.AddWithValue(@"UpdatePaymentDate", UpdatePaymentDate);
                        }


                        object Value = command.ExecuteScalar();

                        if (Value != null && int.TryParse(Value.ToString(), out int ID))
                        {
                            PaymentID = ID;
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

            return PaymentID;

        }

        public static bool UpdatePayment(int? ID, DateTime PaymentDate, decimal IntialTotalDueAmount, decimal? ActualTotalDueAmount,
            decimal? TotalRemainingCost, decimal? TotalAddtionalCost, DateTime UpdatePaymentDate)
        {
            if (ID == null || ID <= 0) return false;


            byte Roweffected = 0;
            try
            {

                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {
                    connection.Open();

                    string query = @"update Payments 
                                    set PaymentDate = @PayDate,
                                        IntialTotalDueAmount = @IntialTotal,
                                        ActualTotalDueAmount = @ActualTotal,
                                        TotalRemainingCost   = @TotalRemaining,
                                        TotalAdditionalCost  = @TotalAdditional,
                                        UpdatePaymentDate    = @UpdatePayDate
                                        where PaymentID = @ID;";


                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue(@"ID", ID);
                        command.Parameters.AddWithValue(@"PayDate", PaymentDate);
                        command.Parameters.AddWithValue(@"IntialTotal", IntialTotalDueAmount);

                        if (ActualTotalDueAmount == 0)
                        {
                            command.Parameters.AddWithValue(@"ActualTotal", DBNull.Value);

                        }
                        else
                        {
                            command.Parameters.AddWithValue(@"ActualTotal", ActualTotalDueAmount);
                        }

                        if (TotalRemainingCost == 0)
                        {
                            command.Parameters.AddWithValue(@"TotalRemaining", DBNull.Value);

                        }
                        else
                        {
                            command.Parameters.AddWithValue(@"TotalRemaining", TotalRemainingCost);
                        }

                        if (TotalAddtionalCost == 0)
                        {
                            command.Parameters.AddWithValue(@"TotalAdditional", DBNull.Value);

                        }
                        else
                        {
                            command.Parameters.AddWithValue(@"TotalAdditional", TotalAddtionalCost);
                        }

                        if (UpdatePaymentDate == new DateTime(2000, 01, 01))
                        {
                            command.Parameters.AddWithValue(@"UpdatePayDate", DBNull.Value);

                        }
                        else
                        {
                            command.Parameters.AddWithValue(@"UpdatePayDate", UpdatePaymentDate);
                        }

                        Roweffected = (byte)command.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

            return (Roweffected > 0);
        }


        public static bool DeletePayment(int? ID)
        {
            if (ID == null || ID <= 0) return false;

            byte RowEffected = 0;

            try
            {

                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {
                    connection.Open();

                    string query = @"Delete Payments where PaymentID = @ID";

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

        public static bool DeletePayment(DateTime PaymentDate)
        {
            if (PaymentDate == null) return false;

            byte RowEffected = 0;

            try
            {

                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {
                    connection.Open();

                    string query = @"Delete Payments where PaymentDate = @PayDate";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue(@"PayDate", PaymentDate);

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


        public static async Task<DataTable> GetEachPaymentsAsync()
        {
            DataTable List = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {
                    await connection.OpenAsync();

                    string query = @"Select * from Payments";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            if (await reader.ReadAsync())
                            {
                                List.Load(reader);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return List;
            }

            return List;
        }

        public static DataTable GetEachPaymentsSync()
        {
            DataTable List = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {
                     connection.Open();

                    string query = @"Select * from Payments";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            if ( reader.Read())
                            {
                                List.Load(reader);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return List;
            }

            return List;
        }


    }
}
