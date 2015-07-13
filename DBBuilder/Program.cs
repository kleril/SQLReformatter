using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows.Forms;

namespace DBBuilder
{
    class Program
    {
        private static SqlConnection connection;

        static void Main(string[] args)
        {
            try
            {
                //Connect to a remote instance of SQL Server.
		SecureString ss = new SecureString();
                var ca = "Ecru9278Fudge".ToCharArray();
                foreach (char n in ca)
                {
                    ss.AppendChar(n);
                }
                ss.MakeReadOnly();
                SqlCredential creds = new SqlCredential("CozDev01_DBA!Us3rAcc0unt@zypnl8g76k", ss);

                connection = new SqlConnection("Server=zypnl8g76k.database.windows.net;database=master", creds);
                connection.Open();
                SqlCommand command = connection.CreateCommand();

                //Create new DB
                string dbName = "TestDb";

                command.CommandText = "CREATE DATABASE " + dbName;
                command.ExecuteNonQuery();

                var scriptRan = ScriptExecutor.executeScript(command, dbName);
                if (scriptRan)
                {
                    Console.WriteLine("It worked!");
                }
                else
                {
                    Console.WriteLine("It didn't work");
                }
                connection.Close();
            }
            catch (Exception e)
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                Console.WriteLine(e.ToString());
            }
            Console.ReadLine();
        }
    }
}
