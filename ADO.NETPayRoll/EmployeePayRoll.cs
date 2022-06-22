using DocumentFormat.OpenXml.Drawing.Diagrams;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NETPayRoll
{
    public class EmployeePayRoll
    {
        public static string dbpath = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=employ_PayRoll;Integrated Security=True";
        SqlConnection Connection = new SqlConnection(dbpath);

      
        public void DatabseConnection()
        {
            try
            {
                Connection.Open();
                using (Connection)
                {
                    Console.WriteLine("Database connectivity successful.");
                }
                Connection.Close();
            }
            catch
            {
                Console.WriteLine("Database connectivity failed");
            }
        }
        public void GetAllEmployeeData()
        {


            ModelClass model = new ModelClass();
            using (Connection)
            {
                string query = @"SELECT * FROM employ_PayRoll;";
                SqlCommand cmd = new SqlCommand(query, Connection);
                Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();



                if (reader.HasRows)
                {
                    Console.WriteLine("ID\t|\tNAME\t|\tSALARY\t\t|\tSTART\n---------------------");
                    while (reader.Read())
                    {
                        model.ID = reader.GetInt32(0);
                        model.NAME = reader.GetString(1);
                        model.SALARY = reader.GetDouble(2);
                        model.START = reader.GetDateTime(3);
                        Console.WriteLine(model.ID + "\t|\t" + model.NAME + "\t|\t" + model.SALARY + "\t|\t" + model.START);
                    }
                }
                else
                {
                    Console.WriteLine("Records not found in Database.");
                }
                reader.Close();

            }
            Connection.Close();
        }
        public void updateRecords()
        {

            SqlConnection connection = new SqlConnection(dbpath);
            try
            {
                using (connection)
                {
                    Console.WriteLine("Enter name of employee to update basic pay:");
                    string name = Console.ReadLine();
                    Console.WriteLine("Enter basic pay to uodate:");
                    decimal salary = Convert.ToDecimal(Console.ReadLine());
                    connection.Open();
                    string query = "UPDATE employ_payRoll set SALARY =" + salary + "where NAME='" + name + "'";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.ExecuteNonQuery();
                    Console.WriteLine("Records updated successfully.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("-----\nError:Records are not updated.\n-------------");
            }
        }


    }
}



