using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataLayer
{
    public class PaymentsInformation
    {

        public static bool FindByID(int? ID, ref string CardNumber, ref string CardHolderName, ref DateTime ExpirationDate, ref string CVV_Code)
        {
            if (ID <= 0 || ID == null) return false;
            bool FillGaps = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {
                    connection.Open();

                    string query = @"select * From Payments_Information where Payment_InfoID = @ID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue(@"ID", ID);


                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {

                                CardNumber = (string)reader["Card_Number"];
                                CardHolderName = (string)reader["CardholderName"];
                                ExpirationDate = (DateTime)reader["ExpirationDate"];
                                CVV_Code = (string)reader["CVV_CVC_Code"];

                                FillGaps = true;
                            }//close Reader
                        }
                    }
                }

            }
            catch
            {
                return false;
            }

            return FillGaps;
        }


        public static bool FindByHolderName(ref int? ID, ref string CardNumber, string CardHolderName, ref DateTime ExpirationDate, ref string CVV_Code)
        {
            if (string.IsNullOrEmpty(CardHolderName)) return false;

            bool FillGaps = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {
                    connection.Open();

                    string query = @"select * From Payments_Information where CardholderName = @HolderName";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue(@"HolderName", CardHolderName);


                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {

                                ID = (int)reader["Payment_InfoID"];
                                CardNumber = (string)reader["Card_Number"];
                                ExpirationDate = (DateTime)reader["ExpirationDate"];
                                CVV_Code = (string)reader["CVV_CVC_Code"];

                                FillGaps = true;
                            }//close Reader
                        }
                    }
                }

            }
            catch
            {
                return false;
            }

            return FillGaps;
        }


        public static int? AddNewPaymentInfo(string CardNumber, string CardholderNumber, DateTime ExpirationDate, string CVV_Code)
        {

            int? payInfoID = null;

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
            {


                try
                {

                    connection.Open();


                    string querey = @"insert into Payments_Information values(@CardNumber,@CardholderNumber,@ExpirationDate,@CVV_Code)
                                        select scope_identity();";

                    using (SqlCommand command = new SqlCommand(querey, connection))
                    {
                        command.Parameters.AddWithValue(@"CardNumber", CardNumber);
                        command.Parameters.AddWithValue(@"CardholderNumber", CardholderNumber);
                        command.Parameters.AddWithValue(@"ExpirationDate", ExpirationDate);
                        command.Parameters.AddWithValue(@"CVV_Code", CVV_Code);



                        object value = command.ExecuteScalar();

                        if (value != null && int.TryParse(value.ToString(), out int ID))
                        {

                            payInfoID = ID;
                        }

                    }



                }
                catch
                {
                    return null;
                }

            }


            return payInfoID;
        }


        public static bool UpdatePaymentInfo(int? id, string CardNumber, string CardholderNumber, DateTime ExpirationDate, string CVV_Code)
        {

            byte? rowEffected = null;

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
            {


                try
                {

                    connection.Open();


                    string querey = @"update Payments_Information
                                      set Card_Number = @CardNumber,
                                          CardholderNumber = @HolderNumber,
                                          ExpirationDate = @ExpirationDate,
                                          CVV_CVC_Code = @Code,
                                          where Payment_InfoID = @ID";

                    using (SqlCommand command = new SqlCommand(querey, connection))
                    {
                        command.Parameters.AddWithValue(@"ID", id);
                        command.Parameters.AddWithValue(@"CardNumber", CardNumber);
                        command.Parameters.AddWithValue(@"HolderNumber", CardholderNumber);
                        command.Parameters.AddWithValue(@"ExpirationDate", ExpirationDate);
                        command.Parameters.AddWithValue(@"Code", CVV_Code);



                        rowEffected = (byte)command.ExecuteNonQuery();
                    }



                }
                catch
                {
                    return false;
                }

            }


            return (rowEffected > 0);
        }


        public static bool DeletePaymentInfo(int? ID)
        {

            if (ID <= 0 || ID == null) return false;

            byte? rowEffected = null;

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
            {


                try
                {
                    connection.Open();

                    string querey = @"delete Payments_Information where Payment_InfoID =@ID";

                    using (SqlCommand command = new SqlCommand(querey, connection))
                    {
                        command.Parameters.AddWithValue(@"ID", ID);

                        rowEffected = (byte)command.ExecuteNonQuery();
                    }

                }
                catch (Exception ex)
                {
                    return false;
                }


                return (rowEffected > 0);

            }
        }


        public static DataTable GetEachPaymentInformationCard()
        {
            DataTable table = new DataTable();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
            {
                try
                {
                    connection.Open();

                    string querey = @"select * from Payments_Information";


                    using (SqlCommand command = new SqlCommand(querey, connection))
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
                catch (Exception ex)
                {
                    return null;
                }
            }

            return table;

        }

       
    }
}
