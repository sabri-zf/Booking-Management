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
    public class Services
    {

        public static bool FindByID(int? ID ,ref  int? CategoryID,ref string ServiceName,ref string Description,ref int? LocationID,ref DateTime DurationTime,
            ref string ServiceProvider,ref short Capacity, ref string AdditionalNotes,ref byte? Discount)
        {

            if(!ID.HasValue || ID <=0) return false;

            bool CorrectData = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {
                    connection.Open();

                    string query = @"select * From Services where ServiceID = @ID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue(@"ID", ID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            if (reader.Read())
                            {
                                CorrectData = true;
                                CategoryID = (int)reader["CategoryID"];
                                ServiceName = (string)reader["ServiceName"];
                                Description = (reader["Description"] == DBNull.Value) ? string.Empty : (string)reader["Description"];
                                LocationID = (int)reader["LocationID"];
                                DurationTime = (reader["DurationTime"] == DBNull.Value) ? new DateTime(2000, 01, 01) : (DateTime)reader["DurationTime"];
                                ServiceProvider = (string)reader["ServiceProvider"];
                                Capacity = (reader["Capacity"] == DBNull.Value) ? (short)0 : (short)reader["Capacity"];
                                AdditionalNotes = (reader["AdditionalNotes"] == DBNull.Value) ? string.Empty : (string)reader["AdditionalNotes"];
                                Discount = (reader["Discount"] == DBNull.Value) ? (byte)0 : (byte)reader["Discount"];
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


        public static int? AddNewService( int? CategoryID,  string ServiceName,  string Description,  int? LocationID,  DateTime DurationTime,
             string ServiceProvider,  short Capacity,  string AdditionalNotes,  byte? Discount)
        {

            int? ServiceID = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {
                    connection.Open();

                    string query = @"insert into Services values (@CategoryID,@ServiceName,@Description,@LocationID,@DurationTime,
                                                                  @ServiceProvider,@Capacity,@Notes,@Discount)
                                     Select Scope_identity();";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue(@"CategoryID", CategoryID);
                        command.Parameters.AddWithValue(@"ServiceName", ServiceName);

                        if (string.IsNullOrEmpty(Description))
                        {
                            command.Parameters.AddWithValue(@"Description", DBNull.Value);

                        }
                        else
                        {
                            command.Parameters.AddWithValue(@"Description", Description);
                        }

                        command.Parameters.AddWithValue(@"LocationID", LocationID);

                        if (DurationTime == new DateTime(2000, 01, 01))
                        {
                            command.Parameters.AddWithValue(@"DurationTime", DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue(@"DurationTime", DurationTime);

                        }

                        command.Parameters.AddWithValue(@"ServiceProvider", ServiceProvider);

                        if (Capacity == 0)
                        {
                            command.Parameters.AddWithValue(@"Capacity", DBNull.Value);

                        }
                        else
                        {
                            command.Parameters.AddWithValue(@"Capacity", Capacity);
                        }

                        if (string.IsNullOrEmpty(AdditionalNotes))
                        {
                            command.Parameters.AddWithValue(@"Notes", DBNull.Value);

                        }
                        else
                        {
                            command.Parameters.AddWithValue(@"Notes", AdditionalNotes);
                        }


                        if (Discount == 0)
                        {
                            command.Parameters.AddWithValue(@"Discount", DBNull.Value);

                        }
                        else
                        {
                            command.Parameters.AddWithValue(@"Discount", Discount);
                        }


                        object value = command.ExecuteScalar();

                        if (value != null && int.TryParse(value.ToString(), out int ID))
                        {
                            ServiceID = ID;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }

            return ServiceID;

        }


        public static bool UpdateService(int?  ID,int? CategoryID, string ServiceName, string Description, int? LocationID, DateTime DurationTime,
          string ServiceProvider, short Capacity, string AdditionalNotes, byte? Discount)
        {
            if(!ID.HasValue || ID <=0) return false;
            byte RowEffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {
                    connection.Open();

                    string query = @"update Services 
                                    set CategoryID = @CategoryID,
                                        ServiceName = @ServiceName,
                                        Description = @Description,
                                        LocationID = @LocationID,
                                        DurationTime = @DurationTime,
                                        ServiceProvider = @ServiceProvider,
                                        Capacity = @Capacity,
                                        AdditionalNotes = @Notes,
                                        Discount = @Discount
                                        where ServiceID = @ID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue(@"ID", ID);
                        command.Parameters.AddWithValue(@"CategoryID", CategoryID);
                        command.Parameters.AddWithValue(@"ServiceName", ServiceName);

                        if (string.IsNullOrEmpty(Description))
                        {
                            command.Parameters.AddWithValue(@"Description", DBNull.Value);

                        }
                        else
                        {
                            command.Parameters.AddWithValue(@"Description", Description);
                        }

                        command.Parameters.AddWithValue(@"LocationID", LocationID);

                        if (DurationTime == new DateTime(2000, 01, 01))
                        {
                            command.Parameters.AddWithValue(@"DurationTime", DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue(@"DurationTime", DurationTime);

                        }

                        command.Parameters.AddWithValue(@"ServiceProvider", ServiceProvider);

                        if (Capacity == 0)
                        {
                            command.Parameters.AddWithValue(@"Capacity", DBNull.Value);

                        }
                        else
                        {
                            command.Parameters.AddWithValue(@"Capacity", Capacity);
                        }

                        if (string.IsNullOrEmpty(AdditionalNotes))
                        {
                            command.Parameters.AddWithValue(@"Notes", DBNull.Value);

                        }
                        else
                        {
                            command.Parameters.AddWithValue(@"Notes", AdditionalNotes);
                        }


                        if (Discount == 0)
                        {
                            command.Parameters.AddWithValue(@"Discount", DBNull.Value);

                        }
                        else
                        {
                            command.Parameters.AddWithValue(@"Discount", Discount);
                        }


                        RowEffected = (byte)command.ExecuteNonQuery();

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


        public static bool Delete(int? ID)
        {
            if (!ID.HasValue || ID <= 0) return false;

            byte roweffected = 0;

            try
            {

                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {
                    connection.Open();

                    string query = @"delete Services Where ServiceID = @ID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue(@"ID", ID);


                        roweffected = (byte)command.ExecuteNonQuery();
                    }


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

            return (roweffected > 0);

        }


        public static async Task<DataTable> GetListOfServicesAsync()
        {
            DataTable List = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {
                    await connection.OpenAsync();

                    string query = @"select * From Services";


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

    }
}
