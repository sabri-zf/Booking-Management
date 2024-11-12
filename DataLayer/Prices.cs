using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Prices
    {


        public static decimal PriceOfService(int ?serviceID)
        {

            if(serviceID <= 0 || serviceID == null)return 0;

            decimal price = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {
                    connection.Open();

                    string querey = @"select Amount from Prices where ServiceID = @ServiceID";

                    using (SqlCommand command = new SqlCommand(querey, connection))
                    {
                        command.Parameters.AddWithValue(@"ServiceID",serviceID);


                        object value = command.ExecuteScalar();

                        if (value != null && decimal.TryParse(value.ToString(), out decimal Cost))
                        {
                            price = Cost;
                        }

                    }
                }
            }
            catch ( Exception ex)
            {
                Console.WriteLine(ex.Message);

                return 0;
               
            }


            return price;

        }

        public static List<decimal> PricesOfService(int serviceID)
        {
            return null;
        }
    }
}
