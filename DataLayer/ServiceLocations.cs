using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataLayer
{
    public class ServiceLocations
    {

        public static bool FindByID(int? ID ,ref int CountryID)
        {
            if(ID == null || ID <=0) return false;


            bool CorrectData = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {
                    connection.Open();

                    string query = @"select * From Locations where LocationID = @ID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue(@"ID", ID);


                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                CorrectData = true;
                                CountryID = (int)reader["CountryID"];
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

        public static bool Delete(int? ID)
        {
            if (ID == null || ID <= 0) return false;

            byte RowEffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {
                    connection.Open();

                    string query = @"delete Locations where LocationID = @ID";

                    using(SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue(@"ID",ID);

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


        public static int? AddNewLocation(int? CountryID)
        {

            int? LocationID = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {
                    connection.Open ();

                    string query = @"insert into Locations values(@CountryID)
                                    select scope_identity();";

                    using( SqlCommand command = new SqlCommand(query,connection))
                    {
                        command.Parameters.AddWithValue(@"CountryID", CountryID);

                       object value = command.ExecuteScalar();

                        if(value != null && int.TryParse(value.ToString(),out int ID))
                        {
                            LocationID = ID;
                        }
                    }

                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

            return LocationID;
        }

        public static bool UpdateLocation(int? ID ,int? CountryID)
        {
            if(ID == null || ID <= 0) return false;

            byte rowEffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {
                    connection.Open();

                    string query = @"Update Locations 
                                      set CountryID = @CountryID
                                      where LocationID = @ID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue(@"CountryID", CountryID);
                        command.Parameters.AddWithValue(@"ID", ID);

                       rowEffected = (byte)command.ExecuteNonQuery();


                    }

                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

            return (rowEffected > 0);
        }

        public static string GetLocationName(int? ID , int? CountryID)
        {
         
            if(ID == null || ID <= 0) return null;

            string LocationName = string.Empty;
            try
            {

                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {
                    connection.Open();

                    string query = @"select Locations.LocationID,Countries.Name as countryName from Locations
                                     inner join Countries on Countries.CountryID = @CountryID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue(@"CountryID",CountryID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            {
                                if (reader.Read())
                                {
                                    LocationName = (string)reader["countryName"];
                                }
                            }
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            return LocationName;
        }


        //public DataTable GetLocationOfServer
    }
}
