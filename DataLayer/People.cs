using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace DataLayer
{
    public class People
    {

        public static int? AddNewPerson(string FirstName,string LastName , DateTime? BirthDay,
            int? CountryID,int? Contact_InfoID, int? Payment_InfoID,string AdditionalNotes)
        {

            int? personID = null;
            try
            {
                using (SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {
                    Connection.Open();

                    string query = @"insert into people Values(@FirstName,@LastName,@BirthDay,@CountryID,@Contact_InfoID,@Payment_InfoID,@Notes)
                            Select scope_identity();";

                    using (SqlCommand command = new SqlCommand(query, Connection))
                    {
                        command.Parameters.AddWithValue(@"FirstName", FirstName);
                        command.Parameters.AddWithValue(@"LastName", LastName);
                        command.Parameters.AddWithValue(@"BirthDay", BirthDay);
                        command.Parameters.AddWithValue(@"CountryID", CountryID);
                        command.Parameters.AddWithValue(@"Contact_InfoID", Contact_InfoID);

                        if ((Payment_InfoID) == null)
                        {
                            command.Parameters.AddWithValue(@"Payment_InfoID", System.DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue(@"Payment_InfoID", Payment_InfoID);
                        }

                        if (string.IsNullOrEmpty(AdditionalNotes))
                        {
                            command.Parameters.AddWithValue(@"Notes", System.DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue(@"Notes", AdditionalNotes);
                        }


                       object Val = command.ExecuteScalar();

                        if (Val != null && int.TryParse(Val.ToString(), out int ID))
                        {
                            personID = ID;
                        }
                    };
                };

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                personID = null;
            }



            return personID;
        }


        public static bool UpdatePerson(int? personID , string FirstName,string LastName,DateTime? BirthDay,int? CountryID,int? Contact_InfoID
            , int? Payment_InfoID,string AdditionalNotes)
        {

            byte RowEffect = 0;
            try
            {
                using (SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {
                    Connection.Open();

                    string query = @"Update people
                                    set FirstName = @FirstName,
                                        LastName   = @LastName,
                                        BirthDay   = @BirthDay,
                                        CountryID  = @CountryID,
                                        Contact_InfoID = @Contact_InfoID,
                                        Payment_InfoID = @Payment_InfoID,
                                        AdditionalNotes = @Notes,
                                        where personID = @PersonID";

                    using (SqlCommand command = new SqlCommand(query, Connection))
                    {
                        command.Parameters.AddWithValue(@"PersonID",personID);
                        command.Parameters.AddWithValue(@"FirstName", FirstName);
                        command.Parameters.AddWithValue(@"LastName", LastName);
                        command.Parameters.AddWithValue(@"BirthDay", BirthDay);
                        command.Parameters.AddWithValue(@"CountryID", CountryID);
                        command.Parameters.AddWithValue(@"Contact_InfoID", Contact_InfoID);

                        if ((Payment_InfoID) == null)
                        {
                            command.Parameters.AddWithValue(@"Payment_InfoID", System.DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue(@"Payment_InfoID", Payment_InfoID);
                        }

                        if (string.IsNullOrEmpty(AdditionalNotes))
                        {
                            command.Parameters.AddWithValue(@"Notes", System.DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue(@"Notes", AdditionalNotes);
                        }


                         RowEffect = (byte)command.ExecuteNonQuery();
                    };
                };

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }

            return (RowEffect > 0);
        }


        public static bool DeletePerson(int? ID)
        {
            if (ID == null)  return false; 

            byte RowEffected = 0;
            try
            {
                using (SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))

                {
                    Connection.Open();

                    string query = @"Delete people where personID = @ID";

                    using(SqlCommand command = new SqlCommand(query, Connection))
                    {

                        command.Parameters.AddWithValue(@"ID", ID);

                        RowEffected = (byte)command.ExecuteNonQuery();
                    }

                }// final block automatically close the connction and command
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }


            return (RowEffected > 0);
        }


        public static bool FindByID(int? ID, ref string FirstName, ref string LastName, ref DateTime BirthDay, ref byte age, ref int CountryID
            , ref int Contact_InfoID, ref int? payment_InfoID,ref string AdditionalNotes)
        {

            if(ID == null) return false;

            bool PushIsSuccessfully = false;
            try
            {

                using (SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {
                    Connection.Open();

                    string query = @"select *,Age = cast(DATEDIFF(YEAR,'2022-09-12',GETDATE())as tinyint) From people where personID = @ID";

                    using (SqlCommand Command = new SqlCommand(query, Connection))
                    {
                        Command.Parameters.AddWithValue(@"ID", ID);

                        using (SqlDataReader reader = Command.ExecuteReader())
                        {

                            if (reader.Read())
                            {
                                FirstName = (string)reader["FirstName"];
                                LastName = (string)reader["LastName"];
                                BirthDay = (DateTime)reader["BirthDay"];
                                CountryID = (int)reader["CountryID"];
                                Contact_InfoID = (int)reader["Contact_InfoID"];
                                age = (byte)reader["Age"];


                                if ((reader["Payment_InfoID"] == DBNull.Value))
                                {
                                    payment_InfoID = null;
                                }
                                else
                                {
                                    payment_InfoID = (int)reader["Payment_InfoID"];
                                }

                                if ((reader["Additional_Info"] == DBNull.Value) || string.IsNullOrEmpty((string)(reader["Additional_Info"])))
                                {
                                    AdditionalNotes = string.Empty;
                                }
                                else
                                {
                                    AdditionalNotes = (string)reader["Additional_Info"];
                                }

                                PushIsSuccessfully=true;
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

            return PushIsSuccessfully;
        }

        public static bool FindByFullName(ref int ID,  string FirstName,  string LastName, ref DateTime BirthDay,ref byte age, ref int CountryID
         , ref int Contact_InfoID, ref int? payment_InfoID,ref string AdditionalNotes)
        {

            if (string.IsNullOrEmpty(FirstName) && string.IsNullOrEmpty(LastName)) return false;

            bool PushIsSuccessfully = false;
            try
            {

                using (SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {
                    Connection.Open();

                    string query = @"select *,Age = cast(DATEDIFF(YEAR,'2022-09-12',GETDATE())as tinyint) From people where FirstName = @FirstName and lastName = @LastName";

                    using (SqlCommand Command = new SqlCommand(query, Connection))
                    {
                        Command.Parameters.AddWithValue(@"FirstName", FirstName);
                        Command.Parameters.AddWithValue(@"LastName", LastName);

                        using (SqlDataReader reader = Command.ExecuteReader())
                        {

                            if (reader.Read())
                            {
                                ID = (int) reader["personID"];
                                BirthDay = (DateTime)reader["BirthDay"];
                                CountryID = (int)reader["CountryID"];
                                Contact_InfoID = (int)reader["Contact_InfoID"];
                                age = (byte)reader["Age"];


                                if ((reader["Payment_InfoID"] == DBNull.Value))
                                {
                                    payment_InfoID = null;
                                }
                                else
                                {
                                    payment_InfoID = (int)reader["Payment_InfoID"];
                                }

                                if ((reader["AdditionalNotes"] == DBNull.Value) || string.IsNullOrEmpty((string)(reader["AdditionalNotes"])))
                                {
                                    AdditionalNotes = string.Empty;
                                }
                                else
                                {
                                    AdditionalNotes = (string)reader["AdditionalNotes"];
                                }

                                PushIsSuccessfully = true;
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

            return PushIsSuccessfully;
        }


        public static bool FindByBirthDay(ref int ID, ref string FirstName, ref string LastName,  DateTime BirthDay, ref byte age, ref int CountryID
          , ref int Contact_InfoID, ref int? payment_InfoID,ref string AdditionalNotes)
        {

            if (BirthDay == DateTime.MinValue) return false;

            bool PushIsSuccessfully = false;
            try
            {

                using (SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {
                    Connection.Open();

                    string query = @"select * ,Age = cast(DATEDIFF(YEAR,'2022-09-12',GETDATE())as tinyint) From people where BirthDay = @BirthDay";

                    using (SqlCommand Command = new SqlCommand(query, Connection))
                    {
                        Command.Parameters.AddWithValue(@"BirthDay", BirthDay);

                        using (SqlDataReader reader = Command.ExecuteReader())
                        {

                            if (reader.Read())
                            {
                                ID = (int)reader["personID"];
                                FirstName = (string)reader["FirstName"];
                                LastName = (string)reader["LastName"];
                                CountryID = (int)reader["CountryID"];
                                Contact_InfoID = (int)reader["Contact_InfoID"];
                                age = (byte)reader["Age"];

                                if ((reader["Payment_InfoID"] == DBNull.Value))
                                {
                                    payment_InfoID = null;
                                }
                                else
                                {
                                    payment_InfoID = (int)reader["Payment_InfoID"];
                                }

                                if ((reader["AdditionalNotes"] == DBNull.Value) || string.IsNullOrEmpty((string)(reader["AdditionalNotes"])))
                                {
                                    AdditionalNotes = string.Empty;
                                }
                                else
                                {
                                    AdditionalNotes = (string)reader["AdditionalNotes"];
                                }

                                PushIsSuccessfully = true;
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

            return PushIsSuccessfully;
        }



       public static DataTable GetEachPeople()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BookingMangement_DB"].ConnectionString))
                {
                    Connection.Open();

                    string query = @"select * From people";

                    using (SqlCommand command = new SqlCommand(query, Connection))
                    {

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                dt.Load(reader);
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return dt;
            }

            return dt;

        }

    }
}
