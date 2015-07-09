using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.VersionControl.Client;
using System.IO;
using GitSharp.Commands;

namespace SQLReformatter
{
    class FileDownloader
    {
        public static List<String> download()
        {
            string url = Program.downloadUrl;

			string file = "/Test.cs";
			string filePath = "../../Test.cs";

            List<String> lines = new List<String>();
            using (WebClient client = new WebClient())
            {
                string downloaded = client.DownloadString(url);
                string[] lineArray = downloaded.Split(new char[]{'\n'});
                lines.AddRange(lineArray);

				string fileResource = url + file;
				Console.WriteLine (fileResource);

				client.DownloadFile (fileResource, filePath);

            }
            return lines;
        }

		public static void downloadFromGitHub()
		{
			
		}

		/*
		public static void downloadFileFromTFS()
		{
			string teamProjectCollectionUrl = Program.downloadUrl;
			string filePath = "/Test.cs";

			// Get the version control server
			TfsTeamProjectCollection teamProjectCollection = TfsTeamProjectCollectionFactory.GetTeamProjectCollection(new Uri(teamProjectCollectionUrl));
			VersionControlServer versionControlServer = teamProjectCollection.GetService<VersionControlServer>();

			// Get the latest Item for filePath
			Item item = versionControlServer.GetItem(filePath, VersionSpec.Latest);

			// Download and display content to console
			string fileString = string.Empty;

			using (Stream stream = item.DownloadFile())
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					stream.CopyTo(memoryStream);

					// Use StreamReader to read MemoryStream created from byte array
					using (StreamReader streamReader = new StreamReader(new MemoryStream(memoryStream.ToArray())))
					{
						fileString = streamReader.ReadToEnd();
					}
				}
			}

			Console.WriteLine(fileString);
		}
		*/
    }
}
