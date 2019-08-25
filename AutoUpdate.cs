using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace Ch2r
{
	public class AutoUpdate
	{
		WebClient wc = new WebClient();
		bool update = false;
		string updatefile = "";
		string updateexe = "";

		public AutoUpdate()
		{
		}

		public bool UpdateFound
		{
			get {
				if (!update) {
					update = LookForUpdate();
				}

				return update;
			}
		}

		public bool RunUpdate()
		{
			bool result = false;

			try {
				wc.DownloadFile(updateexe, Application.StartupPath + "\\update.exe");

				Process.Start(Application.StartupPath + "\\update.exe", Application.StartupPath);

				Application.Exit();

				result = true;
			} catch (WebException) {
				MessageBox.Show("Update not found on server");
			}

			return result;
		}

		bool LookForUpdate()
		{
			bool result = false;

			try {
				updatefile = Path.GetTempFileName();
				wc.DownloadFile("http://users.telenet.be/-_X_-/ch2r/update.txt", updatefile);

				FileStream fs = new FileStream(updatefile, FileMode.Open, FileAccess.Read);
				StreamReader sr = new StreamReader(fs);
				string data = sr.ReadToEnd();
				sr.Close();
				fs.Close();

				string appver = Application.ProductVersion;
				appver = appver.Substring(0, appver.LastIndexOf(".")).Replace(".", "");
				string updver = data.Substring(0, data.IndexOf(" "));
				string updurl = data.Substring(data.IndexOf(" ") + 1);

				if (int.Parse(updver) > int.Parse(appver)) {
					result = true;
					updateexe = updurl;
				}
			} catch (WebException) {
				// Update not found
			}
			
			return result;
		}
	}
}
