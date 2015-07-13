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
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DBBuilder
{
    class Program
    {
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
                SqlCredential creds = new SqlCredential("CozDev01_DBA!Us3rAcc0unt@zypnl8g76k", ss);

                SqlConnection connection = new SqlConnection("tcpid:zypnl8g76k.database.windows.net", creds);
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
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            //The connection is automatically disconnected when the Server variable goes out of scope
            Console.ReadLine();

        }
    }
}
