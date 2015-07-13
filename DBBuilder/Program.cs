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
                var makeTableQuery = @"CREATE TABLE testTable(
                                    [OrgId] [int] NULL,
                                    [TimeStamp] [datetimeoffset](7) NULL,
                                    [AlertId] [varchar](6) NULL,
                                    [AlertData] [varchar](64) NULL,
                                    [Id] [int] IDENTITY(1,1) NOT NULL,
                             CONSTRAINT [PK_tblAlerts] PRIMARY KEY CLUSTERED 
                                (
                                [Id] ASC
                                )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
                            )
                    ";

                db.ExecuteNonQuery(makeTableQuery);

                var addToTableQuery = @"INSERT INTO testTable
                                        VALUES ('194387',
                                        '2007-05-08 12:35:29.1234567 +12:15',
                                        'alrtid',
                                        'oieanhireugbg'
                                        )";

                db.ExecuteNonQuery(addToTableQuery);

                var dbs = srv.Databases;
                Console.WriteLine("Databases:");
                foreach (Database next in dbs)
                {
                    Console.WriteLine(next.Name);
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
