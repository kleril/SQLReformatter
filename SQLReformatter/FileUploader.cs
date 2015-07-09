using GitHubPushLib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.VersionControl.Client;
using System.IO;
using GitHubPushLib;

namespace SQLReformatter
{
    class FileUploader
    {
        public static void upload(string fileName)
        {
            string url = Program.uploadUrl;
            using (WebClient client = new WebClient())
            {
                try
                {
                    client.UploadFile(url, fileName);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        public static void uploadFileToGit(string fileName)
        {
            Console.Write("Github Username:");
            var user = Console.ReadLine();

            var service = new ContentService(Program.token);

            var repo = ConfigurationManager.AppSettings[Program.REPO_KEY];

            var file = new DiskFile(fileName);
            var target = new FileTarget(user, repo, file.Name);

            try
            {
                service.PushFile(file, target, "pushing file via GitHubPushLib");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        

		public static void uploadFileToTFS()
		{
			string uploadUrl = Program.uploadUrl;

			// Get a reference to our Team Foundation Server.
			TfsTeamProjectCollection tpc = new TfsTeamProjectCollection(new Uri(uploadUrl));

			// Get a reference to Version Control.
			VersionControlServer versionControl = tpc.GetService<VersionControlServer>();


			// Create a workspace.
			Workspace workspace = versionControl.CreateWorkspace("tempWorkSpace", versionControl.AuthorizedUser);

			String topDir = null;

			try
			{
				String localDir = "../..";
				Console.WriteLine("Create a mapping.");
				workspace.Map(uploadUrl, localDir);

				Console.WriteLine("Get the files from the repository.");
				workspace.Get();

				Console.WriteLine("Create a file.");
				topDir = Path.Combine(workspace.Folders[0].LocalItem, "sub");
				Directory.CreateDirectory(topDir);
				String fileName = Path.Combine(topDir, "basic.cs");
				using (StreamWriter sw = new StreamWriter(fileName))
				{
					sw.WriteLine("revision 1 of basic.cs");
				}

				Console.WriteLine("Now add everything.");
				workspace.PendAdd(topDir, true);

				Console.WriteLine("Show our pending changes.");
				PendingChange[] pendingChanges = workspace.GetPendingChanges();
				Console.WriteLine("Your current pending changes:");
				foreach (PendingChange pendingChange in pendingChanges)
				{
					Console.WriteLine("path: " + pendingChange.LocalItem +
						", change: " + PendingChange.GetLocalizedStringForChangeType(pendingChange.ChangeType));
				}

				Console.WriteLine("Checkin the items we added.");
				int changesetNumber = workspace.CheckIn(pendingChanges, "Sample changes");
				Console.WriteLine("  Checked in changeset " + changesetNumber);

				Console.WriteLine("Checkout and modify the file.");
				workspace.PendEdit(fileName);
				using (StreamWriter sw = new StreamWriter(fileName))
				{
					sw.WriteLine("revision 2 of basic.cs");
				}

				Console.WriteLine("Get the pending change and check in the new revision.");
				pendingChanges = workspace.GetPendingChanges();
				changesetNumber = workspace.CheckIn(pendingChanges, "Modified basic.cs");
				Console.WriteLine("  Checked in changeset " + changesetNumber);
			}
			finally
			{
				if (topDir != null)
				{
					Console.WriteLine("Delete all of the items under the test project.");
					workspace.PendDelete(topDir, RecursionType.Full);
					PendingChange[] pendingChanges = workspace.GetPendingChanges();
					if (pendingChanges.Length > 0)
					{
						workspace.CheckIn(pendingChanges, "Clean up!");
					}

					Console.WriteLine("Delete the workspace.");
					workspace.Delete();
				}
			}
		}
	}
}
