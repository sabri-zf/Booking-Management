using Microsoft.SqlServer.Server;
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
    public class Countries
    {

        public static bool FindbyID(int ID , ref string CountryName,ref string ISO)
        {
            if (ID <= 0 || ID == null) return false;

            bool FillGaps=false;
            try
            {

                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {

                    string query = @"select * from Countries where  countryID = @ID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        command.Parameters.AddWithValue(@"ID", ID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {

                                FillGaps = true;

                                CountryName = (string)reader["Name"];
                                ISO = (string)reader["ISO"];

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

        public static bool FindbyISO(ref int? ID, ref string CountryName,  string ISO)
        {
            if (string.IsNullOrEmpty(ISO)) return false;

            bool FillGaps = false;
            try
            {

                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {

                    string query = @"select * from Countries where  ISO = @iso_code";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        command.Parameters.AddWithValue(@"iso_code", ISO);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {

                                FillGaps = true;

                                ID = (int)reader["CountryID"];
                                CountryName = (string)reader["Name"];

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


        public static string GetToCountryName(int? ID)
        {
            if (ID == null) return null;

            string CountryName = string.Empty;
            try
            {

                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {
                    string querey = @" select Name from Countries where CountryID = @ID";

                    using (SqlCommand command = new SqlCommand(querey, connection))
                    {
                        connection.Open();

                        command.Parameters.AddWithValue(@"ID", ID);

                        object value = command.ExecuteScalar();

                        if (value != null)
                        {
                            CountryName = value.ToString();
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                return null;
            }

            return CountryName;
        }

        public static string GetToCountryName(string ISO)
        {
            if (string.IsNullOrEmpty(ISO)) return null;

            string CountryName = string.Empty;
            try
            {

                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {
                    string querey = @" select Name from Countries where ISO = @ISO_Code";

                    using (SqlCommand command = new SqlCommand(querey, connection))
                    {
                        connection.Open();

                        command.Parameters.AddWithValue(@"ISO_Code", ISO);

                        object value = command.ExecuteScalar();

                        if (value != null)
                        {
                            CountryName = value.ToString();
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                return null;
            }

            return CountryName;
        }


        public static DataTable GetEachCountries()
        {
            DataTable table = new DataTable();

            try
            {

                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {
                    string querey = @"select * From Countries";

                    using (SqlCommand command = new SqlCommand(querey, connection))
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            if (reader.HasRows)
                            {
                                table.Load(reader);
                            }
                        }
                    }
                }
            }
            catch
            {
                return table;
            }

            return table;
        }

    }
}
