
using LogicLayer.PeopleModule;
using System.Data;

namespace Booking_Service_Management
{
    internal class Program
    {
        static void ChooseCountry( ref ClsPerson person)
        {
            Console.WriteLine("Please Chooes Country from 1 to 230 countries \n");

            DataTable? dt = ClsCountries.GetEachCountry();
            Console.WriteLine("Numbert |         Country Name ");
            foreach (DataRow row in dt.Rows)
            {
                Console.WriteLine($" {row["CountryID"]} |   {row["Name"]}");
            }

            Console.WriteLine();
            byte Number = Convert.ToByte( Console.ReadLine() );

            person.CountryID = Number;
          
        }

        static bool PrintContactInformation(ref ClsPerson Person)
        {
            Console.WriteLine(" Pleses Enter Your Contact Infromation ");

            ClsContactsInfo contactInfo = new ClsContactsInfo();

            Console.WriteLine("Enter Phone   : ");
            contactInfo.Phone = Console.ReadLine();
            Console.WriteLine("Enter Email   : ");
            contactInfo.Email = Console.ReadLine();
            Console.WriteLine("Enter Address : ");
            contactInfo.Address = Console.ReadLine();

            if (contactInfo.Save())
            {
               Person.ContactInformationID = contactInfo.ID;
                return true;
            }
            return false;
        }
        static void testCreationOneInstensofFullInfoPerson()
        {
            ClsPerson Person1 = new ClsPerson();
            Console.WriteLine("Good Evening Sir we need some inforamtion about you so pleses fill this form \n\n");
            Console.Write("Enter First Name : ");
            Person1.FirstName = Console.ReadLine();
            Console.Write("Enter Last Name  : ");
            Person1.LastName = Console.ReadLine();
            Person1.BirthDay = DateTime.UtcNow;
            ChooseCountry(ref Person1);
            Person1.PaymentInformationID = null;
            if(!PrintContactInformation(ref Person1))
            {
                return;
            }

            if (Person1.Save())
            {
                Console.WriteLine("Thinks for involve your information ");
            }
            else
            {
                 
                Console.WriteLine("Error For completed sorry :<");
            }
            }


            static void Main(string[] args)
        {
            testCreationOneInstensofFullInfoPerson();
        }
    }
}
