using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NETPayRoll
{
    internal class EmployeePayRoll
    {
        public static string dbpath = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=employ_PayRoll;Integrated Security=True";
        SqlConnection connect = new SqlConnection(dbpath);
        //Fuction to check DB connection Establishment
        public void DatabseConnection()
        {
            try
            {
                connect.Open();
                using (connect)
                {
                    Console.WriteLine("Database connectivity successful.");
                }
                connect.Close();
            }
            catch
            {
                Console.WriteLine("Database connectivity failed");
            }
        }

    }
}

