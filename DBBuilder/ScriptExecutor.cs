using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBBuilder
{
    class ScriptExecutor
    {
        private static string path = ConfigurationManager.ConnectionStrings["scriptFilePath"].ConnectionString;
        //Take script from file, execute on server.
        public static bool executeScript(SqlConnection string db)
        {
            List<string> queries = new List<string>();
            string builder = "";
            try
            {
                StreamReader reader = new StreamReader(path);
                while (!reader.EndOfStream)
                {
                    string nextLine = reader.ReadLine();
                    if (nextLine.Equals("GO"))
                    {
                        queries.Add(builder);
                        builder = "";
                    }
                    else
                    {
                        builder = builder + nextLine;
                    }
                }
                foreach (string nextQuery in queries)
                {
                    db.ExecuteNonQuery(nextQuery);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return true;
        }
    }
}
