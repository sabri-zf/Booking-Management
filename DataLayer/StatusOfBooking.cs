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
    public class StatusOfBooking
    {

        public static bool FindByID(sbyte? ID ,ref string StatusName)
        {
            if(ID <=0 || ID == null) return false;

            bool CorrectData = false;

            try
            {

                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {
                    connection.Open();

                    string query = @"Select * From Status Where StatusID =@ID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue(@"ID", ID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                CorrectData = true;
                                StatusName = (string)reader["Name"];
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

        public static string GetStatusName(sbyte? ID)
        {
            if (ID <= 0 || ID == null) return string.Empty;

            //bool CorrectData = false;
            string statusName = string.Empty;
            try
            {

                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {
                    connection.Open ();

                    string query = @" Select Name From Status Where StatusID =@ID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue(@"ID", ID);

                        object value = command.ExecuteScalar();

                        if (value != null)
                        {
                            statusName = (string)value;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return string.Empty;
            }

            return statusName;
        }


        public static DataTable GetAllStatusValue()
        {
            DataTable List = new DataTable();


            try
            {

                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {
                    connection.Open();

                    string query = @"select * From Status";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
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
