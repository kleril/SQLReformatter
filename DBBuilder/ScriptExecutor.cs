using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace DBBuilder
{
	class ScriptExecutor
	{
		private static string path = ConfigurationManager.ConnectionStrings["ScriptFilePath"].ConnectionString;
		//Take script from file, execute on server.

		public static bool executeScript(SqlCommand command, string dbName)
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
					command.CommandText = nextQuery;
					command.ExecuteNonQuery();
				}
				return true;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
				return false;
			}
		}
	}
}


