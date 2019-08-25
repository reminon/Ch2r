using Halo2;
using System;
using System.IO;
using System.Windows.Forms;
using XPExplorerBar;

namespace Ch2r
{
	/// <summary>
	/// All the global code is here.
	/// </summary>
	public class Global
	{
		/// <summary>
		/// Access to our main form.
		/// </summary>
		public static MainForm mainForm = null;

		public static AutoUpdate Update = new AutoUpdate();

		// Skin
		private static Skin skin = null;
		/// <summary>
		/// Get/set the current skin.
		/// </summary>
		public static Skin Skin
		{
			get {
				if (skin == null) {
					skin = new Skin();

					// Set default colors
					skin.current.Default();
				}

				return skin;
			}
			set {
				skin = value;
			}
		}


		/// <summary>
		/// Path of shared map files.
		/// </summary>
		public static string SharedPath
		{
			get {
				return cfg.SharedPath;
			}
			set {
				cfg.SharedPath = value;
			}
		}

		/// <summary>
		/// Returns the stream from a shared map.
		/// </summary>
		public static FileStream SharedStream(MapTypes type)
		{
			FileStream result = null;
			Map map = null;

			switch (type) {
				case MapTypes.MainMenu:
					map = MapByPath(SharedPath + "mainmenu.map");
					if (map != null)
						result = map.stream;
					break;
				case MapTypes.Shared:
					map = MapByPath(SharedPath + "shared.map");
					if (map != null)
						result = map.stream;
					break;
				case MapTypes.SinglePlayerShared:
					map = MapByPath(SharedPath + "single_player_shared.map");
					if (map != null)
						result = map.stream;
					break;
			}

			return result;
		}

		public static ConfigForm cfg = new ConfigForm();

		/// <summary>
		/// Access the maps from our main form.
		/// </summary>
		/// <param name="filepath">path of map</param>
		/// <returns>null if not found otherwise the map</returns>
		public static Map MapByPath(string filepath)
		{
			if (mainForm == null)
				return null;

			foreach (ListViewItem item in mainForm.mapsXPListView.Items) {
				Map map = (Map)item.Tag;
				if (map.Filename == filepath) {
					return map;
				}
			}

			return null;
		}

		/// <summary>
		/// Fix bug which resizes the TaskItems in an Expando.
		/// </summary>
		/// <param name="panel">reference to Task Panel.</param>
		public static void ResizeTaskItems(ref TaskPane panel)
		{
			for (int i = 0; i < panel.Expandos.Count; i++) {
				Expando exp = panel.Expandos[i];
				for (int j = 0; j < exp.Items.Count; j++) {
					Control ctrl = exp.Items[j];
					if (ctrl.GetType() == typeof(TaskItem)) {
						ctrl.Width = exp.Width - 16;
					}
				}
			}
		}

		/// <summary>
		/// Endian swap an integer.
		/// </summary>
		static public int ReverseEndian(int x) 
		{
			return ((x<<24) | ((x & 0xff00)<<8) | ((x & 0xff0000)>>8) | (x>>24));
		}

		/// <summary>
		/// Endian swap an unsigned integer.
		/// </summary>
		static public uint ReverseEndian(uint x) 
		{
			return ((x<<24) | ((x & 0xff00)<<8) | ((x & 0xff0000)>>8) | (x>>24));
		}

		static public string[] DraggedFiles(System.Windows.Forms.DragEventArgs e)
		{
			return (string[])e.Data.GetData(DataFormats.FileDrop);
		}

		/// <summary>
		/// Formats the size so it shows in bytes, KB, MB, GB.
		/// </summary>
		static public string ConvertSize(long size)
		{
			string result = "";
			long tmp = 0;

			if (size > 1024) {
				tmp = (size % 1024) / 100;
				size /= 1024;
				if (size > 1024) {
					tmp = (size % 1024) / 100;
					size /= 1024;
					if (size > 1024) {
						tmp = (size % 1024) / 100;
						size /= 1024;
						result = size + "." + tmp + " GB";
					} else {
						result = size + "." + tmp + " MB";
					}
				} else {
					result = size + "." + tmp + " KB";
				}
			} else {
				result = size + " bytes";
			}

			return result;
		}

		/// <summary>
		/// Returns current version of Ch2r.
		/// </summary>
		static public string Version
		{
			get {
				string ver = Application.ProductVersion;
				ver = "v" + ver.Substring(0, ver.LastIndexOf("."));
				return ver;
			}
		}
	}
}
