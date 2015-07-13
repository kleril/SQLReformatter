using System;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace DBBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Connect to a remote instance of SQL Server.
                SqlCredential creds = new SqlCredential(, "CozDev01_DBA!Us3rAcc0unt@zypnl8g76k", "Ecru9278Fudge");

                SqlConnection connection = new SqlConnection("tcpid:zypnl8g76k.database.windows.net", creds);

                //Create new DB
                string dbName = "TestDb";

                var scriptRan = ScriptExecutor.executeScript(connection, dbName);
                if (scriptRan)
                {
                    Console.WriteLine("It worked!");
                }
                else
                {
                    Console.WriteLine("It didn't work");
                }
                db.Drop();
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
