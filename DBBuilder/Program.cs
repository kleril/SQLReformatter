using System;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
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
                Server srv;
                ServerConnection connection = new ServerConnection("tcp:zypnl8g76k.database.windows.net", "CozDev01_DBA!Us3rAcc0unt@zypnl8g76k", "Ecru9278Fudge");

                //The strServer string variable contains the name of a remote instance of SQL Server. 
                srv = new Server(connection);
                //The actual connection is made when a property is retrieved. 
                Console.WriteLine("Version");
                Console.WriteLine(srv.Information.Version);
                //Define a Database object variable by supplying the parent server and the database name arguments in the constructor. 
                Database db;
                db = new Database(srv, "Test_SMO_Database " + DateTime.Now.ToShortTimeString());
                db.Create();
                var scriptRan = ScriptExecutor.executeScript(db);
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
