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
        SqlConnection Connect = new SqlConnection(dbpath);

      
        public void DatabseConnection()
        {
            try
            {
                Connect.Open();
                using (Connect)
                {
                    Console.WriteLine("Database connectivity successful.");
                }
                Connect.Close();
            }
            catch
            {
                Console.WriteLine("Database connectivity failed");
            }
        }
        public void GetAllEmployeeData()
        {


            ModelClass model = new ModelClass();
            using (Connect)
            {
                string query = @"SELECT * FROM employ_PayRoll;";
                SqlCommand cmd = new SqlCommand(query, Connect);
                Connect.Open();
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
            Connect.Close();
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
        public void createARecord()
        {
            SqlConnection connect = new SqlConnection(dbpath);
            using (Connect)
            {
                Connect.Open();
                ADO.NETPayRoll.ModelClass model = new ADO.NETPayRoll.ModelClass();
                Console.WriteLine("Name of Employee:");
                model.NAME = Console.ReadLine();
                Console.WriteLine("salary of Employee:");
                model.SALARY = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Start Date of Employee:");
                model.START = DateTime.Now;
                Console.WriteLine("Gender of Employee:");
                model.gender = Console.ReadLine();
                Console.WriteLine("phone num of Employee:");
                model.PHONENO = Convert.ToDecimal(Console.ReadLine());
                Console.WriteLine("Address of Employee:");
                model.ADDRESS = Console.ReadLine();
                Console.WriteLine("Department of Employee:");
                model.DEPARTMENT = Console.ReadLine();
                Console.WriteLine("Basic Pay of Employee:");
                model.BASIC_PAY = Convert.ToDouble(Console.ReadLine());
                SqlCommand exp = new SqlCommand("SpAddEmployeeDetails", connect);
                exp.CommandType = CommandType.StoredProcedure;
                exp.Parameters.AddWithValue("@ID", model.ID);
                exp.Parameters.AddWithValue("@Name", model.NAME);
                exp.Parameters.AddWithValue("@start", model.START);
                exp.Parameters.AddWithValue("@Gender", model.gender);
                exp.Parameters.AddWithValue("@PHONENO", model.PHONENO);
                exp.Parameters.AddWithValue("@ADDRESS", model.ADDRESS);
                exp.Parameters.AddWithValue("@DEPARTMENT", model.DEPARTMENT);
                exp.Parameters.AddWithValue("@BASIC_PAY", model.BASIC_PAY);
                exp.ExecuteNonQuery();
                Console.WriteLine("Record created successfully.");
                Connect.Close();
            }
        }


    }
}



