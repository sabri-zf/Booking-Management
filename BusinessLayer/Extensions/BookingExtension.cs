using BusinessLayer.Booking_Commponent;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Extensions
{
    public static class BookingExtension
    {


        public static IEnumerable<DataRow> Filter(this IEnumerable<DataRow> List,Func<DataRow,bool> Action)
        {

            foreach (var B in List)
            {

                if (Action(B))
                {
                    yield return B;
                }
            }
        }

        public static void PrintList(this IEnumerable<DataRow> Source, string Title)
        {

            Console.WriteLine(Title);
            foreach (DataRow B in Source)
            {
                Console.WriteLine($"{B["ID"]} , User ID : {B["UserID"]}");
            }
        }

    }
}
